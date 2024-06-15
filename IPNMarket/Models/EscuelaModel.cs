using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace IPNMarket.Models
{
    public class EscuelaModel
    {
        public int ID_Escuela { get; set; }
        public string Nombre { get; set; }
        public string Dirección { get; set; }

        public static List<EscuelaModel> CargarEscuelas(string connectionString)
        {
            try
            {
                List<EscuelaModel> escuelas = new List<EscuelaModel>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT ID_Escuela, Nombre, Dirección FROM Escuela";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            EscuelaModel esc = new EscuelaModel
                            {
                                ID_Escuela = Convert.ToInt32(reader["ID_Escuela"]),
                                Nombre = reader["Nombre"].ToString(),
                                Dirección = reader["Dirección"].ToString()
                            };
                            escuelas.Add(esc);
                        }
                    }
                }

                return escuelas;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al cargar los productos. Detalles: " + ex.Message);
            }
        }
    }
}
