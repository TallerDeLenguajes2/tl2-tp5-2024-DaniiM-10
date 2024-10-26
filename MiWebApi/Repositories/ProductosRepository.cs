using Microsoft.Data.Sqlite;
namespace TP5.Models;

public class ProductosRepository {
    private string ConnectionString = @"Data Source=db/Tienda.db;Cache=Shared";

    public ProductosRepository() {}

    public List<Productos> GetAllProducts() {
        List<Productos> productos = new List<Productos>();
        
        using (SqliteConnection connection = new SqliteConnection(ConnectionString))
        {
            string queryString = "SELECT * FROM Productos;";

            SqliteCommand command = new SqliteCommand(queryString, connection);
            connection.Open();
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Productos producto = new Productos();
                    producto.idProducto = Convert.ToInt32(reader["idProducto"]);
                    producto.descripcion = reader["Descripcion"].ToString();
                    producto.precio = Convert.ToInt32(reader["Precio"]);
                    productos.Add(producto);
                }
            }
            connection.Close();
        }

        return productos;
    }
}