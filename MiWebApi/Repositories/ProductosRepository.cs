using Microsoft.Data.Sqlite;
namespace TP5.Models;

public class ProductosRepository {
    private string ConnectionString = @"Data Source=db/Tienda.db;Cache=Shared";

    public ProductosRepository() {}

    public List<Productos> GetAllProducts() {
        List<Productos> products = new List<Productos>();
        
        using (SqliteConnection connection = new SqliteConnection(ConnectionString))
        {
            string queryString = @"SELECT * FROM Productos;";

            SqliteCommand command = new SqliteCommand(queryString, connection);
            connection.Open();
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Productos product = new Productos();
                    product.setIdProducto(Convert.ToInt32(reader["idProducto"]));
                    product.Descripcion = reader["Descripcion"].ToString();
                    product.Precio = Convert.ToInt32(reader["Precio"]);
                    products.Add(product);
                }
            }
            connection.Close();
        }

        return products;
    }

    public bool PostProduct(Productos product) {
        string queryString = @"INSERT INTO Productos (Descripcion, Precio) VALUES (@Descripcion, @Precio);";

        try {
            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@Descripcion", product.Descripcion);
                command.Parameters.AddWithValue("@Precio", product.Precio);
                command.ExecuteNonQuery();
                connection.Close();            
            }
            return true;
        } catch (Exception ex) { return false; }
    }

    public bool PutProduct(int idProduct, Productos product) {
        string queryString = @"UPDATE Productos SET Descripcion = @Descripcion, Precio = @Precio WHERE idProducto = @IdP;";

        try {

            using (SqliteConnection connection = new SqliteConnection(ConnectionString))
            {
                SqliteCommand command = new SqliteCommand(queryString, connection);
                connection.Open();
                command.Parameters.AddWithValue("@Descripcion", product.Descripcion);
                command.Parameters.AddWithValue("@Precio", product.Precio);
                command.Parameters.AddWithValue("@IdP", idProduct);
                command.ExecuteNonQuery();
                connection.Close();            
            }
            return true;
        } catch (Exception ex) { return false; }
    }

    public Productos GetProduct(int idProduct) {
        using (SqliteConnection connection = new SqliteConnection(ConnectionString)) {
            string queryString = @"SELECT * FROM Productos WHERE idProducto = @IdP;";
            
            SqliteCommand command = new SqliteCommand(queryString, connection);
            command.Parameters.AddWithValue("@IdP", idProduct);

            connection.Open();
            
            using (SqliteDataReader reader = command.ExecuteReader()) {
                if (reader.Read()) {
                    Productos product = new Productos();
                    product.setIdProducto(Convert.ToInt32(reader["idProducto"]));
                    product.Descripcion = reader["Descripcion"].ToString();
                    product.Precio = Convert.ToInt32(reader["Precio"]);
                    return product;
                }
            }
        }
        return null;
    }

    public bool DeleteProduct(int idProduct) {
        string queryString = @"DELETE FROM Productos WHERE idProducto = @IdP;";
        using (SqliteConnection connection = new SqliteConnection(ConnectionString)) {
        
            SqliteCommand command = new SqliteCommand(queryString, connection);
            connection.Open();
            command.Parameters.AddWithValue("@IdP", idProduct);

            int rowsAffected = command.ExecuteNonQuery();
            connection.Close();
            return rowsAffected > 0;
        }
    }
}