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

    [HttpGet("api/")]
    public ActionResult<List<Productos>> GetAllProducts()
    {
        var productos = productosRepository.GetProductos();
        
        if (productos == null || !productos.Any()) { return NotFound(new { message = "No se encontraron productos." }); }

        return Ok(productos);
    }

    [HttpGet("api/{idP:int}")]
    public ActionResult<Productos> GetProductById(int idP)
    {
        if (idP <= 0) return BadRequest(new { message = "ID de producto inválido." });

        var producto = productosRepository.GetProducto(idP);
        
        if (producto == null) { return NotFound(new { message = "Producto no encontrado." }); }

        return Ok(producto);
    }

    [HttpPost("api/")]
    public ActionResult PostProduct([FromBody] Productos producto)
    {
        if (producto == null || string.IsNullOrWhiteSpace(producto.Descripcion) || producto.Precio <= 0) { return BadRequest(new { message = "Datos de producto inválidos." }); }

        var success = productosRepository.PostProducto(producto);

        return success 
            ? Created(string.Empty, new { message = "Producto creado con éxito." }) 
            : StatusCode(500, new { message = "Error al crear el producto." });
    }

    [HttpPut("api/{IdP:int}")]
    public ActionResult PutProduct(int IdP, [FromBody] Productos producto)
    {
        if (IdP <= 0 || producto == null || string.IsNullOrWhiteSpace(producto.Descripcion) || producto.Precio <= 0) { return BadRequest(new { message = "Datos de producto inválidos." }); }

        var success = productosRepository.PutProducto(IdP, producto);
        return success ? Ok(new { message = "Producto actualizado con éxito." })
                       : StatusCode(500, new { message = "Error al actualizar el producto." });
    }

    [HttpDelete("api/{IdP:int}")]
    public ActionResult DeleteProduct(int IdP)
    {
        if (IdP <= 0) return BadRequest(new { message = "ID de producto inválido." });

        var success = productosRepository.DeleteProducto(IdP);
        return success ? Ok(new { message = "Producto eliminado con éxito." })
                       : StatusCode(500, new { message = "Error al eliminar el producto o producto no encontrado." });
    }
}