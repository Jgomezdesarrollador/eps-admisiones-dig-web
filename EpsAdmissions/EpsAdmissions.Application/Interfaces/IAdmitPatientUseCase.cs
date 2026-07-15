using EpsAdmissions.Application.DTOs.Admissions;

namespace EpsAdmissions.Application.Interfaces;

public interface IAdmitPatientUseCase
{
    Task ExecuteAsync(AdmissionRequest request, CancellationToken cancellationToken = default);
}