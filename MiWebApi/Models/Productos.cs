namespace TP5.Models;

public class Productos {
    private int idProductoPrivate;
    public int idProducto { get => idProductoPrivate; }
    public string? Descripcion { get; set; }
    public int Precio { get; set; }

    public void setIdProducto(int idP) {
        this.idProductoPrivate = idP;
    }
}