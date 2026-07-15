using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace EpsAdmissions.Infrastructure.Persistence.Mongo.Documents;

public sealed class ClinicalHistoryDocument
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string Id { get; set; } = default!;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public PatientDocument Patient { get; set; } = default!;

    public ClinicalHistorySection ClinicalHistory { get; set; } = default!;

    public CopaymentDocument Copayment { get; set; } = default!;

    public MetadataDocument Metadata { get; set; } = default!;
}