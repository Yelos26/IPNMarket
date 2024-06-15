using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace IPNMarket.Models
{
    public class CAT_PERFILModel
    {
        public int ID_PERFIL { get; set; }
        public string DESCRIPCIÓN { get; set; }

        public static List<CAT_PERFILModel> CargarPerfiles(string connectionString)
        {
            try
            {
                List<CAT_PERFILModel> catalogo = new List<CAT_PERFILModel>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT ID_PERFIL, DESCRIPCIÓN FROM CAT_PERFIL";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            CAT_PERFILModel cat = new CAT_PERFILModel
                            {
                                ID_PERFIL = Convert.ToInt32(reader["ID_PERFIL"]),
                                DESCRIPCIÓN = reader["DESCRIPCIÓN"].ToString()
                            };
                            catalogo.Add(cat);
                        }
                    }
                }

                return catalogo;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al cargar los perfiles. Detalles: " + ex.Message);
            }
        }
    }
}