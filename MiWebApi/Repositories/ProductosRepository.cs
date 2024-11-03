using Microsoft.Data.Sqlite;
namespace TP5.Models;

public class ProductosRepository
{
    private string ConnectionString = @"Data Source=db/Tienda.db;Cache=Shared";

    public ProductosRepository() {}

    public List<Productos> GetProductos()
    {
        List<Productos> productos = new List<Productos>();

        try
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                string queryString = @"SELECT * FROM Productos;";

                SqliteCommand command = new SqliteCommand(queryString, connection);
                connection.Open();
                using (SqliteDataReader reader = command.ExecuteReader())
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
            }    
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en GetProductos: {ex.Message}");
        }

        return productos;
    }

    public bool PostProducto(Productos producto)
    {
        string queryString = @"INSERT INTO Productos (Descripcion, Precio) 
        VALUES (@Descripcion, @Precio);";

        try
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                command.Parameters.AddWithValue("@Precio", producto.Precio);
                command.ExecuteNonQuery();
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en PostProducto: {ex.Message}");
            return false;
        }
    }

    public bool PutProducto(int idProducto, Productos producto)
    {
        string queryString = @"UPDATE Productos SET Descripcion = @Descripcion, Precio = @Precio 
        WHERE idProducto = @IdP;";

        try
        {

            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                command.Parameters.AddWithValue("@Precio", producto.Precio);
                command.Parameters.AddWithValue("@IdP", idProducto);
                command.ExecuteNonQuery();
            }
            return true;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en PutProducto: {ex.Message}");
            return false;
        }
    }

    public Productos GetProducto(int idProducto)
    {
        try
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                string queryString = @"SELECT * FROM Productos 
                WHERE idProducto = @IdP;";

                SqliteCommand command = new SqliteCommand(queryString, connection);
                command.Parameters.AddWithValue("@IdP", idProducto);

                connection.Open();

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Productos producto = new Productos();
                        producto.setIdProducto(Convert.ToInt32(reader["idProducto"]));
                        producto.Descripcion = reader["Descripcion"].ToString();
                        producto.Precio = Convert.ToInt32(reader["Precio"]);
                        return producto;
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en GetProducto: {ex.Message}");
        }

        return null;
    }

    public bool DeleteProducto(int idProducto)
    {
        string queryString = @"DELETE FROM Productos 
        WHERE idProducto = @IdP;";
        
        try
        {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@IdP", idProducto);

                int rowsAffected = command.ExecuteNonQuery();
                return rowsAffected > 0;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error en DeleteProducto: {ex.Message}");
            return false;
        }
    }
}