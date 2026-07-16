using EpsAdmissions.Application.DTOs.Admissions;
using EpsAdmissions.Application.DTOs.Responses;

namespace EpsAdmissions.Application.Interfaces;

public interface IAdmitPatientUseCase
{
    Task<AdmissionResponse> HandleAsync(AdmissionRequest request, CancellationToken cancellationToken = default);
}