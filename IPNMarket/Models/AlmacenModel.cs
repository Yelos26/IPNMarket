using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;

namespace IPNMarket.Models
{
    public class AlmacenModel
    {

        public int ID_Almacen { get; set; }
        public int Cantidad { get; set; }
        public int Precio_Total { get; set; }
        public int ID_Producto { get; set; }

        public static List<AlmacenModel> CargarAlmacen(string connectionString)
        {
            try
            {
                List<AlmacenModel> almacen = new List<AlmacenModel>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT ID_Almacen, Cantidad, Precio_Total, ID_Producto FROM Almacen";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            AlmacenModel item = new AlmacenModel
                            {
                                ID_Almacen = Convert.ToInt32(reader["ID_Almacen"]),
                                Cantidad = Convert.ToInt32(reader["Cantidad"]),
                                Precio_Total = Convert.ToInt32(reader["Precio_Total"]),
                                ID_Producto = Convert.ToInt32(reader["ID_Producto"])
                            };
                            almacen.Add(item);
                        }
                    }
                }

                return almacen;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al cargar el almacén. Detalles: " + ex.Message);
            }
        }

        public AlmacenModel ObtenerAlmacenID(string connectionString, int id)
        {
            try
            {
                AlmacenModel almacen = null;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string query = "SELECT ID_Almacen, Cantidad, Precio_Total FROM Almacen WHERE ID_Producto = @ID_Producto";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@ID_Producto", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                almacen = new AlmacenModel
                                {
                                    ID_Almacen = Convert.ToInt32(reader["ID_Almacen"]),
                                    Cantidad = Convert.ToInt32(reader["Cantidad"]),
                                    Precio_Total = Convert.ToInt32(reader["Precio_Total"]),
                                    ID_Producto = id
                                };
                            }
                        }
                    }
                }
                return almacen;
            }
            catch
            {
                return null;
            }
        }


        public bool Actualizar(string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "UPDATE Almacen SET Cantidad = @Cantidad WHERE ID_Almacen = @ID_Almacen";

                    using (SqlCommand updateCmd = new SqlCommand(sqlQuery, connection))
                    {
                        updateCmd.Parameters.AddWithValue("@ID_Almacen", ID_Almacen);
                        updateCmd.Parameters.AddWithValue("@Cantidad", Cantidad);

                        updateCmd.ExecuteNonQuery();

                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public bool ActualizarIDP(string connectionString, int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "UPDATE Almacen SET Cantidad = @Cantidad, Precio_Total = @Precio_Total WHERE ID_Producto = @ID_Producto";

                    using (SqlCommand updateCmd = new SqlCommand(sqlQuery, connection))
                    {
                        updateCmd.Parameters.AddWithValue("@ID_Producto", id);
                        updateCmd.Parameters.AddWithValue("@Cantidad", Cantidad);
                        updateCmd.Parameters.AddWithValue("@Precio_Total", Precio_Total);

                        updateCmd.ExecuteNonQuery(); 

                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public bool GuardarEnAlmacen(string connectionString)
        {

            try
            {
                string nextID;
                int next;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    //Ultimo ID de la BD
                    string Query = "SELECT MAX(ID_Almacen) FROM Almacen";
                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        object result = command.ExecuteScalar();

                        int maxID = Convert.ToInt32(result);
                        next = maxID + 1;
                        nextID = next.ToString();
                        ID_Almacen = next;
                    }


                    string sqlQuery = "INSERT INTO Almacen (ID_Almacen, Cantidad, Precio_Total, ID_Producto) VALUES (@ID_Almacen, @Cantidad, @Precio_Total, @ID_Producto)";

                    using (SqlCommand insertCmd = new SqlCommand(sqlQuery, connection))
                    {
                        insertCmd.Parameters.AddWithValue("@ID_Almacen", int.Parse(nextID));
                        insertCmd.Parameters.AddWithValue("@Cantidad", Cantidad);
                        insertCmd.Parameters.AddWithValue("@Precio_Total", Precio_Total);
                        insertCmd.Parameters.AddWithValue("@ID_Producto", ID_Producto);

                        insertCmd.ExecuteNonQuery();

                        return true;
                    }
                }
            }
            catch { return false; }

        }

        public bool EliminarProducto(string connectionString)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        // Eliminar registros relacionados en Almacen
                        cmd.Connection = conn;
                        cmd.CommandText = "DELETE FROM Almacen WHERE ID_Producto = @ID_Producto";
                        cmd.Parameters.Add("@ID_Producto", System.Data.SqlDbType.Int).Value = ID_Producto;
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmd = new SqlCommand())
                    {
                        // Eliminar producto
                        cmd.Connection = conn;
                        cmd.CommandText = "DELETE FROM Producto WHERE ID_Producto = @ID_Producto";
                        cmd.Parameters.Add("@ID_Producto", System.Data.SqlDbType.Int).Value = ID_Producto;
                        cmd.ExecuteNonQuery();
                    }

                    //using (SqlCommand cmdUpdateAlmacen = new SqlCommand())
                    //{
                    //    // Actualizar IDs en Almacen
                    //    cmdUpdateAlmacen.Connection = conn;
                    //    cmdUpdateAlmacen.CommandText = "UPDATE Almacen SET ID_Almacen = ID_Almacen - 1 WHERE ID_Almacen > @DeletedID";
                    //    cmdUpdateAlmacen.Parameters.Add("@DeletedID", System.Data.SqlDbType.Int).Value = ID_Producto;
                    //    cmdUpdateAlmacen.ExecuteNonQuery();
                    //}

                    using (SqlCommand cmdUpdateAlmacen = new SqlCommand())
                    {
                        cmdUpdateAlmacen.Connection = conn;
                        cmdUpdateAlmacen.CommandText = @"
                    UPDATE Almacen 
                    SET 
                        ID_Almacen = ID_Almacen - 1,
                        ID_Producto = CASE 
                                            WHEN ID_Producto > @DeletedID THEN ID_Producto - 1
                                            ELSE ID_Producto 
                                        END 
                    WHERE 
                        ID_Almacen > @DeletedID";
                        cmdUpdateAlmacen.Parameters.Add("@DeletedID", System.Data.SqlDbType.Int).Value = ID_Producto;
                        cmdUpdateAlmacen.ExecuteNonQuery();
                    }

                    using (SqlCommand cmdUpdateProducto = new SqlCommand())
                    {
                        // Actualizar IDs en Producto
                        cmdUpdateProducto.Connection = conn;
                        cmdUpdateProducto.CommandText = "UPDATE Producto SET ID_Producto = ID_Producto - 1 WHERE ID_Producto > @DeletedID";
                        cmdUpdateProducto.Parameters.Add("@DeletedID", System.Data.SqlDbType.Int).Value = ID_Producto;
                        cmdUpdateProducto.ExecuteNonQuery();
                    }

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
}
