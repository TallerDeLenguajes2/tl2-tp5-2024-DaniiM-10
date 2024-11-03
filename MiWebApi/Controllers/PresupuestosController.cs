using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using TP5.Models;
namespace TP5.Controllers;

[ApiController]
[Route("[controller]")]
public class PresupuestosController : ControllerBase
{
    private PresupuestosRepository presupuestosRepository;

    public PresupuestosController() {
        this.presupuestosRepository = new PresupuestosRepository();
    }

    [HttpGet("api")]
    public ActionResult<List<Presupuestos>> GetPresupuestos()
    {
        var presupuestos = presupuestosRepository.GetPresupuestos();

        if (presupuestos == null || !presupuestos.Any()) { return NotFound(new { message = "No se encontraron presupuestos." }); }
        return Ok(presupuestos);
    }
    
    [HttpGet("api/{idPresupuesto:int}")]
    public ActionResult<Presupuestos> GetPresupuestoId(int idPresupuesto)
    {
        var presupuesto = presupuestosRepository.GetPresupuestoId(idPresupuesto);
        if (presupuesto == null) { return NotFound(new { message = "Presupuesto no encontrado." }); }
        return Ok(presupuesto);
    }
    
    [HttpPost("api")]
    public ActionResult PostPresupuesto([FromBody] Presupuestos presupuesto)
    {
        if (presupuesto == null || string.IsNullOrWhiteSpace(presupuesto.NombreDestinatario) || presupuesto.FechaCreacion == DateTime.MinValue) { return BadRequest(new { message = "Datos de presupuesto inválidos." }); }

        var success = presupuestosRepository.PostPresupuesto(presupuesto);
        return success 
            ? Created(string.Empty, new { message = "Presupuesto creado con éxito." }) 
            : StatusCode(500, new { message = "Error al crear el presupuesto." });
    }
    
    [HttpPost("api/{idPresupuesto:int}/ProductoDetalle")]
    public ActionResult PostPresupuestoDetalle(int idPresupuesto, [FromBody] PresupuestosDetallesPost presupuestosDetalles)
    {
        if (presupuestosDetalles == null || presupuestosDetalles.cantidad <= 0 || presupuestosDetalles.idProducto <= 0) { return BadRequest(new { message = "Datos de detalle de presupuesto inválidos." }); }

        var success = presupuestosRepository.PostPresupuestoDetalle(idPresupuesto, presupuestosDetalles);
        return success 
            ? Ok(new { message = "Detalle de presupuesto agregado con éxito." })
            : NotFound(new { message = "Presupuesto o producto no encontrado." });
    }
}