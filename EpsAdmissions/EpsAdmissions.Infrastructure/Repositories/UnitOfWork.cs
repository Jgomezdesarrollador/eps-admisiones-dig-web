using EpsAdmissions.Application.Interfaces;
using EpsAdmissions.Infrastructure.Persistence.Sql.Context;

namespace EpsAdmissions.Infrastructure.Repositories;

public sealed class UnitOfWork(AdmissionDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await context.SaveChangesAsync(cancellationToken);
    }
}