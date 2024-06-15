using IPNMarket.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.Design.Serialization;
using System.Linq;

namespace IPNMarket.Controllers
{
    public class ProductosController : Controller
    {
        /*
        TempData["ID_PERFIL"]
        TempData["ID_Usuario"]
        TempData["nombre"];
        TempData["Ap_Paterno"];
        TempData["Ap_Materno"];
        TempData["correo"];
        TempData["contraseña"];
        TempData["dirección"];
        TempData["Imagen"]

         */

        private readonly IConfiguration _configuration;

        public ProductosController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public IActionResult Productos()
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

                List<ProductosModel> productos = ProductosModel.CargarProductos(connectionString);

                return View(productos);
            }
            catch { return RedirectToAction("Error", "Home"); }
        }

        public ActionResult EditarProducto(int id)
        {
            try
            {
                if (id != 0)
                {
                    TempData["ID_Producto"] = Convert.ToString(id);
                }

                int ID2 = Convert.ToInt32(TempData["ID_Producto"] as string);

                if (ID2 != 0)
                {
                    id = ID2;
                    TempData["ID_Producto"] = Convert.ToString(id);
                }
                else
                {
                    TempData["ID_Producto"] = Convert.ToString(id);
                }

                string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

                // Recuperar los detalles del producto por su ID
                ProductosModel producto = ProductosModel.ObtenerProductoPorId(connectionString, id);

                AlmacenModel alm = new AlmacenModel();
                var almacen = alm.ObtenerAlmacenID(connectionString, id);

                List<EscuelaModel> escuelas = EscuelaModel.CargarEscuelas(connectionString);
                List<int> idEscuelas = escuelas.Select(e => e.ID_Escuela).ToList();
                List<string> nombresEscuelas = escuelas.Select(e => e.Nombre).ToList();

                ViewBag.NombresEscuelas = nombresEscuelas;
                ViewBag.ID_Escuelas = idEscuelas;
                ViewBag.Almacen = almacen;
                ViewBag.Seccion = producto.ID_Secciones;

                return View(producto);
            }
            catch { return RedirectToAction("Error", "Home"); }
        }

        public IActionResult ModificarImagen()
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

                ProductosModel pro = new ProductosModel();

                pro.ID_Producto = Convert.ToInt32(TempData["ID_Producto"] as string);
                TempData["ID_Producto"] = Convert.ToString(pro.ID_Producto);

                pro.ObtenerIMG(connectionString);

                if (pro.Imagen != null)
                {
                    string extension = "";

                    // Determinar la extensión del archivo de imagen
                    switch (Path.GetExtension("nombre_archivo.jpg").ToLowerInvariant()) // Reemplaza "nombre_archivo.jpg" por el nombre del archivo
                    {
                        case ".jpg":
                        case ".jpeg":
                            extension = "jpeg";
                            break;
                        case ".png":
                            extension = "png";
                            break;
                    }

                    if (!string.IsNullOrEmpty(extension))
                    {
                        string base64String = Convert.ToBase64String(pro.Imagen);
                        string imageDataURL = string.Format("data:image/{0};base64,{1}", extension, base64String);
                        ViewBag.Imagen = imageDataURL;
                        ViewBag.n = "0";
                    }
                    else
                    {
                        ViewBag.n = "1";
                    }
                }
                else
                {
                    ViewBag.n = "1";
                }

                return View();
            }
            catch { return RedirectToAction("Error", "Home"); }
        }

        //Guardar IMAGEN en la BD
        [HttpPost]
        public ActionResult GuardarIMG(IFormFile archivoInput)
        {
            try
            {
                if (archivoInput != null && archivoInput.Length > 0)
                {
                    string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

                    // Verifica que el archivo sea una imagen
                    if (archivoInput.ContentType == "image/jpeg" || archivoInput.ContentType == "image/jpg" || archivoInput.ContentType == "image/png")
                    {
                        ProductosModel pro = new ProductosModel();

                        pro.ID_Producto = Convert.ToInt32(TempData["ID_Producto"] as string);

                        TempData["ID_Producto"] = Convert.ToString(pro.ID_Producto);

                        pro.Imagen = ConvertToByteArray(archivoInput.OpenReadStream());

                        pro.GuardarIMG(connectionString);

                        //TempData["Imagen"] = archivoInput.OpenReadStream();

                    }
                    else
                    {
                        // El archivo no es una imagen válida
                    }
                }

                return RedirectToAction("EditarProducto", "Productos");

            }
            catch { return RedirectToAction("Error", "Home"); }

        }

        // Convertir la imagen a un arreglo de bytes
        private byte[] ConvertToByteArray(Stream stream)
        {
            using (var memoryStream = new MemoryStream())
            {
                stream.CopyTo(memoryStream);
                return memoryStream.ToArray();
            }
        }

        // Actualizar el producto en la BD por ID
        [HttpPost]
        public ActionResult ActualizarProducto(ProductosModel producto, int cantidad)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

                int id = Convert.ToInt32(TempData["ID_Producto"] as string);

                producto.ID_Producto = id;

                producto.ActualizarProducto(connectionString);

                TempData["ID_Producto"] = Convert.ToString(producto.ID_Producto);

                AlmacenModel almacen = new AlmacenModel();
                almacen.Cantidad = cantidad;
                almacen.Precio_Total = cantidad * Convert.ToInt32(producto.Costo_Unitario);

                almacen.ActualizarIDP(connectionString, id);

                return RedirectToAction("EditarProducto", new { id = producto.ID_Producto });
            }
            catch
            {
                // Manejo de errores
                return View("EditarProducto", producto); // Volver a la vista de edición con los datos del producto
            }
        }

        public IActionResult EliminarProducto(int id)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

                ProductosModel pro = new ProductosModel();
                AlmacenModel alm = new AlmacenModel();

                if (id != 0)
                {
                    TempData["ID_Producto"] = Convert.ToString(id);
                }

                int ID2 = Convert.ToInt32(TempData["ID_Producto"] as string);

                if (ID2 != 0)
                {
                    id = ID2;

                    TempData["ID_Producto"] = Convert.ToString(id);
                }
                else
                {
                    TempData["ID_Producto"] = Convert.ToString(id);
                }

                pro.ID_Producto = Convert.ToInt32(id);
                //alm.ID_Producto = Convert.ToInt32(id);

                //alm.ObtenerAlmacenID(connectionString, id);
                //alm.EliminarProducto(connectionString);

                pro.EliminarProducto(connectionString);

                return RedirectToAction("Productos", "Productos");
            }
            catch { return RedirectToAction("Error", "Home"); }

        }

        public IActionResult AgregarProducto()
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

                ViewBag.n = "1";
                var producto = new ProductosModel();

                List<EscuelaModel> escuelas = EscuelaModel.CargarEscuelas(connectionString);
                List<int> idEscuelas = escuelas.Select(e => e.ID_Escuela).ToList();
                List<string> nombresEscuelas = escuelas.Select(e => e.Nombre).ToList();

                ViewBag.NombresEscuelas = nombresEscuelas;
                ViewBag.ID_Escuelas = idEscuelas;

                return View(producto);
            }
            catch { return RedirectToAction("Error", "Home"); }
        }

        [HttpPost]
        public IActionResult AgregarProducto(ProductosModel producto, IFormFile archivoInput, int cantidad)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

                // Guardar el nuevo producto en la base de datos
                producto.GuardarNuevoProducto(connectionString);

                AlmacenModel alm = new AlmacenModel();
                alm.ID_Producto = producto.ID_Producto;
                alm.Cantidad = cantidad;
                alm.Precio_Total = cantidad * Convert.ToInt32(producto.Costo_Unitario);

                alm.GuardarEnAlmacen(connectionString);

                if (archivoInput != null && archivoInput.Length > 0)
                {
                    // Verifica que el archivo sea una imagen
                    if (archivoInput.ContentType == "image/jpeg" || archivoInput.ContentType == "image/jpg" || archivoInput.ContentType == "image/png")
                    {
                        TempData["ID_Producto"] = Convert.ToString(producto.ID_Producto);

                        producto.Imagen = ConvertToByteArray(archivoInput.OpenReadStream());

                        producto.GuardarIMG(connectionString);

                    }
                    else
                    {
                        
                        return View(producto);
                    }
                }

                // Redirigir a la página de productos después de agregar el producto
                return RedirectToAction("Productos");
            }
            catch { return RedirectToAction("Error", "Home"); }
        }

        public IActionResult Carrusel()
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");
                List<CarruselModel> carrusel = CarruselModel.CargarCarrusel(connectionString);
                return View(carrusel);
            }
            catch { return RedirectToAction("Error", "Home"); }
        }

        public ActionResult EditarIMGCarrusel(int id)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

                TempData["ID_Carrusel"] = Convert.ToString(id);

                // Recuperar los detalles del producto por su ID
                CarruselModel carrusel = CarruselModel.ObtenerCarruselPorId(connectionString, id);

                if (carrusel.Imagen != null)
                {
                    string extension = "";

                    // Determinar la extensión del archivo de imagen
                    switch (Path.GetExtension("nombre_archivo.jpg").ToLowerInvariant()) // Reemplaza "nombre_archivo.jpg" por el nombre del archivo
                    {
                        case ".jpg":
                        case ".jpeg":
                            extension = "jpeg";
                            break;
                        case ".png":
                            extension = "png";
                            break;
                    }

                    if (!string.IsNullOrEmpty(extension))
                    {
                        string base64String = Convert.ToBase64String(carrusel.Imagen);
                        string imageDataURL = string.Format("data:image/{0};base64,{1}", extension, base64String);
                        ViewBag.Imagen = imageDataURL;
                        ViewBag.n = "0";
                    }
                    else
                    {
                        ViewBag.n = "1";
                    }
                }
                else
                {
                    ViewBag.n = "1";
                }

                return View(carrusel);
            }
            catch { return RedirectToAction("Error", "Home"); }
        }

        [HttpPost]
        public ActionResult GuardarIMGcarrusel(IFormFile archivoInput, string Controlador, string Accion)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

                CarruselModel carru = new CarruselModel();

                // Obtener el ID_Carrusel de TempData
                int idCarrusel;
                if (TempData["ID_Carrusel"] != null && int.TryParse(TempData["ID_Carrusel"].ToString(), out idCarrusel))
                {
                    carru.ID_Carrusel = idCarrusel;
                    carru.Controlador = Controlador;
                    carru.Accion = Accion;
                }
                else
                {
                    // Manejar el caso en que no se pueda obtener el ID_Carrusel
                    return RedirectToAction("Error", "Home");
                }

                if (archivoInput != null)
                {
                    carru.Imagen = ConvertToByteArray(archivoInput.OpenReadStream());
                }
                else
                {
                    carru.Imagen = null;
                }

                carru.GuardarIMG(connectionString);

                TempData["ID_Carrusel"] = carru.ID_Carrusel.ToString(); // Actualizar el TempData con el ID_Carrusel

                return RedirectToAction("Carrusel", "Productos");
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult EliminarIMGCarrusel(int id)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

                CarruselModel carru = new CarruselModel();

                carru.ID_Carrusel = id;

                carru.EliminarCarrusel(connectionString);

                return RedirectToAction("Carrusel", "Productos");
            }
            catch { return RedirectToAction("Error", "Home"); }

        }

        public IActionResult AgregarIMGCarrusel()
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

                ViewBag.n = "1";
                var carr = new CarruselModel();

                return View(carr);
            }
            catch { return RedirectToAction("Error", "Home"); }
        }

        [HttpPost]
        public IActionResult AgregarIMGCarrusel(CarruselModel carr, IFormFile archivoInput)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

                if (archivoInput != null && archivoInput.Length > 0)
                {
                    // Verifica que el archivo sea una imagen
                    if (archivoInput.ContentType == "image/jpeg" || archivoInput.ContentType == "image/jpg" || archivoInput.ContentType == "image/png")
                    {

                        carr.Imagen = ConvertToByteArray(archivoInput.OpenReadStream());

                        carr.GuardarIMGcarrusel(connectionString);

                    }
                    else
                    {

                        return View(carr);
                    }
                }

                return RedirectToAction("Carrusel");
            }
            catch { return RedirectToAction("Error", "Home"); }
        }
    }
}
