using System.Collections.Generic;
using System;
using System.Data.SqlClient;

namespace IPNMarket.Models
{
    public class VentasModel
    {
        public int ID_Ventas { get; set; }
        public int ID_Usuario { get; set; }
        public int ID_Pedido { get; set; }

        public static List<VentasModel> CargarVentas(string connectionString)
        {
            try
            {
                List<VentasModel> ventas = new List<VentasModel>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT ID_Ventas, ID_Usuario, ID_Pedido FROM Ventas";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            VentasModel venta = new VentasModel
                            {
                                ID_Ventas = Convert.ToInt32(reader["ID_Ventas"]),
                                ID_Usuario = Convert.ToInt32(reader["ID_Usuario"]),
                                ID_Pedido = Convert.ToInt32(reader["ID_Pedido"])
                            };
                            ventas.Add(venta);
                        }
                    }
                }

                return ventas;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al cargar las ventas. Detalles: " + ex.Message);
            }
        }

        public bool GuardarVenta(string connectionString)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Obtener el último ID de Pedido de la base de datos
                    string obtenerUltimoIdQuery = "SELECT MAX(ID_Ventas) FROM Ventas";
                    using (SqlCommand command = new SqlCommand(obtenerUltimoIdQuery, conn))
                    {
                        object result = command.ExecuteScalar();
                        int maxID = result != DBNull.Value ? Convert.ToInt32(result) : 0;
                        int nextID = maxID + 1;

                        string insertPedidoQuery = "INSERT INTO Ventas (ID_Ventas, ID_Usuario, ID_Pedido) VALUES (@ID_Pedido, @ID_Usuario, @ID_Pedido)";
                        using (SqlCommand insertCommand = new SqlCommand(insertPedidoQuery, conn))
                        {
                            insertCommand.Parameters.AddWithValue("@ID_Ventas", Convert.ToInt32(nextID));
                            insertCommand.Parameters.AddWithValue("@ID_Usuario", Convert.ToInt32(ID_Usuario));
                            insertCommand.Parameters.AddWithValue("@ID_Pedido", Convert.ToInt32(ID_Pedido));

                            insertCommand.ExecuteNonQuery();
                        }
                    }
                }

                return true;
            }
            catch { return false; }
        }
    }
}
