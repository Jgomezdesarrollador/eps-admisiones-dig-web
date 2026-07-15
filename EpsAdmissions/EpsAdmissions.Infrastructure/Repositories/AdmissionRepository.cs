using EpsAdmissions.Application.Interfaces;
using EpsAdmissions.Domain.Entities;
using EpsAdmissions.Infrastructure.Persistence.Sql.Context;

namespace EpsAdmissions.Infrastructure.Repositories;

public sealed class AdmissionRepository(AdmissionDbContext context) : IAdmissionRepository
{
    public async Task AddAsync(Admission admission, CancellationToken cancellationToken = default)
    {
        await context.Admissions.AddAsync(admission, cancellationToken);
    }
}