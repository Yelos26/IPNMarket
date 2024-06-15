using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using System.Data;

namespace IPNMarket.Models
{
    public class ProductosModel
    {
        public byte[] Imagen { get; set; }
        public int ID_Producto { get; set; }
        public string Nombre_Producto { get; set; }
        public string Descripción { get; set; }
        public string Tamaño { get; set; }
        public decimal Costo_Unitario { get; set; }
        public int ID_Secciones { get; set; }
        public int ID_Escuela { get; set; }
        public DateTime Fecha_Reg { get; set; }
        public DateTime Fecha_Cad { get; set; }

        /* ESCUELAS */
        public string Nombre { get; set; }

        /* Producto */
        public string Nombre_Seccion { get; set; }


        public static List<ProductosModel> CargarProductos(string connectionString)
        {
            try
            {
                List<ProductosModel> productos = new List<ProductosModel>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT P.Imagen, P.ID_Producto, P.Nombre_Producto, P.Descripción, P.Tamaño, P.Costo_Unitario, S.Nombre AS Nombre_Seccion, P.ID_Secciones, P.ID_Escuela, P.Fecha_Reg, P.Fecha_Cad, (SELECT E.Nombre FROM Escuela E WHERE E.ID_Escuela = P.ID_Escuela) AS Nombre_Escuela FROM Producto P JOIN Secciones S ON P.ID_Secciones = S.ID_Secciones WHERE P.ID_Producto = P.ID_Producto";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            if (reader["Imagen"] != DBNull.Value)
                            {
                                ProductosModel producto = new ProductosModel
                                {
                                    Imagen = (byte[])reader["Imagen"],
                                    ID_Producto = Convert.ToInt32(reader["ID_Producto"]),
                                    Nombre_Producto = reader["Nombre_Producto"].ToString(),
                                    Descripción = reader["Descripción"].ToString(),
                                    Tamaño = reader["Tamaño"].ToString(),
                                    Costo_Unitario = reader.GetDecimal(reader.GetOrdinal("Costo_Unitario")),
                                    Nombre_Seccion = reader["Nombre_Seccion"].ToString(),
                                    ID_Secciones = Convert.ToInt32(reader["ID_Secciones"]),
                                    ID_Escuela = Convert.ToInt32(reader["ID_Escuela"]),
                                    Fecha_Reg = reader["Fecha_Reg"] != DBNull.Value ? reader.GetDateTime(reader.GetOrdinal("Fecha_Reg")) : DateTime.MinValue,
                                    Fecha_Cad = reader["Fecha_Cad"] != DBNull.Value ? reader.GetDateTime(reader.GetOrdinal("Fecha_Cad")) : DateTime.MinValue,
                                    Nombre = reader["Nombre_Escuela"].ToString()
                                };
                                productos.Add(producto);
                            }
                            else
                            {
                                ProductosModel producto = new ProductosModel
                                {
                                    ID_Producto = Convert.ToInt32(reader["ID_Producto"]),
                                    Nombre_Producto = reader["Nombre_Producto"].ToString(),
                                    Descripción = reader["Descripción"].ToString(),
                                    Tamaño = reader["Tamaño"].ToString(),
                                    Costo_Unitario = reader.GetDecimal(reader.GetOrdinal("Costo_Unitario")),
                                    Nombre_Seccion = reader["Nombre_Seccion"].ToString(),
                                    ID_Secciones = Convert.ToInt32(reader["ID_Secciones"]),
                                    ID_Escuela = Convert.ToInt32(reader["ID_Escuela"]),
                                    Fecha_Reg = reader["Fecha_Reg"] != DBNull.Value ? reader.GetDateTime(reader.GetOrdinal("Fecha_Reg")) : DateTime.MinValue,
                                    Fecha_Cad = reader["Fecha_Cad"] != DBNull.Value ? reader.GetDateTime(reader.GetOrdinal("Fecha_Cad")) : DateTime.MinValue,
                                    Nombre= reader["Nombre_Escuela"].ToString()
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
                throw new Exception("Ocurrió un error al cargar los productos. Detalles: " + ex.Message);
            }
        }


        public static List<ProductosModel> CargarProductosID(string connectionString, int id)
        {
            try
            {
                List<ProductosModel> productos = new List<ProductosModel>();

                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT P.Imagen, P.ID_Producto, P.Nombre_Producto, P.Descripción, P.Tamaño, P.Costo_Unitario, E.Nombre AS Nombre_Escuela FROM Producto P INNER JOIN Escuela E ON E.ID_Escuela = (SELECT ID_Escuela FROM Producto WHERE ID_Producto = P.ID_Producto) WHERE P.ID_Secciones = @ID_Secciones AND P.ID_Producto = P.ID_Producto";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@ID_Secciones", id);

                        SqlDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            if (reader["Imagen"] != DBNull.Value)
                            {
                                ProductosModel producto = new ProductosModel
                                {
                                    Imagen = (byte[])reader["Imagen"],
                                    ID_Producto = Convert.ToInt32(reader["ID_Producto"]),
                                    Nombre_Producto = reader["Nombre_Producto"].ToString(),
                                    Descripción = reader["Descripción"].ToString(),
                                    Tamaño = reader["Tamaño"].ToString(),
                                    Costo_Unitario = reader.GetDecimal(reader.GetOrdinal("Costo_Unitario")),
                                    Nombre = reader["Nombre_Escuela"].ToString()
                                };
                                productos.Add(producto);
                            }
                            else
                            {
                                ProductosModel producto = new ProductosModel
                                {
                                    ID_Producto = Convert.ToInt32(reader["ID_Producto"]),
                                    Nombre_Producto = reader["Nombre_Producto"].ToString(),
                                    Descripción = reader["Descripción"].ToString(),
                                    Tamaño = reader["Tamaño"].ToString(),
                                    Costo_Unitario = reader.GetDecimal(reader.GetOrdinal("Costo_Unitario")),
                                    Nombre = reader["Nombre_Escuela"].ToString()
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
                Console.WriteLine("Error al cargar productos: " + ex.Message);

                // O puedes lanzar una nueva excepción con un mensaje personalizado:
                throw new Exception("Ocurrió un error al cargar los productos. Detalles: " + ex.Message);
            }
        }

        public static ProductosModel ObtenerProductoPorId(string connectionString, int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT Nombre_Producto, Descripción, Tamaño, Costo_Unitario, Fecha_Reg, Fecha_Cad, ID_Secciones, ID_Escuela FROM Producto WHERE ID_Producto = @Id";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Id", id);

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            ProductosModel producto = new ProductosModel
                            {
                                Nombre_Producto = reader["Nombre_Producto"].ToString(),
                                Descripción = reader["Descripción"].ToString(),
                                Tamaño = reader["Tamaño"].ToString(),
                                Costo_Unitario = reader.GetDecimal(reader.GetOrdinal("Costo_Unitario")),
                                //Fecha_Reg = Convert.ToDateTime(reader["Fecha_Reg"]),
                                Fecha_Cad = reader["Fecha_Cad"] != DBNull.Value ? reader.GetDateTime(reader.GetOrdinal("Fecha_Cad")) : DateTime.MinValue,
                                ID_Secciones = Convert.ToInt32(reader["ID_Secciones"]),
                                ID_Escuela = Convert.ToInt32(reader["ID_Escuela"])
                            };

                            return producto;
                        }
                        else
                        {
                            // Si no se encuentra el producto con el ID especificado, puedes devolver nulo o un producto con valores predeterminados
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar errores aquí
                throw new Exception("Ocurrió un error al obtener el producto por su ID. Detalles: " + ex.Message);
            }
        }

        public bool ObtenerIMG(string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "SELECT Imagen FROM Producto WHERE ID_Producto = @ID_Producto";

                    using (SqlCommand command = new SqlCommand(sqlQuery, connection))
                    {
                        command.Parameters.AddWithValue("@ID_Producto", ID_Producto);

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

        public bool GuardarIMG(string connectionString)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    string sqlQuery = "UPDATE Producto SET Imagen = @Imagen WHERE ID_Producto = @ID_Producto";

                    using (SqlCommand updateCmd = new SqlCommand(sqlQuery, connection))
                    {
                        updateCmd.Parameters.AddWithValue("@ID_Producto", ID_Producto);
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

        public void ActualizarProducto(string connectionString)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "UPDATE Producto SET Nombre_Producto = @Nombre, Descripción = @Descripcion, Tamaño = @Tamaño, Costo_Unitario = @Costo, Fecha_Cad = @Fecha_Cad, ID_Secciones = @ID_Secciones, ID_Escuela = @ID_Escuela WHERE ID_Producto = @Id";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@Nombre", this.Nombre_Producto);
                        command.Parameters.AddWithValue("@Descripcion", this.Descripción);
                        command.Parameters.AddWithValue("@Tamaño", this.Tamaño);
                        command.Parameters.AddWithValue("@Costo", this.Costo_Unitario);
                        command.Parameters.AddWithValue("@Id", this.ID_Producto);
                        command.Parameters.Add("@Fecha_Cad", SqlDbType.Date).Value = this.Fecha_Cad.Date;
                        command.Parameters.AddWithValue("@ID_Secciones", this.ID_Secciones);
                        command.Parameters.AddWithValue("@ID_Escuela", this.ID_Escuela);

                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar errores
                throw new Exception("Error al actualizar el producto en la base de datos. Detalles: " + ex.Message);
            }
        }

        public bool GuardarNuevoProducto(string connectionString)
        {
            try
            {
                string nextID;
                int next;

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();

                    //Ultimo ID de la BD
                    string Query = "SELECT MAX(ID_Producto) FROM Producto";
                    using (SqlCommand command = new SqlCommand(Query, connection))
                    {
                        object result = command.ExecuteScalar();

                        int maxID = Convert.ToInt32(result);
                        next = maxID + 1;
                        nextID = next.ToString();
                        ID_Producto = next;
                    }


                    string sqlQuery = "INSERT INTO Producto (ID_Producto, Nombre_Producto, Descripción, Tamaño, Costo_Unitario, Fecha_Reg, Fecha_Cad, ID_Secciones, ID_Escuela) VALUES (@ID_Producto, @Nombre, @Descripcion, @Tamaño, @Costo, @Fecha_Reg, @Fecha_Cad, @ID_Secciones, @ID_Escuela)";

                    using (SqlCommand insertCmd = new SqlCommand(sqlQuery, connection))
                    {
                        insertCmd.Parameters.AddWithValue("@ID_Producto", int.Parse(nextID));
                        insertCmd.Parameters.AddWithValue("@Nombre", Nombre_Producto);
                        insertCmd.Parameters.AddWithValue("@Descripcion", Descripción);
                        insertCmd.Parameters.AddWithValue("@Tamaño", Tamaño);
                        insertCmd.Parameters.AddWithValue("@Costo", Costo_Unitario);
                        insertCmd.Parameters.AddWithValue("@Fecha_Reg", DateTime.Now.Date);
                        insertCmd.Parameters.AddWithValue("@Fecha_Cad", Convert.ToDateTime(Fecha_Cad));
                        insertCmd.Parameters.AddWithValue("@ID_Secciones", ID_Secciones);
                        insertCmd.Parameters.AddWithValue("@ID_Escuela", ID_Escuela);

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

        public bool EliminarProducto(string connectionString)
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
                            // Eliminar registros relacionados en Ventas
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cmd.Connection = conn;
                                cmd.Transaction = transaction;
                                cmd.CommandText = @"
                                DELETE FROM Ventas 
                                WHERE ID_Pedido IN (SELECT ID_Pedido FROM Pedido WHERE ID_Almacen IN 
                                    (SELECT ID_Almacen FROM Almacen WHERE ID_Producto = @ID_Producto))";
                                cmd.Parameters.Add("@ID_Producto", System.Data.SqlDbType.Int).Value = ID_Producto;
                                cmd.ExecuteNonQuery();
                            }

                            // Eliminar registros relacionados en Carrito
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cmd.Connection = conn;
                                cmd.Transaction = transaction;
                                cmd.CommandText = @"
                                DELETE FROM Carrito 
                                WHERE ID_Almacen IN (SELECT ID_Almacen FROM Almacen WHERE ID_Producto = @ID_Producto)";
                                cmd.Parameters.Add("@ID_Producto", System.Data.SqlDbType.Int).Value = ID_Producto;
                                cmd.ExecuteNonQuery();
                            }

                            // Eliminar registros relacionados en Pedido
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cmd.Connection = conn;
                                cmd.Transaction = transaction;
                                cmd.CommandText = @"
                                DELETE FROM Pedido 
                                WHERE ID_Almacen IN (SELECT ID_Almacen FROM Almacen WHERE ID_Producto = @ID_Producto)";
                                cmd.Parameters.Add("@ID_Producto", System.Data.SqlDbType.Int).Value = ID_Producto;
                                cmd.ExecuteNonQuery();
                            }

                            // Eliminar registros relacionados en Almacen
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cmd.Connection = conn;
                                cmd.Transaction = transaction;
                                cmd.CommandText = "DELETE FROM Almacen WHERE ID_Producto = @ID_Producto";
                                cmd.Parameters.Add("@ID_Producto", System.Data.SqlDbType.Int).Value = ID_Producto;
                                cmd.ExecuteNonQuery();
                            }

                            // Eliminar el producto
                            using (SqlCommand cmd = new SqlCommand())
                            {
                                cmd.Connection = conn;
                                cmd.Transaction = transaction;
                                cmd.CommandText = "DELETE FROM Producto WHERE ID_Producto = @ID_Producto";
                                cmd.Parameters.Add("@ID_Producto", System.Data.SqlDbType.Int).Value = ID_Producto;
                                cmd.ExecuteNonQuery();
                            }

                            transaction.Commit();
                            return true;
                        }
                        catch
                        {
                            transaction.Rollback();
                            throw;
                        }
                    }
                }
            }
            catch (SqlException sqlEx)
            {
                // Log o manejar la excepción SQL según sea necesario
                Console.WriteLine("Excepción SQL: " + sqlEx.Message);
                return false;
            }
            catch (Exception ex)
            {
                // Log o manejar la excepción general según sea necesario
                Console.WriteLine("Excepción: " + ex.Message);
                return false;
            }
        }



        public ProductosModel ObtenerProductos(string connectionString, int id)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connectionString))
                {
                    conn.Open();

                    string query = "SELECT P.ID_Producto, P.Imagen, P.Nombre_Producto, P.Descripción, P.Tamaño, P.Costo_Unitario, P.ID_Secciones, E.Nombre AS Nombre_Escuela FROM Producto P JOIN Escuela E ON E.ID_Escuela = (SELECT ID_Escuela FROM Producto WHERE ID_Producto = P.ID_Producto) WHERE P.ID_Producto = @ID_Producto";

                    using (SqlCommand command = new SqlCommand(query, conn))
                    {
                        command.Parameters.AddWithValue("@ID_Producto", id);

                        SqlDataReader reader = command.ExecuteReader();

                        if (reader.Read())
                        {
                            byte[] imagen = reader["Imagen"] != DBNull.Value ? (byte[])reader["Imagen"] : null;

                            ProductosModel producto = new ProductosModel
                            {
                                Imagen = imagen,
                                ID_Producto = Convert.ToInt32(id),
                                Nombre_Producto = reader["Nombre_Producto"].ToString(),
                                Descripción = reader["Descripción"].ToString(),
                                Tamaño = reader["Tamaño"].ToString(),
                                Costo_Unitario = reader.GetDecimal(reader.GetOrdinal("Costo_Unitario")),
                                ID_Secciones = Convert.ToInt32(reader["ID_Secciones"]),
                                Nombre = reader["Nombre_Escuela"].ToString()
                            };

                            return producto;
                        }
                        else
                        {
                            // Aquí puedes manejar el caso donde no se encuentra ningún producto con el ID especificado
                            return null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejar errores aquí
                throw new Exception("Ocurrió un error al obtener el producto por su ID. Detalles: " + ex.Message);
            }
        }
    }
}
