namespace EpsAdmissions.Infrastructure.Persistence.Mongo.Documents;

public sealed class ClinicalHistorySection
{
    public List<DiagnosisDocument> Diagnoses { get; set; } = [];

    public List<MedicationDocument> Medications { get; set; } = [];

    public List<string> Allergies { get; set; } = [];
}