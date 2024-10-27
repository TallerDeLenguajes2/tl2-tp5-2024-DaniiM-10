namespace TP5.Models;

public class Presupuestos {
    private List<PresupuestosDetalles> DetallesPrivate;

    public Presupuestos() {
        this.DetallesPrivate = new List<PresupuestosDetalles>();
    }

    public int idPresupuesto { get; set; }
    public string? NombreDestinatario { get; set; }
    public List<PresupuestosDetalles> Detalles { get => DetallesPrivate; }
    public DateTime FechaCreacion { get; set; }

    public void AgregarProducto(Productos producto, int cantidad) {
        PresupuestosDetalles presupuestosDetalles = new PresupuestosDetalles();
        presupuestosDetalles.CargarProducto(producto);
        presupuestosDetalles.cantidad = cantidad;
        DetallesPrivate.Add(presupuestosDetalles);
    }
}