namespace EpsAdmissions.Infrastructure.Persistence.Mongo.Documents;

public sealed class MedicationDocument
{
    public string Name { get; set; } = string.Empty;
    public string Dose { get; set; } = string.Empty;
}