using EpsAdmissions.Application.Interfaces.Messaging;
using Microsoft.Extensions.Logging;

namespace EpsAdmissions.Infrastructure.Messaging;

public sealed class AdmissionEventPublisher(
    ILogger<AdmissionEventPublisher> logger)
    : IAdmissionEventPublisher
{
    public Task PublishAsync(string eventType, string payload, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("Publicando evento {EventType}: {Payload}", eventType, payload);

        return Task.CompletedTask;
    }
}