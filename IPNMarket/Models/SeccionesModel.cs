using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace IPNMarket.Models
{
    public class SeccionesModel
    {

        public int ID_Secciones { get; set; }
        public string Nombre { get; set; }

        public static List<SeccionesModel> CargarSecciones(string connectionString)
        {
            try
            {
                List<SeccionesModel> secciones = new List<SeccionesModel>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT ID_Secciones, Nombre FROM Secciones";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            SeccionesModel seccion = new SeccionesModel
                            {
                                ID_Secciones = Convert.ToInt32(reader["ID_Secciones"]),
                                Nombre = reader["Nombre"].ToString()
                            };
                            secciones.Add(seccion);
                        }
                    }
                }

                return secciones;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al cargar las secciones. Detalles: " + ex.Message);
            }
        }

        public SeccionesModel ObtenerSeccion(string connectionString, int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Verificar si el producto ya existe en el carrito del usuario
                    string existQuery = "SELECT Nombre FROM Secciones WHERE ID_Secciones = @ID_Secciones";
                    using (SqlCommand command = new SqlCommand(existQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ID_Secciones", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                SeccionesModel sec = new SeccionesModel
                                {
                                    Nombre = Convert.ToString(reader["Nombre"])
                                };

                                return sec;
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


    }
}
