using EpsAdmissions.Domain.Enums;

namespace EpsAdmissions.Domain.Entities;

public sealed class Admission
{
    public Guid Id { get; private set; }

    public string PatientDocument { get; private set; }

    public decimal CopaymentAmount { get; private set; }

    public DateTime AdmissionDate { get; private set; }

    public AdmissionStatus Status { get; private set; }

    public string MongoDocumentId { get; private set; }

   
    public Admission(string patientDocument, decimal copaymentAmount, string mongoDocumentId)
    {
        Id = Guid.NewGuid();
        PatientDocument = patientDocument;
        CopaymentAmount = copaymentAmount;
        MongoDocumentId = mongoDocumentId;
        AdmissionDate = DateTime.UtcNow;
        Status = AdmissionStatus.Pending;
    }

    public Admission()
    {
    }

    public void MarkAsCompleted()
    {
        Status = AdmissionStatus.Completed;
    }

    public void MarkAsFailed()
    {
        Status = AdmissionStatus.Failed;
    }
}
