using EpsAdmissions.Application.DTOs.Admissions;
using EpsAdmissions.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EpsAdmissions.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class AdmissionsController(IAdmitPatientUseCase admitPatientUseCase) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Admit(AdmissionRequest request, CancellationToken cancellationToken)
    {
        var response = await admitPatientUseCase.HandleAsync(request, cancellationToken);

        return Created($"/api/admissions/{response.AdmissionId}", response);
    }
}