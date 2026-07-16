namespace EpsAdmissions.Application.Interfaces.Messaging;

public interface IAdmissionEventPublisher
{
    Task PublishAsync(string eventType,string payload, CancellationToken cancellationToken = default);
}