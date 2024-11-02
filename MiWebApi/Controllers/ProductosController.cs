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
    public ActionResult<List<Productos>> GetAllProducts() => Ok(productosRepository.GetProductos());

    [HttpGet("api/Producto/{idP:int}")]
    public ActionResult<Productos> GetProductById(int idP) {
        Productos responseP = productosRepository.GetProducto(idP);
        return ((responseP != null) ? Ok(responseP) : NotFound(new { message = "Producto no encontrado :(" }));
    }

    [HttpPost("api/Producto")]
    public ActionResult PostProduct([FromBody] Productos producto) => (productosRepository.PostProducto(producto)) ? Created() : StatusCode(500);

    [HttpPut("/api/Producto/{IdP:int}")]
    public ActionResult PutProduct(int IdP, [FromBody] Productos producto) => (productosRepository.PutProducto(IdP, producto)) ? Ok() : StatusCode(500);

    [HttpDelete("api/Producto/{IdP:int}")]
    public ActionResult DeleteProduct(int IdP) => (productosRepository.DeleteProduct(IdP)) ? Ok() : StatusCode(500);
    
}