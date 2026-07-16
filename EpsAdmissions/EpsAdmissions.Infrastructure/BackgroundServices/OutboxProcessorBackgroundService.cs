using EpsAdmissions.Application.Interfaces;
using EpsAdmissions.Application.Interfaces.Messaging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EpsAdmissions.Infrastructure.BackgroundServices;

public sealed class OutboxProcessorBackgroundService(
    IServiceScopeFactory scopeFactory,
    ILogger<OutboxProcessorBackgroundService> logger)
    : BackgroundService
{
    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        logger.LogInformation("Outbox Processor iniciado.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                using var scope = scopeFactory.CreateScope();

                var repository = scope.ServiceProvider.GetRequiredService<IOutboxRepository>();

                var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

                var publisher = scope.ServiceProvider.GetRequiredService<IAdmissionEventPublisher>();

                var messages = await repository.GetPendingAsync(stoppingToken);

                foreach (var message in messages)
                {
                    logger.LogInformation("Procesando evento {EventType}",message.EventType);

                    await publisher.PublishAsync(message.EventType, message.Payload, stoppingToken);

                    // Se envia mensaje por SignalR
                    message.MarkAsProcessed();

                    await repository.UpdateAsync(message, stoppingToken);
                }

                await unitOfWork.SaveChangesAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                logger.LogError(ex,"Error procesando mensajes del Outbox.");
            }

            await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
        }
    }
}