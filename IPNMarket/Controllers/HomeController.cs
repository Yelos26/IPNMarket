using IPNMarket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Data.SqlClient;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
//using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using Microsoft.AspNetCore.Http;
using System.Runtime.ConstrainedExecution;
//using System.Web.Mvc;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting;
using System.Net.Mail;

namespace IPNMarket.Controllers
{
    public class HomeController : Controller
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
        private readonly CarritoModel _carrito;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public HomeController(IConfiguration configuration, IWebHostEnvironment webHostEnvironment)
        {
            _carrito = new CarritoModel();
            _configuration = configuration;
            _webHostEnvironment = webHostEnvironment;
        }


        public IActionResult Home()
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

                List<ProductosModel> productos = ProductosModel.CargarProductos(connectionString);

                string ID_PERFIL = TempData["ID_PERFIL"] as string;
                string ID = TempData["ID_Usuario"] as string;
                string nombre = TempData["nombre"] as string;
                string ApPaterno = TempData["Ap_Paterno"] as string;
                string ApMaterno = TempData["Ap_Materno"] as string;
                string correo = TempData["correo"] as string;
                string contraseña = TempData["contraseña"] as string;
                string dirección = TempData["dirección"] as string;
                string Imagen = TempData["Imagen"] as string;

                if (nombre != null)
                {
                    UsuarioModel usu = new UsuarioModel();

                    usu.ID_Usuario = Convert.ToInt32(ID);

                    usu.ObtenerIMG(connectionString);

                    if (usu.Imagen != null)
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
                            string base64String = Convert.ToBase64String(usu.Imagen);
                            string imageDataURL = string.Format("data:image/{0};base64,{1}", extension, base64String);
                            ViewBag.Imagen = imageDataURL;
                            ViewBag.n = "0";
                        }
                        else
                        {
                            ViewBag.n = "1"; // Formato de imagen no reconocido
                        }
                    }
                    else
                    {
                        ViewBag.n = "1"; // No se encontró ninguna imagen
                    }

                    ViewData["Nomb"] = nombre;
                    TempData["nombre"] = nombre;

                    ViewData["ID_PERFIL"] = ID_PERFIL;
                    TempData["ID_PERFIL"] = ID_PERFIL;

                    TempData["ID_Usuario"] = ID;
                    TempData["Ap_Paterno"] = ApPaterno;
                    TempData["Ap_Materno"] = ApMaterno;
                    TempData["correo"] = correo;
                    TempData["contraseña"] = contraseña;
                    TempData["dirección"] = dirección;
                }
                else
                {
                    ViewData["Nomb"] = null;
                }

                List<CarruselModel> carrusel = CarruselModel.CargarCarrusel(connectionString);
                ViewBag.Carrusel = carrusel;

                return View(productos);
            }
            catch
            { return RedirectToAction("Error", "Home"); }
        }

        public IActionResult Carrusel()
        {
            string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");
            List<CarruselModel> carrusel = CarruselModel.CargarCarrusel(connectionString);
            return View(carrusel);
        }

        public IActionResult cerrar()
        {
            try
            {
                TempData.Remove("ID_PERFIL");
                TempData.Remove("nombre");
                ViewData.Remove("Nomb");
                TempData.Remove("Ap_Paterno");
                TempData.Remove("Ap_Materno");
                TempData.Remove("correo");
                TempData.Remove("contraseña");
                TempData.Remove("dirección");
                TempData.Remove("numeroGC");

                return RedirectToAction("Home", "Home");
            }
            catch { return RedirectToAction("Error", "Home"); }
        }

        public IActionResult Categorias(int? id, string text)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

                string ID_PERFIL = TempData["ID_PERFIL"] as string;
                string ID2 = TempData["ID_Usuario"] as string;
                string nombre = TempData["nombre"] as string;
                string ApPaterno = TempData["Ap_Paterno"] as string;
                string ApMaterno = TempData["Ap_Materno"] as string;
                string correo = TempData["correo"] as string;
                string contraseña = TempData["contraseña"] as string;
                string dirección = TempData["dirección"] as string;
                string Imagen = TempData["Imagen"] as string;

                ViewData["Nomb"] = nombre;

                string nomb = text ?? "";
                ViewBag.nomb = nomb;

                int ID = id ?? 0;
                ViewBag.ID = ID;

                List<ProductosModel> productos = new List<ProductosModel>();

                if (ID == 0)
                {
                    productos = ProductosModel.CargarProductos(connectionString);
                }
                else
                {
                    productos = ProductosModel.CargarProductosID(connectionString, ID);
                }

                TempData["nombre"] = nombre;
                TempData["ID_PERFIL"] = ID_PERFIL;
                TempData["ID_Usuario"] = ID2;
                TempData["Ap_Paterno"] = ApPaterno;
                TempData["Ap_Materno"] = ApMaterno;
                TempData["correo"] = correo;
                TempData["contraseña"] = contraseña;
                TempData["dirección"] = dirección;

                return View(productos); // Pasar la lista de productos al modelo de la vista
            }
            catch
            { return RedirectToAction("Error", "Home"); }
        }

        public IActionResult Index()
        {
            return RedirectToAction("Home", "Home");
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult VerProducto(int? ID_Producto)
        {
            try
            {
                string ID_PERFIL = TempData["ID_PERFIL"] as string;
                ViewData["ID_PERFIL"] = ID_PERFIL;
                TempData["ID_PERFIL"] = ID_PERFIL;

                if (ID_Producto == null)
                {
                    return RedirectToAction("Home", "Home");
                }

                int id = Convert.ToInt32(ID_Producto);

                string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

                ProductosModel prO = new ProductosModel();
                ProductosModel pro = prO.ObtenerProductos(connectionString, id);

                int ID = pro.ID_Secciones;

                SeccionesModel seC = new SeccionesModel();
                SeccionesModel sec = seC.ObtenerSeccion(connectionString, ID);

                AlmacenModel alM = new AlmacenModel();
                AlmacenModel alm = alM.ObtenerAlmacenID(connectionString, id);

                ViewBag.Cantidad = alm.Cantidad;

                ViewBag.Seccion = sec.Nombre;

                return View(pro);
            }
            catch { return RedirectToAction("Error", "Home"); }
        }

        public IActionResult AgregarAlCarrito(CarritoModel car, int id, int cantidad)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

                ProductosModel prO = new ProductosModel();
                ProductosModel pro = prO.ObtenerProductos(connectionString, id);

                AlmacenModel alM = new AlmacenModel();
                AlmacenModel alm = alM.ObtenerAlmacenID(connectionString, id);

                car.ID = Convert.ToInt32(TempData["ID"] as string);
                if (car.ID == 0) 
                {
                    car.ID = 1;                
                }
                else
                {
                    car.ID = car.ID + 1;
                }

                TempData["ID"] = Convert.ToString(car.ID);

                car.Imagen = pro.Imagen;
                car.Descripción = pro.Descripción;
                car.Cantidad = cantidad;
                car.Precio = Convert.ToInt32(pro.Costo_Unitario);
                car.Sub_Total = Convert.ToInt32(pro.Costo_Unitario) * car.Cantidad;
                car.Costo_Total = 0; 
                car.ID_Almacen = alm.ID_Almacen;
                car.ID_Usuario = Convert.ToInt32(TempData["ID_Usuario"] as string);

                car.GuardarPedido(connectionString, id);

                alm.ID_Almacen = car.ID_Almacen;
                alm.Cantidad = alm.Cantidad - cantidad;

                alm.Actualizar(connectionString);

                TempData["ID_Usuario"] = Convert.ToString(car.ID_Usuario);
                TempData["ID_Carrito"] = Convert.ToString(car.ID_Carrito);

                return RedirectToAction("Carrito");
            }
            catch
            { return RedirectToAction("Error", "Home"); }

        }

        public IActionResult RestarAlCarrito(int id, int cantidad)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

                int ID = Convert.ToInt32(TempData["ID_Carrito"] as string);

                ProductosModel prO = new ProductosModel();
                ProductosModel pro = prO.ObtenerProductos(connectionString, id);

                AlmacenModel alM = new AlmacenModel();
                AlmacenModel alm = alM.ObtenerAlmacenID(connectionString, id);

                CarritoModel caR = new CarritoModel();
                CarritoModel car = caR.ObtenerInfoCarrito(connectionString, ID);

                car.Cantidad = car.Cantidad - cantidad;
                car.Sub_Total = car.Sub_Total - (Convert.ToInt32(pro.Costo_Unitario) * cantidad);
                car.Costo_Total = car.Costo_Total - car.Sub_Total;

                car.ActualizarCarrito(connectionString);

                alm.ID_Almacen = car.ID_Almacen;

                alm.Cantidad = alm.Cantidad + cantidad;
                alm.Actualizar(connectionString);

                TempData["ID_Carrito"] = Convert.ToString(car.ID_Carrito);

                return RedirectToAction("Carrito");
            }
            catch
            { return RedirectToAction("Error", "Home"); }

        }

        public IActionResult Carrito()
        {
            try
            {
                int id = Convert.ToInt32(TempData["ID_Usuario"] as string);
                TempData["ID_Usuario"] = Convert.ToString(id);

                string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

                List<CarritoModel> carrito = CarritoModel.ObtenerCarrito(connectionString, id);

                return View(carrito);
            }
            catch
            { return RedirectToAction("Error", "Home"); }
        }

        public IActionResult MetodoPago()
        {
            try
            {
                int id = Convert.ToInt32(TempData["ID_Usuario"] as string);
                TempData["ID_Usuario"] = Convert.ToString(id);

                return View();
            }
            catch
            { return RedirectToAction("Error", "Home"); }
        }

        [HttpPost]
        public ActionResult RevisarTarjeta(string numtarjeta, int mes, int año, int cvc, string nomtitu)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

                TarjetaModel tar = new TarjetaModel
                {
                    Numero_Tarjeta = numtarjeta,
                    Mes = mes,
                    Año = año,
                    CVC = cvc
                };

                tar.VerificarTarjeta(connectionString);

                if (tar.Nombre_Titular == nomtitu)
                {
                    return RedirectToAction("Comprar", "Home");
                }

                return RedirectToAction("MetodoPago", "Home");
            }
            catch
            { return RedirectToAction("Error", "Home"); }
        }


        public IActionResult Comprar()
        {
            try
            {
                int id = Convert.ToInt32(TempData["ID_Usuario"] as string);
                TempData["ID_Usuario"] = Convert.ToString(id);

                string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

                List<CarritoModel> carrito = CarritoModel.ObtenerCarrito(connectionString, id);

                foreach (var item in carrito)
                {
                    item.Imagen = null; // Establecer la imagen a null
                }

                List<int> cantidades = carrito.Select(e => e.Cantidad).ToList();
                List<int> costosTotales = carrito.Select(e => e.Costo_Total).ToList();
                List<int> idAlmacenes = carrito.Select(e => e.ID_Almacen).ToList();

                List<int> idcarrito = carrito.Select(e => e.ID_Carrito).ToList();
                List<int> Sub_Total = carrito.Select(e => e.Sub_Total).ToList();
                
                PedidoModel pedido = new PedidoModel();
                pedido.GuardarPedido(cantidades, costosTotales, idAlmacenes, id, idcarrito, Sub_Total, connectionString);

                TempData["Carrito"] = JsonConvert.SerializeObject(carrito);

                CarritoModel car = new CarritoModel();
                car.ID_Usuario = id;
                car.VaciarCarrito(connectionString);

                return RedirectToAction("GeneratePdf", "Home");
            }
            catch
            { return RedirectToAction("Error", "Home"); } 
        }

        public IActionResult GeneratePdf()
        {
            try
            {
                // Obtener los datos del carrito
                string carritoJ = TempData["Carrito"] as string;
                List<CarritoModel> carrito = string.IsNullOrEmpty(carritoJ) ? new List<CarritoModel>() : JsonConvert.DeserializeObject<List<CarritoModel>>(carritoJ);

                // Obtener los datos del reporte
                List<ReporteModel> reportData = GetReportData(carrito);

                // Generar el PDF
                using (MemoryStream stream = new MemoryStream())
                {
                    // Definir el tamaño de página para el ticket
                    Document document = new Document(PageSize.LETTER, 36, 36, 36, 36);

                    PdfWriter writer = PdfWriter.GetInstance(document, stream);
                    document.Open();

                    // Agregar imagen en la parte superior derecha
                    string imagePath = Path.Combine(_webHostEnvironment.WebRootPath, "Img", "logo", "IPN Market.png");
                    if (System.IO.File.Exists(imagePath))
                    {
                        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance(imagePath);
                        logo.ScaleAbsoluteHeight(32f); // Establecer altura de la imagen a 32 píxeles
                        float ratio = logo.Width / logo.Height; // Calcular relación de aspecto
                        logo.ScaleAbsoluteWidth(32f * ratio); // Establecer ancho proporcional
                        logo.Alignment = iTextSharp.text.Image.ALIGN_RIGHT; // Alinear imagen en la esquina superior derecha
                        document.Add(logo);
                    }

                    // Estilos de fuente
                    BaseFont bf = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, BaseFont.NOT_EMBEDDED);
                    Font titleFont = new Font(bf, 16, Font.BOLD, BaseColor.BLACK);
                    Font headerFont = new Font(bf, 12, Font.NORMAL, BaseColor.BLACK);
                    Font itemFont = new Font(bf, 10, Font.NORMAL, BaseColor.BLACK);
                    Font totalFont = new Font(bf, 12, Font.BOLD, BaseColor.BLACK);

                    // Título del reporte
                    Paragraph title = new Paragraph("TICKET DE COMPRA", titleFont);
                    title.Alignment = Element.ALIGN_CENTER;
                    document.Add(title);
                    document.Add(new Paragraph(" "));

                    // Información del vendedor
                    document.Add(new Paragraph("Nombre del vendedor: IPN Market", headerFont));
                    document.Add(new Paragraph("RFC: SAE1234567", headerFont));
                    document.Add(new Paragraph("Número de ticket: 123456789", headerFont));
                    document.Add(new Paragraph("Fecha y hora: " + DateTime.Now.ToString("dd/MM/yyyy, HH:mm"), headerFont));
                    document.Add(new Paragraph(" "));
                    document.Add(new Paragraph("Productos adquiridos:", headerFont));
                    document.Add(new Paragraph(" "));

                    // Tabla de datos
                    PdfPTable table = new PdfPTable(2);
                    table.WidthPercentage = 100;
                    table.SetWidths(new float[] { 3, 1 });

                    // Datos del reporte
                    foreach (var item in reportData)
                    {
                        table.AddCell(new PdfPCell(new Phrase("- " + item.Descripción + " ( " + item.Cantidad + " )", itemFont)) { Border = 0, Padding = 5f });
                        table.AddCell(new PdfPCell(new Phrase(item.Precio.ToString("C"), itemFont)) { Border = 0, Padding = 5f, HorizontalAlignment = Element.ALIGN_RIGHT });
                    }

                    document.Add(table);
                    document.Add(new Paragraph(" "));

                    // Calcular el subtotal, IVA y total
                    decimal subtotalAmount = reportData.Sum(x => x.Subtotal);
                    decimal ivaAmount = subtotalAmount * 0.16m;
                    decimal totalAmount = subtotalAmount + ivaAmount;

                    // Agregar subtotal, IVA y total
                    document.Add(new Paragraph("Subtotal: " + subtotalAmount.ToString("C"), totalFont));
                    document.Add(new Paragraph("IVA (16%): " + ivaAmount.ToString("C"), totalFont));
                    document.Add(new Paragraph("Total a pagar: " + totalAmount.ToString("C"), totalFont));
                    document.Add(new Paragraph(" "));
                    document.Add(new Paragraph("¡Gracias por su compra!", totalFont));
                    document.Add(new Paragraph(" "));

                    document.Close();
                    writer.Close();

                    // Guardar el PDF en el servidor
                    string fileName = "Reporte.pdf";
                    string filePath = Path.Combine(_webHostEnvironment.WebRootPath, "Content", fileName);

                    // Crea el directorio si no existe
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));

                    System.IO.File.WriteAllBytes(filePath, stream.ToArray());

                    EnviarPDF(filePath);

                    // Redirigir a la vista que muestra el PDF
                    return RedirectToAction("ViewPdf", new { fileName });
                }
            }
            catch { return RedirectToAction("Error", "Home"); }
        }

        private List<ReporteModel> GetReportData(List<CarritoModel> carrito)
        {
            // Lógica para mapear los datos del carrito a ReporteModel
            List<ReporteModel> reportData = new List<ReporteModel>();

            foreach (var item in carrito)
            {
                reportData.Add(new ReporteModel
                {
                    Descripción = item.Descripción,
                    Cantidad = item.Cantidad,
                    Precio = item.Precio,
                    Subtotal = item.Cantidad * item.Precio // Calcular subtotal, por ejemplo
                });
            }

            return reportData;
        }

        public IActionResult ViewPdf(string fileName)
        {
            try
            {
                ViewBag.FileName = fileName;
                return View();
            }
            catch { return RedirectToAction("Error", "Home"); }
        }

        private void EnviarPDF(string archivoAdjunto)
        {
            string email = TempData["correo"] as string;

            TempData["correo"] = email;

            using (MailMessage mail = new MailMessage())
            {
                mail.From = new MailAddress("PNImarket45@outlook.com");
                mail.To.Add(email);
                mail.Subject = "Reporte de Compra";
                mail.Body = "Adjunto encontrará el reporte de su compra.";
                mail.IsBodyHtml = true;
                mail.Priority = MailPriority.Normal;

                Attachment attachment = new Attachment(archivoAdjunto);
                mail.Attachments.Add(attachment);

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
        }

        public IActionResult EditarProducto(int? idC)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

                CarritoModel caR = new CarritoModel();
                CarritoModel car = caR.ObtenerInfoCarrito(connectionString, idC.Value);

                int id = Convert.ToInt32(car.ID_Producto);

                TempData["ID_Carrito"] =Convert.ToString(car.ID_Carrito);

                ProductosModel prO = new ProductosModel();
                ProductosModel pro = prO.ObtenerProductos(connectionString, id);

                int ID = pro.ID_Secciones;

                SeccionesModel seC = new SeccionesModel();
                SeccionesModel sec = seC.ObtenerSeccion(connectionString, ID);

                AlmacenModel alM = new AlmacenModel();
                AlmacenModel alm = alM.ObtenerAlmacenID(connectionString, id);

                ViewBag.Cantidad = alm.Cantidad;
                ViewBag.CantidadCarrito = car.Cantidad;
                ViewBag.ID_Carrito = car.ID_Carrito;

                ViewBag.Seccion = sec.Nombre;

                return View(pro);
            }
            catch { return RedirectToAction("Error", "Home"); }
        }

        public IActionResult EliminarProducto(int id, int cantidad)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");
                CarritoModel caR = new CarritoModel();
                CarritoModel car = caR.ObtenerInfoCarrito(connectionString, id);

                AlmacenModel alM = new AlmacenModel();
                AlmacenModel alm = alM.ObtenerAlmacenID(connectionString, car.ID_Producto);

                car.ID_Carrito = Convert.ToInt32(id);
                car.ID_Usuario = Convert.ToInt32(TempData["ID_Usuario"] as string);
                TempData["ID_Usuario"] = Convert.ToString(car.ID_Usuario);

                car.EliminarProducto(connectionString);

                alm.Cantidad = alm.Cantidad + cantidad;
                alm.Actualizar(connectionString);

                return RedirectToAction("Carrito", "Home");
            }
            catch { return RedirectToAction("Error", "Home"); }
        }
    }
}
