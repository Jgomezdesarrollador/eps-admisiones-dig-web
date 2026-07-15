namespace EpsAdmissions.Application.DTOs.Admissions;

public class PatientDto
{
    public string Document { get; set; } = string.Empty;

    public string DocumentType { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateOnly BirthDate { get; set; }
}