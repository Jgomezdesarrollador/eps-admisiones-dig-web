namespace EpsAdmissions.Application.DTOs.Admissions;

public class AdmissionRequest
{
    public PatientDto Patient { get; set; } = default!;

    public ClinicalHistoryDto ClinicalHistory { get; set; } = default!;

    public CopaymentDto Copayment { get; set; } = default!;

    public MetadataDto Metadata { get; set; } = default!;
}