using EpsAdmissions.Infrastructure.Persistence.Mongo.Documents;
using EpsAdmissions.Infrastructure.Persistence.Mongo.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace EpsAdmissions.Infrastructure.Persistence.Mongo.Context;

public sealed class MongoContext
{
    private readonly IMongoDatabase _database;

    public MongoContext(IMongoClient client,IOptions<MongoSettings> options)
    {
        var settings = options.Value;

        _database = client.GetDatabase(settings.DatabaseName);
    }

    public IMongoCollection<ClinicalHistoryDocument> ClinicalHistories =>
        _database.GetCollection<ClinicalHistoryDocument>("ClinicalHistories");
}