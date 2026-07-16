using Microsoft.EntityFrameworkCore;
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

    public async Task<List<OutboxMessage>> GetPendingAsync(CancellationToken cancellationToken = default)
    {
        return await context.OutboxMessages
            .Where(x => !x.Processed)
            .OrderBy(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public Task UpdateAsync(OutboxMessage message, CancellationToken cancellationToken = default)
    {
        context.OutboxMessages.Update(message);

        return Task.CompletedTask;
    }
}