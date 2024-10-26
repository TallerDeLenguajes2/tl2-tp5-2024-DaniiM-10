namespace TP5.Models;

public class Presupuestos {
    private List<PresupuestosDetalles> detalle;

    public Presupuestos() {
        this.detalle = new List<PresupuestosDetalles>();
    }

    public int idPresupuesto { get; set; }
    public string? NombreDestinatario { get; set; }
    public List<PresupuestosDetalles> Detalles { get => detalle; }
    public DateTime FechaCreacion { get; set; }

    public void AgregarProducto(Productos producto, int cantidad) {
        PresupuestosDetalles pd = new PresupuestosDetalles();
        pd.CargarProducto(producto);
        pd.cantidad = cantidad;
        detalle.Add(pd);
    }
}