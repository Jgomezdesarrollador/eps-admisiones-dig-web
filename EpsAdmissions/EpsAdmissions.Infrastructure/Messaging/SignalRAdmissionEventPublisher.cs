using EpsAdmissions.Application.Interfaces.Messaging;
using EpsAdmissions.Infrastructure.SignalR;
using Microsoft.AspNetCore.SignalR;

namespace EpsAdmissions.Infrastructure.Messaging;

public sealed class SignalRAdmissionEventPublisher(IHubContext<AdmissionHub> hubContext) : IAdmissionEventPublisher
{
    public async Task PublishAsync(string eventType, string payload, CancellationToken cancellationToken = default)
    {
        await hubContext.Clients.All.SendAsync("AdmissionCreated", payload, cancellationToken);
    }
}