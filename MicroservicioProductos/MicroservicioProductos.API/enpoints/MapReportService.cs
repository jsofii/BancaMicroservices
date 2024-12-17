using MicroservicioProductos.Application.Services.Interfaces;

public static class ReportEndpoints
{
    public static void MapReportEndpoints(this IEndpointRouteBuilder endpoints)
    {
        endpoints.MapGet("/reportes/movimientos", async (IReportsService reportService,
                string clienteId, DateTime fechaInicio, DateTime fechaFin) =>
            {
                try
                {
                    var reporte = await reportService.GenerarEstadoDeCuentaReporteAsync(clienteId, fechaInicio, fechaFin);
                    return Results.Ok(reporte);
                }
                catch (KeyNotFoundException ex)
                {
                    return Results.NotFound(ex.Message);
                }
            })
            .WithName("GetMovimientoReport")
            .WithOpenApi();
    }
}