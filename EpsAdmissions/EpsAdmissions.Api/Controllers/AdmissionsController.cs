using EpsAdmissions.Application.DTOs.Admissions;
using EpsAdmissions.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace EpsAdmissions.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class AdmissionsController(IAdmitPatientUseCase admitPatientUseCase,
    ILogger<AdmissionsController> logger) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Admit(AdmissionRequest request, CancellationToken cancellationToken)
    {
        if (request is null)
            return BadRequest();

        logger.LogInformation("Iniciando proceso de admisión de un paciente.");

        var response = await admitPatientUseCase.HandleAsync(request, cancellationToken);

        logger.LogInformation("Admisión creada correctamente. Id: {AdmissionId}", response.AdmissionId);

        return Created($"/api/admissions/{response.AdmissionId}", response);
    }
}