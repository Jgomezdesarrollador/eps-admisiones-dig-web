using EpsAdmissions.Application.Interfaces.Messaging;
using EpsAdmissions.Infrastructure.SignalR;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace EpsAdmissions.Infrastructure.Messaging;

public sealed class SignalRAdmissionEventPublisher(IHubContext<AdmissionHub> hubContext,
    ILogger<SignalRAdmissionEventPublisher> logger) : IAdmissionEventPublisher
{
    public async Task PublishAsync(string eventType, string payload, CancellationToken cancellationToken = default)
    {
        logger.LogInformation("========== PUBLICANDO ==========");
        logger.LogInformation("Payload: {Payload}", payload);
        await hubContext.Clients.All.SendAsync("AdmissionCreated", payload, cancellationToken);
        logger.LogInformation("========== PUBLICADO ==========");
    }
}