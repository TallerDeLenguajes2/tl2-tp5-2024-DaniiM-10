using Microsoft.Data.Sqlite;
namespace TP5.Models;

public class ProductosRepository
{
    private string ConnectionString = @"Data Source=db/Tienda.db;Cache=Shared";
    private PresupuestosRepository presupuestosRepository;

    public ProductosRepository() {
        presupuestosRepository = new PresupuestosRepository();
    }

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
                command.Parameters.AddWithValue("@Descripcion", producto.Descripcion);
                command.Parameters.AddWithValue("@Precio", producto.Precio);
                command.Parameters.AddWithValue("@IdP", idProducto);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery(); // Obtiene el número de filas afectadas

                // Retorna true solo si se actualizó al menos una fila
                return rowsAffected > 0;
            }
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
        bool productosDetalleCambio = presupuestosRepository.ExisteIdProdEnPresupuestosDetalle(idProducto);

        using (SqliteConnection connection = new SqliteConnection(ConnectionString))
        {
            connection.Open();
            using (var transaction = connection.BeginTransaction())
            {
                try
                {
                    if (productosDetalleCambio) {
                        string queryStringPD = @"UPDATE ProductosDetalle SET idProducto = NULL WHERE idProducto = @IdP;";
                        using (SqliteCommand updateCommand = new SqliteCommand(queryStringPD, connection, transaction))
                        {
                            updateCommand.Parameters.AddWithValue("@IdP", idProducto);
                            updateCommand.ExecuteNonQuery();
                        }
                    }

                    string queryString = @"DELETE FROM Productos WHERE idProducto = @IdP;";
                    using (SqliteCommand deleteCommand = new SqliteCommand(queryString, connection, transaction))
                    {
                        deleteCommand.Parameters.AddWithValue("@IdP", idProducto);
                        int rowsAffected = deleteCommand.ExecuteNonQuery();
                        
                        if (rowsAffected > 0)
                        {
                            transaction.Commit();
                            return true;
                        }
                        else
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                }
                catch (Exception ex)
                {
                    transaction.Rollback(); // Revertir la transacción en caso de error
                    Console.WriteLine($"Error en DeleteProducto: {ex.Message}");
                    return false;
                }
            }
        }
    }
}