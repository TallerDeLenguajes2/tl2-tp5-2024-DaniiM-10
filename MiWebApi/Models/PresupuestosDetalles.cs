namespace TP5.Models;

public class PresupuestosDetalles {
    private Productos productoPrivate;
    public PresupuestosDetalles() {
        this.productoPrivate = new Productos();
    }

    public Productos producto { get => productoPrivate; }
    public int cantidad { get; set; }

    public void CargarProducto(Productos producto) {
        this.productoPrivate = producto;
    }
}