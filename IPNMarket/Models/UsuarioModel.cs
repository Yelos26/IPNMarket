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
    public class UsuarioModel
    {
        public int ID_Usuario { get; set; }
        public string Nombre_Usuario { get; set; }
        public string Ap_Paterno { get; set; }
        public string Ap_Materno { get; set; }
        public string Correo { get; set; }
        public string Contraseña { get; set; }
        public DateTime Fecha_Reg { get; set; }
        public string Direccion { get; set; }
        public byte[] Imagen { get; set; }
        public int ID_PERFIL { get; set; }

        //Seleccionar todos los Usuarios

        public static List<UsuarioModel> CargarUsuarios(string connectionString)
        {
            try
            {
                List<UsuarioModel> usuarios = new List<UsuarioModel>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT ID_Usuario, Nombre_Usuario, Ap_Paterno, Ap_Materno, Correo, Fecha_Reg, Direccion, Imagen, ID_PERFIL FROM Usuario";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            if (reader["Imagen"] != DBNull.Value)
                            {
                                UsuarioModel usu = new UsuarioModel
                                {
                                    Imagen = (byte[])reader["Imagen"],
                                    ID_Usuario = Convert.ToInt32(reader["ID_Usuario"]),
                                    Nombre_Usuario = reader["Nombre_Usuario"].ToString(),
                                    Ap_Paterno = reader["Ap_Paterno"].ToString(),
                                    Ap_Materno = reader["Ap_Materno"].ToString(),
                                    Correo = reader["Correo"].ToString(),
                                    Fecha_Reg = reader["Fecha_Reg"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha_Reg"]) : DateTime.MinValue,
                                    Direccion = reader["Direccion"].ToString(),
                                    ID_PERFIL = Convert.ToInt32(reader["ID_PERFIL"]),
                                };
                                usuarios.Add(usu);
                            }
                            else
                            {
                                UsuarioModel usu = new UsuarioModel
                                {
                                    ID_Usuario = Convert.ToInt32(reader["ID_Usuario"]),
                                    Nombre_Usuario = reader["Nombre_Usuario"].ToString(),
                                    Ap_Paterno = reader["Ap_Paterno"].ToString(),
                                    Ap_Materno = reader["Ap_Materno"].ToString(),
                                    Correo = reader["Correo"].ToString(),
                                    Fecha_Reg = Convert.ToDateTime(reader["Fecha_Reg"]),
                                    Direccion = reader["Direccion"].ToString(),
                                    ID_PERFIL = Convert.ToInt32(reader["ID_PERFIL"]),
                                };
                                usuarios.Add(usu);
                            }
                        }
                    }
                }

                return usuarios;
            }
            catch (Exception ex)
            {
                throw new Exception("Ocurrió un error al cargar los productos. Detalles: " + ex.Message);
            }
        }


        //Verificar los correos
        public bool VerificarCorreo(string connectionString, string correo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT COUNT(*) FROM Usuario WHERE Correo = @Correo";
                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Correo", correo);

                        int count = Convert.ToInt32(command.ExecuteScalar());

                        // Si count es mayor que cero, significa que ya existe un usuario con ese correo.
                        return count > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }


        public void InsertarNuevoUsuario(string connectionString, string nombre, string Ap_Paterno, string Ap_Materno, string correo, string contraseña, string Dirección)
        {
            try
            {
                string nextID;
                int next;

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    //Ultimo ID de la BD
                    string Query = "SELECT MAX(ID_Usuario) FROM Usuario";
                    using (SqlCommand command = new SqlCommand(Query, conn))
                    {
                        object result = command.ExecuteScalar();

                        int maxID = Convert.ToInt32(result);
                        next = maxID + 1;
                        nextID = next.ToString();
                        ID_Usuario = next;
                        ID_PERFIL = 1;
                    }

                    string query = "INSERT INTO Usuario (ID_Usuario, Nombre_Usuario, Ap_Paterno, Ap_Materno, Correo, Contraseña, Fecha_Reg, Direccion, ID_PERFIL) " +
                                    "VALUES (@ID_Usuario, @Nombre_Usuario, @Ap_Paterno, @Ap_Materno, @Correo, ENCRYPTBYPASSPHRASE('pasword', @Contraseña), @Fecha_Reg, @Direccion, @ID_PERFIL)";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID_Usuario", int.Parse(nextID));
                        cmd.Parameters.AddWithValue("@Nombre_Usuario", nombre);
                        cmd.Parameters.AddWithValue("@Ap_Paterno", Ap_Paterno);
                        cmd.Parameters.AddWithValue("@Ap_Materno", Ap_Materno);
                        cmd.Parameters.AddWithValue("@Correo", correo);
                        cmd.Parameters.AddWithValue("@Contraseña", Encoding.UTF8.GetBytes(contraseña));
                        cmd.Parameters.AddWithValue("@Fecha_Reg", DateTime.Now);
                        cmd.Parameters.AddWithValue("@Direccion", Dirección);
                        cmd.Parameters.AddWithValue("@ID_PERFIL", 1);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores (puedes agregar registro de errores aquí)
                throw new Exception("Error al insertar un nuevo usuario.", ex);
            }
        }

        public UsuarioModel ObtenerUsuarioPorCredenciales(string connectionString, string email, string contraseña)
        {
            UsuarioModel usuario = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "SELECT ID_Usuario, Nombre_Usuario, Ap_Paterno, Ap_Materno, Correo, Direccion, ID_PERFIL, CONVERT(varchar(max), DECRYPTBYPASSPHRASE('pasword', Contraseña)) AS Contraseña_des FROM Usuario WHERE Correo = @Email AND (CONVERT(varchar(max), DECRYPTBYPASSPHRASE('pasword', Contraseña))) = @Contraseña";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        command.Parameters.AddWithValue("@Contraseña", contraseña);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                usuario = new UsuarioModel
                                {
                                    ID_PERFIL = Convert.ToInt32(reader["ID_PERFIL"]),
                                    ID_Usuario = Convert.ToInt32(reader["ID_Usuario"]),
                                    Nombre_Usuario = reader["Nombre_Usuario"].ToString(),
                                    Ap_Paterno = reader["Ap_Paterno"].ToString(),
                                    Ap_Materno = reader["Ap_Materno"].ToString(),
                                    Correo = reader["Correo"].ToString(),
                                    Contraseña = contraseña,
                                    Direccion = reader["Direccion"].ToString()
                                };
                            }
                        }
                    }
                }

                return usuario;
            }
            catch (Exception ex)
            {
                // Manejo de errores (puedes registrar el error)
                throw new Exception("Error al obtener el usuario por credenciales.", ex);
            }
        }

        public UsuarioModel ObtenerUsuarioPorID(string connectionString)
        {
            UsuarioModel usuario = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "SELECT Imagen, ID_Usuario, Nombre_Usuario, Ap_Paterno, Ap_Materno, Correo, Fecha_Reg, Direccion, ID_PERFIL FROM Usuario WHERE ID_Usuario = @ID_Usuario";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ID_Usuario", ID_Usuario);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read()) 
                            {
                                if (reader["Imagen"] != DBNull.Value)
                                {
                                    usuario = new UsuarioModel
                                    {
                                        Imagen = (byte[])reader["Imagen"],
                                        ID_PERFIL = Convert.ToInt32(reader["ID_PERFIL"]),
                                        ID_Usuario = Convert.ToInt32(reader["ID_Usuario"]),
                                        Nombre_Usuario = reader["Nombre_Usuario"].ToString(),
                                        Ap_Paterno = reader["Ap_Paterno"].ToString(),
                                        Ap_Materno = reader["Ap_Materno"].ToString(),
                                        Correo = reader["Correo"].ToString(),
                                        Fecha_Reg = reader["Fecha_Reg"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha_Reg"]) : DateTime.MinValue,
                                        Direccion = reader["Direccion"].ToString()
                                    };
                                }
                                else
                                {
                                    usuario = new UsuarioModel
                                    {
                                        ID_PERFIL = Convert.ToInt32(reader["ID_PERFIL"]),
                                        ID_Usuario = Convert.ToInt32(reader["ID_Usuario"]),
                                        Nombre_Usuario = reader["Nombre_Usuario"].ToString(),
                                        Ap_Paterno = reader["Ap_Paterno"].ToString(),
                                        Ap_Materno = reader["Ap_Materno"].ToString(),
                                        Correo = reader["Correo"].ToString(),
                                        Fecha_Reg = reader["Fecha_Reg"] != DBNull.Value ? Convert.ToDateTime(reader["Fecha_Reg"]) : DateTime.MinValue,
                                        Direccion = reader["Direccion"].ToString()
                                    };
                                }
                                    
                            }
                        }
                    }
                }

                return usuario;
            }
            catch (Exception ex)
            {
                // Manejo de errores (puedes registrar el error)
                throw new Exception("Error al obtener el usuario por credenciales.", ex);
            }
        }

        public UsuarioModel ObtenerIDPorEmail(string connectionString, string email)
        {
            UsuarioModel usuario = null;

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "SELECT ID_Usuario, Nombre_Usuario, Ap_Paterno, Ap_Materno, Correo, Direccion, ID_PERFIL, CONVERT(varchar(max), DECRYPTBYPASSPHRASE('pasword', Contraseña)) AS Contraseña_des FROM Usuario WHERE Correo = @Email";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                usuario = new UsuarioModel
                                {
                                    ID_PERFIL = Convert.ToInt32(reader["ID_PERFIL"]),
                                    ID_Usuario = Convert.ToInt32(reader["ID_Usuario"])
                                };
                            }
                        }
                    }
                }

                return usuario;
            }
            catch (Exception ex)
            {
                // Manejo de errores (puedes registrar el error)
                throw new Exception("Error al obtener el usuario por credenciales.", ex);
            }
        }

        public bool RecuperarContraseña(string connectionString, string email)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "SELECT CONVERT(varchar(max), DECRYPTBYPASSPHRASE('pasword', Contraseña)) AS Contraseña_des FROM Usuario WHERE Correo = @Email";
                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@Email", email);
                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            string contraseña = reader["Contraseña_des"].ToString();

                            using (MailMessage mail = new MailMessage())
                            {
                                mail.From = new MailAddress("PNImarket45@outlook.com");
                                mail.To.Add(email);
                                mail.Subject = "Recuperación de contraseña";
                                mail.Body = "Su contraseña es: " + contraseña;
                                mail.IsBodyHtml = true;
                                mail.Priority = MailPriority.High;

                                SmtpClient smtp = new SmtpClient();
                                smtp.Host = "smtp-mail.outlook.com";
                                smtp.Port = 587;
                                smtp.EnableSsl = true;
                                smtp.UseDefaultCredentials = false;
                                string sCorreo = "PNImarket45@outlook.com";
                                string sContraseña = "aeiou12345.";
                                smtp.Credentials = new System.Net.NetworkCredential(sCorreo, sContraseña);
                                smtp.Send(mail);
                            }
                            connection.Close();

                            return true;
                        }
                        else
                        {
                            connection.Close();
                            return false;
                        }
                    }
                }
            }
            catch
            {
                // Manejar cualquier error y registrar el error si es necesario
                return false;
            }
        }


        public bool ActualizarP(string connectionString, string ID, string nombre, string Ap_Paterno, string Ap_Materno, string email, string Dirección) 
        {

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "UPDATE Usuario SET Nombre_Usuario = @Nombre_Usuario, Ap_Paterno = @Ap_Paterno, Ap_Materno = @Ap_Materno, Direccion = @Direccion WHERE ID_Usuario = @ID_Usuario";

                    using (SqlCommand updateCmd = new SqlCommand(sqlQuery, connection))
                    {
                        updateCmd.Parameters.AddWithValue("@ID_Usuario", int.Parse(ID));
                        updateCmd.Parameters.AddWithValue("@Nombre_Usuario", nombre);
                        updateCmd.Parameters.AddWithValue("@Ap_Paterno", Ap_Paterno);
                        updateCmd.Parameters.AddWithValue("@Ap_Materno", Ap_Materno);
                        updateCmd.Parameters.AddWithValue("@Direccion", Dirección);

                        updateCmd.ExecuteNonQuery();

                        connection.Close();
                        return true;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public bool GuardarIMG(string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "UPDATE Usuario SET Imagen = @Imagen WHERE ID_Usuario = @ID_Usuario";

                    using (SqlCommand updateCmd = new SqlCommand(sqlQuery, connection))
                    {
                        updateCmd.Parameters.AddWithValue("@ID_Usuario", ID_Usuario);
                        updateCmd.Parameters.AddWithValue("@Imagen", Imagen);

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

        public bool ObtenerIMG(string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "SELECT Imagen FROM Usuario WHERE ID_Usuario = @ID_Usuario";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ID_Usuario", ID_Usuario);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                Imagen = (byte[])reader["Imagen"];
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

        public bool GuardarNuevoCorreo(string connectionString, string NuevCorreo)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "UPDATE Usuario SET Correo = @Correo WHERE ID_Usuario = @ID_Usuario";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID_Usuario", ID_Usuario);
                        cmd.Parameters.AddWithValue("@Correo", NuevCorreo);

                        cmd.ExecuteNonQuery();
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool GuardarNuevaContraseña(string connectionString, string contraseña2)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "UPDATE Usuario SET Contraseña = ENCRYPTBYPASSPHRASE('pasword', @Contraseña) WHERE ID_Usuario = @ID_Usuario";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@ID_Usuario", ID_Usuario);
                        cmd.Parameters.AddWithValue("@Contraseña", Encoding.UTF8.GetBytes(contraseña2));

                        cmd.ExecuteNonQuery();
                    }
                }
                return false;
            }
            catch
            {
                return false;
            }
        }

        public bool EliminarUsuario(string connectionString)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    using (SqlTransaction transaction = conn.BeginTransaction())
                    {
                        try
                        {
                            // Eliminar de la tabla Ventas
                            SqlCommand cmdVentas = new SqlCommand("DELETE FROM Ventas WHERE ID_Usuario = @ID_Usuario", conn, transaction);
                            cmdVentas.Parameters.AddWithValue("@ID_Usuario", ID_Usuario);
                            cmdVentas.ExecuteNonQuery();

                            // Eliminar de la tabla Carrito
                            SqlCommand cmdCarrito = new SqlCommand("DELETE FROM Carrito WHERE ID_Usuario = @ID_Usuario", conn, transaction);
                            cmdCarrito.Parameters.AddWithValue("@ID_Usuario", ID_Usuario);
                            cmdCarrito.ExecuteNonQuery();

                            // Eliminar de la tabla Pedido
                            SqlCommand cmdPedido = new SqlCommand("DELETE FROM Pedido WHERE ID_Usuario = @ID_Usuario", conn, transaction);
                            cmdPedido.Parameters.AddWithValue("@ID_Usuario", ID_Usuario);
                            cmdPedido.ExecuteNonQuery();

                            // Eliminar de la tabla Usuario
                            SqlCommand cmdUsuario = new SqlCommand("DELETE FROM Usuario WHERE ID_Usuario = @ID_Usuario", conn, transaction);
                            cmdUsuario.Parameters.AddWithValue("@ID_Usuario", ID_Usuario);
                            cmdUsuario.ExecuteNonQuery();

                            // Actualizar los IDs de los usuarios restantes en la tabla Usuario
                            SqlCommand cmdUpdateUsuario = new SqlCommand("UPDATE Usuario SET ID_Usuario = ID_Usuario - 1 WHERE ID_Usuario > @ID_Usuario", conn, transaction);
                            cmdUpdateUsuario.Parameters.AddWithValue("@ID_Usuario", ID_Usuario);
                            cmdUpdateUsuario.ExecuteNonQuery();

                            // Actualizar los IDs de los usuarios restantes en la tabla Ventas
                            SqlCommand cmdUpdateVentas = new SqlCommand("UPDATE Ventas SET ID_Usuario = ID_Usuario - 1 WHERE ID_Usuario > @ID_Usuario", conn, transaction);
                            cmdUpdateVentas.Parameters.AddWithValue("@ID_Usuario", ID_Usuario);
                            cmdUpdateVentas.ExecuteNonQuery();

                            // Actualizar los IDs de los usuarios restantes en la tabla Carrito
                            SqlCommand cmdUpdateCarrito = new SqlCommand("UPDATE Carrito SET ID_Usuario = ID_Usuario - 1 WHERE ID_Usuario > @ID_Usuario", conn, transaction);
                            cmdUpdateCarrito.Parameters.AddWithValue("@ID_Usuario", ID_Usuario);
                            cmdUpdateCarrito.ExecuteNonQuery();

                            // Actualizar los IDs de los usuarios restantes en la tabla Pedido
                            SqlCommand cmdUpdatePedido = new SqlCommand("UPDATE Pedido SET ID_Usuario = ID_Usuario - 1 WHERE ID_Usuario > @ID_Usuario", conn, transaction);
                            cmdUpdatePedido.Parameters.AddWithValue("@ID_Usuario", ID_Usuario);
                            cmdUpdatePedido.ExecuteNonQuery();

                            transaction.Commit();
                            return true;
                        }
                        catch
                        {
                            transaction.Rollback();
                            return false;
                        }
                    }
                }
            }
            catch
            {
                return false;
            }
        }
    }
}