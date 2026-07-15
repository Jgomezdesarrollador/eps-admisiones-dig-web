using EpsAdmissions.Application.DTOs.Admissions;

namespace EpsAdmissions.Application.Interfaces;

public interface IClinicalHistoryRepository
{
    Task<string> SaveAsync(AdmissionRequest request, CancellationToken cancellationToken = default);
}