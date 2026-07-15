using EpsAdmissions.Application.DTOs.Admissions;
using EpsAdmissions.Application.Interfaces;
using EpsAdmissions.Infrastructure.Persistence.Mongo.Context;
using EpsAdmissions.Infrastructure.Persistence.Mongo.Mappers;

namespace EpsAdmissions.Infrastructure.Repositories;

public sealed class MongoClinicalHistoryStorage(MongoContext context) : IClinicalHistoryStorage
{
    public async Task<string> SaveAsync(AdmissionRequest request, CancellationToken cancellationToken = default)
    {
        var document = ClinicalHistoryMapper.ToDocument(request);

        await context.ClinicalHistories.InsertOneAsync(document, cancellationToken: cancellationToken);

        return document.Id;
    }
}