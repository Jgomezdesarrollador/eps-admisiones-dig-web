using EpsAdmissions.Application.Interfaces;
using EpsAdmissions.Domain.Entities;
using EpsAdmissions.Infrastructure.Persistence.Sql.Context;

namespace EpsAdmissions.Infrastructure.Repositories;

public sealed class OutboxRepository(AdmissionDbContext context) : IOutboxRepository
{
    public async Task AddAsync(OutboxMessage message, CancellationToken cancellationToken = default)
    {
        await context.OutboxMessages.AddAsync(message, cancellationToken);
    }
}