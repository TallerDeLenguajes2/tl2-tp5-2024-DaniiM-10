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

    [HttpGet("api/{IdProducto:int}")]
    public ActionResult<Productos> GetProductById(int IdProducto)
    {
        if (IdProducto <= 0) return BadRequest(new { message = "ID de producto inválido." });

        var producto = productosRepository.GetProducto(IdProducto);
        
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

    [HttpPut("api/{IdProducto:int}")]
    public ActionResult PutProduct(int IdProducto, [FromBody] Productos producto)
    {
        if (IdProducto <= 0 || producto == null || string.IsNullOrWhiteSpace(producto.Descripcion) || producto.Precio <= 0) { return BadRequest(new { message = "Datos de producto inválidos." }); }

        var success = productosRepository.PutProducto(IdProducto, producto);
        return success ? Ok(new { message = "Producto actualizado con éxito." })
                       : StatusCode(500, new { message = "Error al actualizar el producto." });
    }

    [HttpDelete("api/{IdProducto:int}")]
    public ActionResult DeleteProduct(int IdProducto)
    {
        if (IdProducto <= 0) return BadRequest(new { message = "ID de producto inválido." });

        var success = productosRepository.DeleteProducto(IdProducto);
        return success ? Ok(new { message = "Producto eliminado con éxito." })
                       : StatusCode(500, new { message = "Error al eliminar el producto o producto no encontrado." });
    }
}