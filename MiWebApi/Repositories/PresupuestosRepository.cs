using Microsoft.Data.Sqlite;
namespace TP5.Models;

public class PresupuestosRepository {
    private string ConnectionString = @"Data Source=db/Tienda.db;Cache=Shared";

    public PresupuestosRepository() {}
    public List<Presupuestos> GetPresupuestos() {
        //asdasdasda 
        List<Presupuestos> presupuestos = new List<Presupuestos>();

        using (SqliteConnection connection = new SqliteConnection(ConnectionString))
        {
            string queryString = @"SELECT * FROM Presupuestos;";

            SqliteCommand command = new SqliteCommand(queryString, connection);
            connection.Open();
            using(SqliteDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    Presupuestos presupuesto = new Presupuestos();
                    presupuesto.setIdPresupuesto(Convert.ToInt32(reader["idPresupuesto"]));
                    presupuesto.NombreDestinatario = reader["NombreDestinatario"].ToString();
                    presupuesto.FechaCreacion = Convert.ToDateTime(reader["FechaCreacion"]);

                    presupuesto.setDetallesPresupuesto(GetPresupuestosDetalles(Convert.ToInt32(reader["idPresupuesto"])));

                    presupuestos.Add(presupuesto);
                }
            }
            connection.Close();
        }

        return presupuestos;
    }

    private List<PresupuestosDetalles> GetPresupuestosDetalles(int id) {
        List<PresupuestosDetalles> pdList = new List<PresupuestosDetalles>();

        using (SqliteConnection connection = new SqliteConnection(ConnectionString)) {
            string queryString = @"SELECT 
                Productos.idProducto,
                Productos.Descripcion,
                Productos.Precio,
                PresupuestosDetalle.Cantidad
            FROM 
                Presupuestos
            LEFT JOIN 
                PresupuestosDetalle ON Presupuestos.idPresupuesto = PresupuestosDetalle.idPresupuesto
            LEFT JOIN 
                Productos ON PresupuestosDetalle.idProducto = Productos.idProducto
            WHERE 
                Presupuestos.idPresupuesto = @idPr;";

            using (SqliteCommand command = new SqliteCommand(queryString, connection)) {
                command.Parameters.AddWithValue("@idPr", id);
                connection.Open();
                using (SqliteDataReader reader = command.ExecuteReader()) {
                    while (reader.Read()) {
                        PresupuestosDetalles pd = new PresupuestosDetalles();

                        if(reader.IsDBNull(reader.GetOrdinal("idProducto"))) {
                            return pdList;
                        }

                        Productos product = new Productos();
                        product.setIdProducto(Convert.ToInt32(reader["idProducto"]));
                        product.Descripcion = reader["Descripcion"].ToString();
                        product.Precio = Convert.ToInt32(reader["Precio"]);

                        pd.CargarProducto(product);
                        pd.cantidad = Convert.ToInt32(reader["Cantidad"]);

                        pdList.Add(pd);
                    }
                }
            }
        }
        return pdList;
    }

}