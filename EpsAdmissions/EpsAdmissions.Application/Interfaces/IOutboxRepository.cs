using EpsAdmissions.Domain.Entities;

namespace EpsAdmissions.Application.Interfaces;

public interface IOutboxRepository
{
    Task AddAsync(OutboxMessage message, CancellationToken cancellationToken = default);
}