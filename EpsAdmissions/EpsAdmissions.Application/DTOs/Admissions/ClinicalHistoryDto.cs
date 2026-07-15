namespace EpsAdmissions.Application.DTOs.Admissions;

public class ClinicalHistoryDto
{
    public List<DiagnosisDto> Diagnoses { get; set; } = [];

    public List<MedicationDto> Medications { get; set; } = [];

    public List<string> Allergies { get; set; } = [];
}