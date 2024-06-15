using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Net.Mail;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static System.Net.Mime.MediaTypeNames;

namespace IPNMarket.Models
{
    public class CarritoModel
    {
        public int ID { get; set; }
        public int ID_Carrito { get; set; }
        public byte[] Imagen { get; set; }
        public string Descripción { get; set; }
        public int Cantidad { get; set; }
        public int Precio { get; set; }
        public int Sub_Total { get; set; }
        public int Costo_Total { get; set; }
        public DateTime Fecha_Pedido { get; set; }
        public DateTime Fecha_Cad { get; set; }
        public int ID_Almacen { get; set; }
        public int ID_Usuario { get; set; }
        public int ID_Producto { get; set; }

        public static List<CarritoModel> CargarPedido(string connectionString, int ID)
        {
            try
            {

                List<CarritoModel> productos = new List<CarritoModel>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT ID_Carrito, Cantidad, Costo_Total, ID_Almacen FROM Carrito WHERE ID_Usuario = @ID_Usuario";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@ID_Usuario", ID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                CarritoModel producto = new CarritoModel
                                {
                                    ID_Carrito = Convert.ToInt32(reader["ID_Carrito"]),
                                    Cantidad = Convert.ToInt32(reader["Cantidad"]),
                                    Costo_Total = Convert.ToInt32(reader["Costo_Total"]),
                                    ID_Almacen = Convert.ToInt32(reader["ID_Almacen"])
                                };
                            }
                        }
                    }
                }

                return productos;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar pedido: " + ex.Message);

                // O puedes lanzar una nueva excepción con un mensaje personalizado:
                throw new Exception("Ocurrió un error al cargar el pedido. Detalles: " + ex.Message);
            }
        }



        public bool GuardarPedido(string connectionString, int id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    // Verificar si el producto ya existe en el carrito del usuario
                    string existQuery = "SELECT COUNT(*) FROM Carrito WHERE ID_Usuario = @ID_Usuario AND ID_Producto = @ID_Producto";
                    using (SqlCommand existCommand = new SqlCommand(existQuery, connection))
                    {
                        existCommand.Parameters.AddWithValue("@ID_Usuario", ID_Usuario);
                        existCommand.Parameters.AddWithValue("@ID_Producto", id);
                        int existingCount = (int)existCommand.ExecuteScalar();

                        if (existingCount > 0)
                        {
                            // Si el producto ya existe, actualiza la cantidad, el subtotal y el costo total

                            string updateQuery;

                            if (Imagen != null)
                            {
                                updateQuery = "UPDATE Carrito SET Imagen = @Imagen, Cantidad = Cantidad + @Cantidad, Sub_Total = Sub_Total + @Sub_Total, Costo_Total = Costo_Total + @Sub_Total WHERE ID_Usuario = @ID_Usuario AND ID_Producto = @ID_Producto";
                            }
                            else
                            {
                                updateQuery = "UPDATE Carrito SET Cantidad = Cantidad + @Cantidad, Sub_Total = Sub_Total + @Sub_Total, Costo_Total = Costo_Total + @Sub_Total WHERE ID_Usuario = @ID_Usuario AND ID_Producto = @ID_Producto";
                            }
                                
                            using (SqlCommand updateCommand = new SqlCommand(updateQuery, connection))
                            {
                                if (Imagen != null)
                                {
                                    updateCommand.Parameters.AddWithValue("@Imagen", Imagen);
                                }
                                updateCommand.Parameters.AddWithValue("@Cantidad", Cantidad);
                                updateCommand.Parameters.AddWithValue("@Sub_Total", Sub_Total);
                                updateCommand.Parameters.AddWithValue("@ID_Usuario", ID_Usuario);
                                updateCommand.Parameters.AddWithValue("@ID_Producto", id);
                                updateCommand.ExecuteNonQuery();
                            }
                        }
                        else
                        {
                            // Si el producto no existe, inserta un nuevo registro en la tabla Carrito
                            string nextID_Carrito;
                            int next;
                            int nextID;

                            // Último ID de la BD
                            string query = "SELECT COALESCE(MAX(ID_Carrito), 0) FROM Carrito";
                            using (SqlCommand command = new SqlCommand(query, connection))
                            {
                                object result = command.ExecuteScalar();
                                int maxID_Carrito = Convert.ToInt32(result);
                                next = maxID_Carrito + 1;
                                nextID_Carrito = next.ToString();
                                ID_Carrito = next;
                            }

                            string query2 = "SELECT COALESCE(MAX(ID), 0) FROM Carrito WHERE ID_Usuario = @ID_Usuario";
                            using (SqlCommand comm = new SqlCommand(query2, connection))
                            {
                                comm.Parameters.AddWithValue("@ID_Usuario", ID_Usuario);

                                object result = comm.ExecuteScalar();
                                int maxID = Convert.ToInt32(result);
                                nextID = maxID + 1;
                            }

                            // Calcular el costo total del pedido
                            string totalQuery = "SELECT SUM(Sub_Total) FROM Carrito WHERE ID_Usuario = @ID_Usuario";
                            using (SqlCommand totalCmd = new SqlCommand(totalQuery, connection))
                            {
                                totalCmd.Parameters.AddWithValue("@ID_Usuario", ID_Usuario);
                                object totalResult = totalCmd.ExecuteScalar();
                                int costoTotal;

                                // Verificar si la suma es nula
                                if (totalResult != DBNull.Value)
                                {
                                    costoTotal = Convert.ToInt32(totalResult) + Sub_Total;
                                }
                                else
                                {
                                    // Si la suma es nula, asignar el valor de Sub_Total
                                    costoTotal = Sub_Total;
                                }

                                // Insertar el nuevo registro en la tabla Carrito

                                string sqlQuery;

                                if (Imagen != null)
                                {
                                    sqlQuery = "INSERT INTO Carrito (ID, ID_Carrito, Imagen, Descripción, Cantidad, Precio, Sub_Total, Costo_Total, Fecha_Pedido, ID_Almacen, ID_Usuario, ID_Producto) VALUES (@ID, @ID_Carrito, @Imagen, @Descripción, @Cantidad, @Precio, @Sub_Total, @Costo_Total, @Fecha_Pedido, @ID_Almacen, @ID_Usuario, @ID_Producto)";
                                }
                                else
                                {
                                    sqlQuery = "INSERT INTO Carrito (ID, ID_Carrito, Descripción, Cantidad, Precio, Sub_Total, Costo_Total, Fecha_Pedido, ID_Almacen, ID_Usuario, ID_Producto) VALUES (@ID, @ID_Carrito, @Descripción, @Cantidad, @Precio, @Sub_Total, @Costo_Total, @Fecha_Pedido, @ID_Almacen, @ID_Usuario, @ID_Producto)";
                                }

                                
                                using (SqlCommand insertCmd = new SqlCommand(sqlQuery, connection))
                                {
                                    insertCmd.Parameters.AddWithValue("@ID", nextID);
                                    insertCmd.Parameters.AddWithValue("@ID_Carrito", int.Parse(nextID_Carrito));

                                    // Verificar si la imagen es null
                                    if (Imagen != null)
                                    {
                                        insertCmd.Parameters.AddWithValue("@Imagen", Imagen);
                                    }

                                    insertCmd.Parameters.AddWithValue("@Descripción", Convert.ToString(Descripción));
                                    insertCmd.Parameters.AddWithValue("@Cantidad", Convert.ToInt32(Cantidad));
                                    insertCmd.Parameters.AddWithValue("@Precio", Convert.ToInt32(Precio));
                                    insertCmd.Parameters.AddWithValue("@Sub_Total", Convert.ToInt32(Sub_Total));
                                    insertCmd.Parameters.AddWithValue("@Costo_Total", Convert.ToInt32(costoTotal));
                                    insertCmd.Parameters.AddWithValue("@Fecha_Pedido", DateTime.Now);
                                    insertCmd.Parameters.AddWithValue("@ID_Almacen", Convert.ToInt32(ID_Almacen));
                                    insertCmd.Parameters.AddWithValue("@ID_Usuario", Convert.ToInt32(ID_Usuario));
                                    insertCmd.Parameters.AddWithValue("@ID_Producto", Convert.ToInt32(id));

                                    insertCmd.ExecuteNonQuery();

                                    return true;
                                }
                            }
                        }
                    }
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

        public static List<CarritoModel> ObtenerCarrito(string connectionString, int ID)
        {
            try
            {
                List<CarritoModel> productos = new List<CarritoModel>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT ID, ID_Carrito, Imagen, Descripción, Cantidad, Precio, Sub_Total, Costo_Total, ID_Almacen, ID_Producto FROM Carrito WHERE ID_Usuario = @ID_Usuario";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@ID_Usuario", ID);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                byte[] imagen = reader["Imagen"] != DBNull.Value ? (byte[])reader["Imagen"] : null;

                                CarritoModel producto = new CarritoModel
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    ID_Carrito = Convert.ToInt32(reader["ID_Carrito"]),
                                    Imagen = imagen,
                                    Descripción = Convert.ToString(reader["Descripción"]),
                                    Cantidad = Convert.ToInt32(reader["Cantidad"]),
                                    Precio = Convert.ToInt32(reader["Precio"]),
                                    Sub_Total = Convert.ToInt32(reader["Sub_Total"]),
                                    Costo_Total = Convert.ToInt32(reader["Costo_Total"]),
                                    ID_Almacen = Convert.ToInt32(reader["ID_Almacen"]),
                                    ID_Producto = Convert.ToInt32(reader["ID_Producto"])
                                };

                                productos.Add(producto);
                            }
                        }
                    }
                }

                return productos;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al cargar pedido: " + ex.Message);

                // O puedes lanzar una nueva excepción con un mensaje personalizado:
                throw new Exception("Ocurrió un error al cargar el pedido. Detalles: " + ex.Message);
            }
        }

        public CarritoModel ObtenerInfoCarrito(string connectionString, int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string queryCarrito = "SELECT ID, ID_Carrito, Imagen, Descripción, Cantidad, Precio, Sub_Total, Costo_Total, ID_Almacen, ID_Producto FROM Carrito WHERE ID_Carrito = @ID_Carrito";

                    using (SqlCommand command = new SqlCommand(queryCarrito, conn))
                    {
                        command.Parameters.AddWithValue("@ID_Carrito", id);

                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                byte[] imagen = reader["Imagen"] != DBNull.Value ? (byte[])reader["Imagen"] : null;

                                CarritoModel car = new CarritoModel
                                {
                                    ID = Convert.ToInt32(reader["ID"]),
                                    ID_Carrito = Convert.ToInt32(reader["ID_Carrito"]),
                                    Imagen = imagen,
                                    Descripción = Convert.ToString(reader["Descripción"]),
                                    Cantidad = Convert.ToInt32(reader["Cantidad"]),
                                    Precio = Convert.ToInt32(reader["Precio"]),
                                    Sub_Total = Convert.ToInt32(reader["Sub_Total"]),
                                    Costo_Total = Convert.ToInt32(reader["Costo_Total"]),
                                    ID_Almacen = Convert.ToInt32(reader["ID_Almacen"]),
                                    ID_Producto = Convert.ToInt32(reader["ID_Producto"])
                                };

                                return car;
                            }
                        }
                    }
                }
                return null;
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
                // Example: log.Error("Error in ObtenerInfoCarrito: ", ex);
                return null;
            }
        }

        public bool ActualizarCarrito(string connectionString)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();
                    string query = "UPDATE Carrito SET Cantidad = @Cantidad, Sub_Total = @SubTotal, Costo_Total = @CostoTotal WHERE ID_Carrito = @IDCarrito";

                    using (SqlCommand cmd = new SqlCommand(query, conn))
                    {
                        cmd.Parameters.AddWithValue("@Cantidad", this.Cantidad);
                        cmd.Parameters.AddWithValue("@SubTotal", this.Sub_Total);
                        cmd.Parameters.AddWithValue("@CostoTotal", this.Costo_Total);
                        cmd.Parameters.AddWithValue("@IDCarrito", this.ID_Carrito);

                        int rowsAffected = cmd.ExecuteNonQuery();
                        return rowsAffected > 0;
                    }
                }
            }
            catch
            {
                return false;
            }
        }

        public bool EliminarProducto(string connectionString)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM Carrito WHERE ID_Carrito = @ID_Carrito AND ID_Usuario = @ID_Usuario";
                    cmd.Parameters.Add("@ID_Carrito", System.Data.SqlDbType.Int).Value = ID_Carrito;
                    cmd.Parameters.Add("@ID_Usuario", System.Data.SqlDbType.Int).Value = ID_Usuario;

                    cmd.ExecuteNonQuery();

                    // Actualizar los ID_Producto restantes si es necesario
                    SqlCommand cmdUpdate = new SqlCommand();
                    cmdUpdate.Connection = conn;
                    cmdUpdate.CommandText = "UPDATE Carrito SET ID_Carrito = ID_Carrito - 1, Costo_Total = Costo_Total - @Sub_Total WHERE ID_Carrito > @DeletedID AND ID_Usuario = @ID_Usuario";
                    cmdUpdate.Parameters.Add("@DeletedID", System.Data.SqlDbType.Int).Value = ID_Carrito;
                    cmdUpdate.Parameters.Add("@ID_Usuario", System.Data.SqlDbType.Int).Value = ID_Usuario;
                    cmdUpdate.Parameters.AddWithValue("@Sub_Total", Sub_Total);

                    cmdUpdate.ExecuteNonQuery();

                    SqlCommand cmdUpdateID = new SqlCommand();
                    cmdUpdateID.Connection = conn;
                    cmdUpdateID.CommandText = "UPDATE Carrito SET ID = ID - 1 WHERE ID_Carrito > @DeletedID AND ID_Usuario = @ID_Usuario";
                    cmdUpdateID.Parameters.Add("@DeletedID", System.Data.SqlDbType.Int).Value = ID_Carrito;
                    cmdUpdateID.Parameters.Add("@ID_Usuario", System.Data.SqlDbType.Int).Value = ID_Usuario;

                    cmdUpdateID.ExecuteNonQuery();

                    conn.Close();

                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        public bool VaciarCarrito(string connectionString)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = "DELETE FROM Carrito WHERE ID_Usuario = @ID_Usuario";
                    cmd.Parameters.Add("@ID_Usuario", System.Data.SqlDbType.Int).Value = ID_Usuario;

                    cmd.ExecuteNonQuery();

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
