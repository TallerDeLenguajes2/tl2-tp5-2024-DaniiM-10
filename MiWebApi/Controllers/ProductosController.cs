using Microsoft.AspNetCore.Mvc;
using SQLitePCL;
using TP5.Models;
namespace TP5.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductosController : ControllerBase
{
    private ProductosRepository productosRepository;

    public ProductosController() {
        this.productosRepository = new ProductosRepository();
    }

    [HttpGet("api/Producto")]
    public ActionResult<List<Productos>> GetAllProducts() => Ok(productosRepository.GetAllProducts());

}