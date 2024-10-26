namespace TP5.Models;

public class PresupuestosDetalles {
    private Productos producto1;
    public PresupuestosDetalles() {
        this.producto1 = new Productos();
    }

    public Productos producto { get => producto; }
    public int cantidad { get; set; }

    public void CargarProducto(Productos producto) {
        this.producto1 = producto;
    }
}