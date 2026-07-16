namespace EpsAdmissions.Application.DTOs.Responses;

public sealed class AdmissionResponse
{
    public Guid AdmissionId { get; init; }

    public string Status { get; init; } = string.Empty;
}