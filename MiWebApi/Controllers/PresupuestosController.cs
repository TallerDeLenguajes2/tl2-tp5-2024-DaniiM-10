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
    public ActionResult<List<Productos>> GetPresupuestos() => Ok(presupuestosRepository.GetPresupuestos());
    
    [HttpGet("api/{idPrresupuesto:int}")]
    public ActionResult<Productos> GetPresupuestoId(int idPrresupuesto) => Ok(presupuestosRepository.GetPresupuestoId(idPrresupuesto));
    
    [HttpPost("api")]
    public ActionResult PostPresupuesto([FromBody] Presupuestos presupuesto) => Ok(presupuestosRepository.PostPresupuesto(presupuesto));
    
    [HttpPost("api/{idPresupuesto:int}/ProductoDetalle")]
    public ActionResult PostPresupuest(int idPresupuesto, [FromBody] PresupuestosDetallesPost presupuestosDetalles) => Ok(presupuestosRepository.PostPresupuestoDetalle(idPresupuesto, presupuestosDetalles));
}