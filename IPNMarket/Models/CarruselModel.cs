using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using System.Data;

namespace IPNMarket.Models
{
    public class CarruselModel
    {
        public int ID_Carrusel { get; set; }
        public byte[] Imagen { get; set; }
        public string Controlador { get; set; }
        public string Accion { get; set; }

        public static List<CarruselModel> CargarCarrusel(string connectionString)
        {
            try
            {
                List<CarruselModel> carru = new List<CarruselModel>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT ID_Carrusel, Imagen, Controlador, Accion FROM Carrusel";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            if (reader["Imagen"] != DBNull.Value)
                            {
                                CarruselModel carr = new CarruselModel
                                {
                                    Imagen = (byte[])reader["Imagen"],
                                    ID_Carrusel = Convert.ToInt32(reader["ID_Carrusel"]),
                                    Controlador = reader["Controlador"].ToString(),
                                    Accion = reader["Accion"].ToString()
                                };
                                carru.Add(carr);
                            }
                            else
                            {
                                CarruselModel carr = new CarruselModel
                                {
                                    ID_Carrusel = Convert.ToInt32(reader["ID_Carrusel"]),
                                    Controlador = reader["Controlador"].ToString(),
                                    Accion = reader["Accion"].ToString()
                                };
                                carru.Add(carr);
                            }
                        }
                    }
                }

                return carru;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al cargar los productos. Detalles: " + ex.Message);
            }
        }

        public static CarruselModel ObtenerCarruselPorId(string connectionString, int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT Imagen, Controlador, Accion FROM Carrusel WHERE ID_Carrusel = @Id";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Id", id); // Corregir el nombre del parámetro

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                if (reader["Imagen"] != DBNull.Value)
                                {
                                    CarruselModel carr = new CarruselModel
                                    {
                                        Imagen = (byte[])reader["Imagen"],
                                        Controlador = reader["Controlador"].ToString(),
                                        Accion = reader["Accion"].ToString()
                                    };
                                    return carr;
                                }
                                else
                                {
                                    CarruselModel carr = new CarruselModel
                                    {
                                        Controlador = reader["Controlador"].ToString(),
                                        Accion = reader["Accion"].ToString()
                                    };
                                    return carr;
                                }
                            }
                        }
                    }
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public bool GuardarIMG(string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "UPDATE Carrusel SET Controlador = @Controlador, Accion = @Accion WHERE ID_Carrusel = @ID_Carrusel";

                    if (Imagen != null)
                    {
                        sqlQuery = "UPDATE Carrusel SET Imagen = @Imagen, Controlador = @Controlador, Accion = @Accion WHERE ID_Carrusel = @ID_Carrusel";
                    }

                    using (SqlCommand updateCmd = new SqlCommand(sqlQuery, connection))
                    {
                        updateCmd.Parameters.AddWithValue("@ID_Carrusel", ID_Carrusel);
                        if (Imagen != null)
                        {
                            updateCmd.Parameters.AddWithValue("@Imagen", Imagen);
                        }
                        updateCmd.Parameters.AddWithValue("@Controlador", Controlador);
                        updateCmd.Parameters.AddWithValue("@Accion", Accion);

                        updateCmd.ExecuteNonQuery();

                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Opcional: Loguear el mensaje de excepción ex.Message
                return false;
            }
        }

        public bool GuardarIMGcarrusel(string connectionString)
        {
            try
            {
                string nextID;
                int next;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    //Ultimo ID de la BD
                    string Query = "SELECT MAX(ID_Carrusel) FROM Carrusel";
                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        object result = command.ExecuteScalar();

                        int maxID = Convert.ToInt32(result);
                        next = maxID + 1;
                        nextID = next.ToString();
                        ID_Carrusel = next;
                    }


                    string sqlQuery = "INSERT INTO Carrusel (Imagen, ID_Carrusel, Controlador, Accion) VALUES (@Imagen, @ID_Carrusel, @Controlador, @Accion)";

                    using (SqlCommand insertCmd = new SqlCommand(sqlQuery, connection))
                    {
                        insertCmd.Parameters.AddWithValue("@ID_Carrusel", int.Parse(nextID));
                        insertCmd.Parameters.AddWithValue("@Controlador", Controlador);
                        insertCmd.Parameters.AddWithValue("@Accion", Accion);
                        insertCmd.Parameters.AddWithValue("@Imagen", Imagen);

                        insertCmd.ExecuteNonQuery();

                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public bool EliminarCarrusel(string connectionString)
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
                        cmd.CommandText = "DELETE FROM Carrusel WHERE ID_Carrusel = @ID_Carrusel";
                        cmd.Parameters.Add("@ID_Carrusel", System.Data.SqlDbType.Int).Value = ID_Carrusel;
                        cmd.ExecuteNonQuery();
                    }

                    using (SqlCommand cmdUpdateProducto = new SqlCommand())
                    {
                        // Actualizar IDs en Producto
                        cmdUpdateProducto.Connection = conn;
                        cmdUpdateProducto.CommandText = "UPDATE Carrusel SET ID_Carrusel = ID_Carrusel - 1 WHERE ID_Carrusel > @DeletedID";
                        cmdUpdateProducto.Parameters.Add("@DeletedID", System.Data.SqlDbType.Int).Value = ID_Carrusel;
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
