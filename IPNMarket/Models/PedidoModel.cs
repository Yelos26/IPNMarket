using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace IPNMarket.Models
{
    public class PedidoModel
    {
        public int ID_Pedido { get; set; }
        public int Cantidad { get; set; }
        public int Costo_Total { get; set; }
        public DateTime Fecha_Pedido { get; set; }
        public int ID_Almacen { get; set; }
        public int ID_Usuario { get; set; }
        
        /* PRODUCTOS */
        public byte[] Imagen { get; set; }
        public string Nombre_Producto { get; set; }
        public decimal Costo_Unitario { get; set; }


        public static List<PedidoModel> CargarPedidos(string connectionString)
        {
            try
            {
                List<PedidoModel> pedidos = new List<PedidoModel>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT ID_Pedido, Cantidad, Costo_Total, Fecha_Pedido, ID_Usuario FROM Pedido";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            PedidoModel ped = new PedidoModel
                            {
                                ID_Pedido = Convert.ToInt32(reader["ID_Pedido"]),
                                Cantidad = Convert.ToInt32(reader["Cantidad"]),
                                Costo_Total = Convert.ToInt32(reader["Costo_Total"]),
                                Fecha_Pedido = reader["Fecha_Pedido"] != DBNull.Value ? reader.GetDateTime(reader.GetOrdinal("Fecha_Pedido")) : DateTime.MinValue,
                                ID_Usuario = Convert.ToInt32(reader["ID_Usuario"]),
                            };
                            pedidos.Add(ped);
                        }
                    }
                }

                return pedidos;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al cargar los pedidos. Detalles: " + ex.Message);
            }
        }

        public bool GuardarPedido(List<int> cantidades, List<int> costosTotales, List<int> idAlmacenes, int idUsuario, List<int> idcarrito, List<int> Sub_Total, string connectionString)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    // Obtener el último ID de Pedido de la base de datos
                    string obtenerUltimoIdQuery = "SELECT MAX(ID_Pedido) FROM Pedido";
                    using (SqlCommand command = new SqlCommand(obtenerUltimoIdQuery, conn))
                    {
                        object result = command.ExecuteScalar();
                        int maxID = result != DBNull.Value ? Convert.ToInt32(result) : 0;
                        int nextID = maxID + 1;

                        // Insertar cada pedido en la base de datos
                        for (int i = 0; i < cantidades.Count; i++)
                        {
                            int currentID = nextID++; // Incrementa antes de usar
                            string insertPedidoQuery = "INSERT INTO Pedido (ID_Pedido, Cantidad, Costo_Total, Fecha_Pedido, ID_Almacen, ID_Usuario) VALUES (@ID_Pedido, @Cantidad, @Costo_Total, @Fecha_Pedido, @ID_Almacen, @ID_Usuario)";
                            using (SqlCommand insertCommand = new SqlCommand(insertPedidoQuery, conn))
                            {
                                insertCommand.Parameters.AddWithValue("@ID_Pedido", currentID);
                                insertCommand.Parameters.AddWithValue("@Cantidad", cantidades[i]);
                                insertCommand.Parameters.AddWithValue("@Costo_Total", costosTotales[i]);
                                insertCommand.Parameters.AddWithValue("@Fecha_Pedido", DateTime.Now);
                                insertCommand.Parameters.AddWithValue("@ID_Almacen", idAlmacenes[i]);
                                insertCommand.Parameters.AddWithValue("@ID_Usuario", idUsuario);

                                insertCommand.ExecuteNonQuery();

                                VentasModel ven = new VentasModel
                                {
                                    ID_Usuario = idUsuario,
                                    ID_Pedido = currentID
                                };
                                ven.GuardarVenta(connectionString);
                            }
                        }
                    }
                }

                return true;
            }
            catch { return false; }
        }

        public static List<PedidoModel> CargarPedidosUsuario(string connectionString, int idusuario)
        {
            try
            {
                List<PedidoModel> pedidos = new List<PedidoModel>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT pr.Imagen, pr.Nombre_Producto, pr.Costo_Unitario, pe.Cantidad, pe.Costo_Total, pe.Fecha_Pedido FROM Pedido pe JOIN Almacen a ON pe.ID_Almacen = a.ID_Almacen JOIN Producto pr ON a.ID_Producto = pr.ID_Producto WHERE pe.ID_Usuario = @ID_Usuario;";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@ID_Usuario", idusuario);

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            if (reader["Imagen"] != DBNull.Value)
                            {
                                PedidoModel ped = new PedidoModel
                                {
                                    Imagen = (byte[])reader["Imagen"],
                                    Nombre_Producto = reader["Nombre_Producto"].ToString(),
                                    Costo_Unitario = reader.GetDecimal(reader.GetOrdinal("Costo_Unitario")),
                                    Cantidad = Convert.ToInt32(reader["Cantidad"]),
                                    Costo_Total = Convert.ToInt32(reader["Costo_Total"]),
                                    Fecha_Pedido = reader["Fecha_Pedido"] != DBNull.Value ? reader.GetDateTime(reader.GetOrdinal("Fecha_Pedido")) : DateTime.MinValue
                                };
                                pedidos.Add(ped);
                            }
                            else
                            {
                                PedidoModel ped = new PedidoModel
                                {
                                    Nombre_Producto = reader["Nombre_Producto"].ToString(),
                                    Costo_Unitario = reader.GetDecimal(reader.GetOrdinal("Costo_Unitario")),
                                    Cantidad = Convert.ToInt32(reader["Cantidad"]),
                                    Costo_Total = Convert.ToInt32(reader["Costo_Total"]),
                                    Fecha_Pedido = reader["Fecha_Pedido"] != DBNull.Value ? reader.GetDateTime(reader.GetOrdinal("Fecha_Pedido")) : DateTime.MinValue

                                };
                                pedidos.Add(ped);
                            }
                                
                        }
                    }
                }

                return pedidos;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al cargar los pedidos. Detalles: " + ex.Message);
            }
        }

        public PedidoModel ObtenerPedidoPorID(string connectionString)
        {
            PedidoModel ped = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "SELECT ID_Pedido, Cantidad, Costo_Total, Fecha_Pedido, ID_Almacen, ID_Usuario FROM Pedido WHERE ID_Pedido = @ID_Pedido";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ID_Pedido", ID_Pedido);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                ped = new PedidoModel
                                {
                                    ID_Pedido = Convert.ToInt32(reader["ID_Pedido"]),
                                    Cantidad = Convert.ToInt32(reader["Cantidad"]),
                                    Costo_Total = Convert.ToInt32(reader["Costo_Total"]),
                                    Fecha_Pedido = reader["Fecha_Pedido"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha_Pedido"]) : DateTime.MinValue,
                                    ID_Almacen = Convert.ToInt32(reader["ID_Almacen"]),
                                    ID_Usuario = Convert.ToInt32(reader["ID_Usuario"])
                                };
                            }
                        }
                    }
                }

                return ped;
            }
            catch
            {
                return null;
            }
        }
    }
}
