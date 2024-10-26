using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using TP5.Models;
namespace TP5.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductosController : ControllerBase
{
    private ProductosRepository pr;
    private List<Productos> productos;

    public ProductosController() {
        this.pr = new ProductosRepository();
    }

    [HttpGet("api/Producto")]
    public ActionResult<List<Productos>> GetAllProducts() {
        this.productos = pr.GetAllProducts();
        return Ok(productos);
    }
}