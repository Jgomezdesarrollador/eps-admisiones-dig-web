app.MapGet("/reporte-mensual", async (AppDbContext dbContext) =>
{
    var resultado = await dbContext.Pacientes
        .AsNoTracking()
        .Where(p => p.Estado == "Activo")
        .Where(p => p.Atenciones.Any(a => a.RequiereAuditoria))
        .Select(p => new ReporteDto
        {
            NombreCompleto = p.Nombre + " " + p.Apellido,
            TotalAuditar = p.Atenciones
                .Where(a => a.RequiereAuditoria)
                .Sum(a => a.Valor)
        })
        .ToListAsync();

    return Results.Ok(resultado);
});