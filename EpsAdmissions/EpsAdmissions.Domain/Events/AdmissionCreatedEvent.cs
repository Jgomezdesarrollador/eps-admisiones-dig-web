namespace EpsAdmissions.Domain.Events;

public sealed class AdmissionCreatedEvent
{
    public Guid AdmissionId { get; init; }
    public string PatientDocument { get; init; } = string.Empty;
    public string MongoDocumentId { get; init; } = string.Empty;
}