namespace EpsAdmissions.Infrastructure.Persistence.Mongo.Documents;

public sealed class MetadataDocument
{
    public string SourceSystem { get; set; } = string.Empty;
    public DateTime ReceivedAt { get; set; }
}