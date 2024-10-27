using Microsoft.Data.Sqlite;
namespace TP5.Models;

public class ProductosRepository {
    private string ConnectionString = @"Data Source=db/Tienda.db;Cache=Shared";

    public ProductosRepository() {}

    public List<Productos> GetAllProducts() {
        List<Productos> productos = new List<Productos>();
        
        using (SqliteConnection connection = new SqliteConnection(ConnectionString))
        {
            string queryString = @"SELECT * FROM Productos;";

            SqliteCommand command = new SqliteCommand(queryString, connection);
            connection.Open();
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Productos producto = new Productos();
                    producto.setIdProducto(Convert.ToInt32(reader["idProducto"]));
                    producto.Descripcion = reader["Descripcion"].ToString();
                    producto.Precio = Convert.ToInt32(reader["Precio"]);
                    productos.Add(producto);
                }
            }
            connection.Close();
        }

        return productos;
    }

    public bool PostProduct(Productos producto) {
        string queryString = @"INSERT INTO Productos (Descripcion, Precio) VALUES (@Descripcion, @Precio)";

        try {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                command.Parameters.AddWithValue("@Precio", producto.Precio);
                command.ExecuteNonQuery();
                connection.Close();            
            }
            return true;
        } catch (Exception ex) { return false; }
    }

    public bool PutProduct(int idProducto, Productos producto) {
        string queryString = @"UPDATE Productos SET Descripcion = @Descripcion, Precio = @Precio WHERE idProducto = @IdP";

        try {

            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                command.Parameters.AddWithValue("@Precio", producto.Precio);
                command.Parameters.AddWithValue("@IdP", idProducto);
                command.ExecuteNonQuery();
                connection.Close();            
            }
            return true;
        } catch (Exception ex) { return false; }
    }
}