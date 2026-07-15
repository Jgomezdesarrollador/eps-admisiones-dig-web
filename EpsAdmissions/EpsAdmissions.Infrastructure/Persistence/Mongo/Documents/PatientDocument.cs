namespace EpsAdmissions.Infrastructure.Persistence.Mongo.Documents;

public sealed class PatientDocument
{
    public string Document { get; set; } = string.Empty;

    public string DocumentType { get; set; } = string.Empty;

    public string FirstName { get; set; } = string.Empty;

    public string LastName { get; set; } = string.Empty;

    public DateOnly BirthDate { get; set; }
}