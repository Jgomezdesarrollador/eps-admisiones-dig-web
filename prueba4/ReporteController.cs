[HttpGet("reporte-mensual")]
public async Task<IActionResult> GenerarReporteMensual()
{
    // Obtiene todos los pacientes y sus atenciones de la base de datos
    var pacientes = _dbContext.Pacientes
        .Include(p => p.Atenciones)
        .ToList();

    var resultado = new List<ReporteDto>();

    foreach (var p in pacientes)
    {
        if (p.Estado == "Activo" && p.Atenciones.Any(a => a.RequiereAuditoria))
        {
            resultado.Add(new ReporteDto
            {
            NombreCompleto = p.Nombre + " " + p.Apellido,
            TotalAuditar = p.Atenciones.Where(a => a.RequiereAuditoria).Sum(a => a.Valor)
            });
        }
    }
    return Ok(resultado);
}