using EpsAdmissions.Domain.Entities;

namespace EpsAdmissions.Application.Interfaces;

public interface IAdmissionRepository
{
    Task AddAsync(Admission admission, CancellationToken cancellationToken = default);
}