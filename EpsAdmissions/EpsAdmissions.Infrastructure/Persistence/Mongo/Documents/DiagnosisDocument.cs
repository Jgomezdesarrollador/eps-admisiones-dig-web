namespace EpsAdmissions.Infrastructure.Persistence.Mongo.Documents;

public sealed class DiagnosisDocument
{
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}