using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace IPNMarket.Models
{
    public class TarjetaModel
    {
        public int ID_Tarjeta { get; set; }
        public string Numero_Tarjeta { get; set; }
        public int Mes { get; set; }
        public int Año { get; set; }
        public int CVC { get; set; }
        public string Nombre_Titular { get; set; }

        public bool VerificarTarjeta(string connectionString)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT Nombre_Titular FROM Tarjeta WHERE Numero_Tarjeta = @Numero_Tarjeta AND Mes = @Mes AND Año = @Año AND CVC = @CVC";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Numero_Tarjeta", Numero_Tarjeta);
                        command.Parameters.AddWithValue("@Mes", Mes);
                        command.Parameters.AddWithValue("@Año", Año);
                        command.Parameters.AddWithValue("@CVC", CVC);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                this.Nombre_Titular = reader["Nombre_Titular"].ToString();
                                return true;
                            }
                        }
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}
