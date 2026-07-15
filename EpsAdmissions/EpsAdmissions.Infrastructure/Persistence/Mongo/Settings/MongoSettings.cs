namespace EpsAdmissions.Infrastructure.Persistence.Mongo.Settings;

public sealed class MongoSettings
{
    public const string SectionName = "MongoDb";
    public string ConnectionString { get; init; } = string.Empty;
    public string DatabaseName { get; init; } = string.Empty;
    public string ClinicalHistoriesCollection { get; init; } = "ClinicalHistories";
}