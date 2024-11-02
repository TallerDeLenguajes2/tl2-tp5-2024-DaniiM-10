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

    [HttpGet("api/Presupuesto")]
    public ActionResult<List<Productos>> GetAllPresupuestos() => Ok(presupuestosRepository.GetPresupuestos());
    
}