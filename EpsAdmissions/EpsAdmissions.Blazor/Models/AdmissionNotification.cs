namespace EpsAdmissions.Blazor.Models;

public sealed class AdmissionNotification
{
    public Guid AdmissionId { get; init; }
    public string PatientDocument { get; init; } = string.Empty;
    public string MongoDocumentId { get; init; } = string.Empty;
    public DateTime ReceivedAt { get; init; } = DateTime.UtcNow;
}