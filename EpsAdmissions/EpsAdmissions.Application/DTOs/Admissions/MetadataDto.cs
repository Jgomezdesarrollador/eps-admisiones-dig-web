namespace EpsAdmissions.Application.DTOs.Admissions;

public class MetadataDto
{
    public string SourceSystem { get; set; } = string.Empty;

    public DateTime ReceivedAt { get; set; }
}