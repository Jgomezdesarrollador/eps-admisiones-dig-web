using EpsAdmissions.Application.DTOs.Admissions;
using EpsAdmissions.Infrastructure.Persistence.Mongo.Documents;

namespace EpsAdmissions.Infrastructure.Persistence.Mongo.Mappers;

public static class ClinicalHistoryMapper
{
    public static ClinicalHistoryDocument ToDocument(AdmissionRequest request)
    {
        return new ClinicalHistoryDocument
        {
            CreatedAt = DateTime.UtcNow,

            Patient = new PatientDocument
            {
                Document = request.Patient.Document,
                DocumentType = request.Patient.DocumentType,
                FirstName = request.Patient.FirstName,
                LastName = request.Patient.LastName,
                BirthDate = request.Patient.BirthDate
            },

            ClinicalHistory = new ClinicalHistorySection
            {
                Allergies = request.ClinicalHistory.Allergies,

                Diagnoses = request.ClinicalHistory.Diagnoses
                    .Select(x => new DiagnosisDocument
                    {
                        Code = x.Code,
                        Description = x.Description
                    })
                    .ToList(),

                Medications = request.ClinicalHistory.Medications
                    .Select(x => new MedicationDocument
                    {
                        Name = x.Name,
                        Dose = x.Dose
                    })
                    .ToList()
            },

            Copayment = new CopaymentDocument
            {
                Amount = request.Copayment.Amount,
                Currency = request.Copayment.Currency
            },

            Metadata = new MetadataDocument
            {
                SourceSystem = request.Metadata.SourceSystem,
                ReceivedAt = request.Metadata.ReceivedAt
            }
        };
    }
}