using EpsAdmissions.Blazor.Configuration;
using EpsAdmissions.Domain.Events;
using Microsoft.AspNetCore.SignalR.Client;
using Microsoft.Extensions.Options;
using System.Text.Json;

namespace EpsAdmissions.Blazor.Services;

public sealed class AdmissionNotificationService : IAsyncDisposable
{
    private readonly HubConnection _hubConnection;
    private readonly ILogger<AdmissionNotificationService> _logger;

    public event Action<AdmissionCreatedEvent>? AdmissionReceived;

    public AdmissionNotificationService(IOptions<ApiSettings> settings, ILogger<AdmissionNotificationService> logger)
    {
        _logger = logger;

        _hubConnection = new HubConnectionBuilder()
            .WithUrl($"{settings.Value.BaseUrl}/hubs/admissions")
            .WithAutomaticReconnect()
            .Build();

        _hubConnection.On<string>(
            "AdmissionCreated",
            payload =>
            {
                _logger.LogInformation("RECIBÍ EVENTO");
                _logger.LogInformation(payload);
                var notification =
                    JsonSerializer.Deserialize<AdmissionCreatedEvent>(payload);

                if (notification is not null)
                {
                    try
                    {
                        AdmissionReceived?.Invoke(notification);
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, "Error notificando la admisión recibida.");
                    }
                }
            });

        _hubConnection.Reconnecting += error =>
        {
            _logger.LogWarning(error, "Reconectando con SignalR...");
            return Task.CompletedTask;
        };

        _hubConnection.Reconnected += connectionId =>
        {
            _logger.LogInformation("Reconectado a SignalR. ConnectionId: {ConnectionId}",
                connectionId);

            return Task.CompletedTask;
        };

        _hubConnection.Closed += error =>
        {
            if (error is null)
            {
                _logger.LogWarning("Conexión de SignalR cerrada sin excepción.");
            }
            else
            {
                _logger.LogError(error, "Conexión de SignalR cerrada con error.");
            }

            return Task.CompletedTask;
        };
    }

    public async Task StartAsync()
    {
        try
        {
            if (_hubConnection.State == HubConnectionState.Disconnected)
            {
                _logger.LogInformation("Conectando a ");

                await _hubConnection.StartAsync();

                _logger.LogInformation(
                    "Conectado. ConnectionId: {ConnectionId}",
                    _hubConnection.ConnectionId);
            }
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error conectando a SignalR.");
            throw;
        }
    }

    public async ValueTask DisposeAsync()
    {
        await _hubConnection.DisposeAsync();
    }
}