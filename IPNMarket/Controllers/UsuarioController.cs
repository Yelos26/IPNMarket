using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System;
using IPNMarket.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Hosting.Server;
using System.IO;
using Microsoft.AspNetCore.Http;
using static System.Net.Mime.MediaTypeNames;
using System.Windows;


namespace IPNMarket.Controllers
{
    public class UsuarioController : Controller
    {
        /*  TempData  */
        /*************************
                 USUARIO

        TempData["ID_PERFIL"]
        TempData["ID_Usuario"]
        TempData["nombre"]
        TempData["Ap_Paterno"]
        TempData["Ap_Materno"]
        TempData["correo"]
        TempData["contraseña"]
        TempData["dirección"]
        TempData["Imagen"]

        *************************/
        /***************************************************************************************************/



        private readonly IConfiguration _configuration;

        public UsuarioController(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        /***************************************************************************************************/



        /* VERIFICAR EN CORREO */

        //Verificar que el correo termine en: "@gmail.com"
        private bool EsGmail(string texto)
        {
            if (texto == null)
            {
                return false;
            }

            string patron = @".+@gmail\.com$";
            Regex regex = new Regex(patron);
            return regex.IsMatch(texto);
        }

        //Verificar que el correo no exista
        private bool VerificarCorreoExistente(UsuarioModel usu, string correo)
        {
            string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

            try
            {
                return usu.VerificarCorreo(connectionString, correo);
            }
            catch
            {
                return false;
            }
        }

        /***************************************************************************************************/



        /* CREAR CUENTA */

        // 1er Controlador/Vista para Crear una cuenta
        public IActionResult Crear_cuenta()
        {
            try
            {
                ViewBag.Correo = TempData["correo"] as string ?? "";

                ViewBag.MensajeCorreo = TempData["MensajeCorreo"] as string;
                ViewBag.MensajeContraseña = TempData["MensajeContraseña"] as string;
                ViewBag.MensajeContraseña2 = TempData["MensajeContraseña2"] as string;

                return View();
            }
            catch { return RedirectToAction("Error", "Home"); }
        }

        // 2do Controlador/Vista para Crear una cuenta
        [HttpPost]
        public ActionResult V_CrearCuenta(UsuarioModel usu, string email, string contraseña, string V_contraseña, string Dirección)
        {
            string texto = email;

            // Verificar que el correo termine en @gmail.com
            bool valido = EsGmail(email);

            if (!valido)
            {
                TempData["MensajeCorreo"] = "CORREO NO VALIDO";

                return RedirectToAction("Crear_cuenta", "Usuario");
            }
            else
            {
                // Verificar que el correo no exista
                bool correoExistente = VerificarCorreoExistente(usu, email);

                if (correoExistente)
                {
                    TempData["MensajeCorreo"] = "CORREO EXISTENTE";

                    return RedirectToAction("Crear_cuenta", "Usuario");
                }
                else
                {
                    if (email == null || contraseña == null || V_contraseña == null)
                    {
                        TempData["MensajeContraseña2"] = "DEBES LLENAR LOS ESPACIOS";

                        return RedirectToAction("Crear_cuenta", "Usuario");
                    }
                    else if (email != null && contraseña != null && V_contraseña != null)
                    {
                        if (contraseña == V_contraseña)
                        {
                            TempData["correo"] = email;
                            TempData["contraseña"] = contraseña;
                            TempData["dirección"] = Dirección;

                            return View();
                        }
                        else
                        {
                            TempData["correo"] = email;

                            TempData["MensajeContraseña2"] = "LAS CONTRASEÑAS SON DIFERENTES";

                            return RedirectToAction("Crear_cuenta", "Usuario");
                        }
                    }
                    else
                    {

                        return RedirectToAction("Crear_cuenta", "Usuario");
                    }

                }

            }
        }

        // 3er Controlador para Crear una cuenta
        [HttpPost]
        public ActionResult V2_CrearCuenta(string nombre, string Ap_Paterno, string Ap_Materno, string Dirección)
        {
            
            string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

            // Llamar los valores de los TempData
            string correo = TempData["correo"] as string;
            string contraseña = TempData["contraseña"] as string;

            try
            {
                UsuarioModel usu = new UsuarioModel();

                // Insertar la información del Usuario en la BD
                usu.InsertarNuevoUsuario(connectionString, nombre, Ap_Paterno, Ap_Materno, correo, contraseña, Dirección);

                // Insertar la información del Usuario en los TempData
                TempData["ID_PERFIL"] = Convert.ToString(usu.ID_PERFIL);
                TempData["ID_Usuario"] = Convert.ToString(usu.ID_Usuario);
                TempData["nombre"] = nombre;
                TempData["Ap_Paterno"] = Ap_Paterno;
                TempData["Ap_Materno"] = Ap_Materno;
                TempData["correo"] = correo;
                TempData["contraseña"] = contraseña;
                TempData["dirección"] = Dirección;
                TempData["numeroGC"] = "1"; 

                return View();
            }
            catch
            {
                return RedirectToAction("V_CrearCuenta", "Usuario");
            }
        }

        /***************************************************************************************************/



        /* INICIAR SESION */

        // 1er Controlador/Vista para Iniciar Sesion
        public IActionResult Iniciar_Sesion(string GuardarContraseña)
        {
            TempData["num"] = "0";

            ViewBag.Correo = TempData["correo"] as string ?? "";

            ViewBag.MensajeCorreo = TempData["MensajeCorreo"] as string;
            ViewBag.MensajeContraseña = TempData["MensajeContraseña"] as string;

            return View();
        }

        // 2do Controlador para Iniciar Sesion
        [HttpPost]
        public ActionResult VerificarCredenciales(string email, string contraseña)
        {
            try
            {
                if ((email != null) && (contraseña != null))
                {
                    string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");
                    UsuarioModel usuarioModel = new UsuarioModel();

                    UsuarioModel usuario = usuarioModel.ObtenerUsuarioPorCredenciales(connectionString, email, contraseña);

                    if (usuario != null)
                    {
                        TempData["ID_PERFIL"] = Convert.ToString(usuario.ID_PERFIL);
                        TempData["ID_Usuario"] = Convert.ToString(usuario.ID_Usuario);
                        TempData["Ap_Paterno"] = usuario.Ap_Paterno;
                        TempData["Ap_Materno"] = usuario.Ap_Materno;
                        TempData["correo"] = usuario.Correo;
                        TempData["contraseña"] = usuario.Contraseña;
                        TempData["nombre"] = usuario.Nombre_Usuario;
                        TempData["dirección"] = usuario.Direccion;

                        return RedirectToAction("Home", "Home");
                    }
                    else
                    {
                        TempData["MensajeContraseña"] = "Error en el correo o en la contraseña";

                        return RedirectToAction("Iniciar_Sesion", "Usuario");
                    }
                }
                else
                {
                    if (email == null)
                    {
                        TempData["MensajeCorreo"] = "Debes colocar un correo";
                        
                        return RedirectToAction("Iniciar_Sesion", "Usuario");
                    }
                    else if (contraseña == null)
                    {
                        TempData["MensajeContraseña"] = "Debes colocar una contraseña";
                        TempData["correo"] = email;

                        return RedirectToAction("Iniciar_Sesion", "Usuario");
                    }
                    else 
                    {
                        TempData["MensajeCorreo"] = "Debes colocar un correo";
                        TempData["MensajeContraseña"] = "Debes colocar una contraseña";

                        return RedirectToAction("Iniciar_Sesion", "Usuario");
                    }
                }
            }
            catch { return RedirectToAction("Error", "Home"); }
        }

        /***************************************************************************************************/


        /* CON SESION INICIADA */

        // Controlador/Vista donde se muestra la información del Usuario(Normal)
        public IActionResult Sesion()
        {
            string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

            UsuarioModel usu = new UsuarioModel();

            usu.ID_Usuario = Convert.ToInt32(TempData["ID_Usuario"] as string);
            TempData["ID_Usuario"] = Convert.ToString(usu.ID_Usuario);

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
                    ViewBag.n = "1"; 
                }
            }
            else
            {
                ViewBag.n = "1";
            }


            string ID = TempData["ID_Usuario"] as string;
            TempData["ID_Usuario"] = ID;

            string ID_PERFIL = TempData["ID_PERFIL"] as string;
            ViewData["ID_PERFIL"] = ID_PERFIL;
            TempData["ID_PERFIL"] = ID_PERFIL;

            string nombre = TempData["nombre"] as string;
            ViewData["Nombree"] = nombre;
            TempData["nombre"] = nombre;

            string Ap_Paterno = TempData["Ap_Paterno"] as string;
            ViewData["ApPat"] = Ap_Paterno;
            TempData["Ap_Paterno"] = Ap_Paterno;

            string Ap_Materno = TempData["Ap_Materno"] as string;
            ViewData["ApMat"] = Ap_Materno;
            TempData["Ap_Materno"] = Ap_Materno;

            string correo = TempData["correo"] as string;
            ViewData["Correo"] = correo;
            TempData["correo"] = correo;

            string contraseña = TempData["contraseña"] as string;
            ViewData["contraseña"] = contraseña;
            TempData["contraseña"] = contraseña;

            string Dirección = TempData["dirección"] as string;
            ViewData["dirección"] = Dirección;
            TempData["dirección"] = Dirección;

            List<PedidoModel> pedido = PedidoModel.CargarPedidosUsuario(connectionString, Convert.ToInt32(ID));

            return View(pedido);
        }

        // Controlador/Vista donde se muestra la información del Usuario(Admin)
        public IActionResult SesionAdmin()
        {
            string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

            UsuarioModel usu = new UsuarioModel();

            usu.ID_Usuario = Convert.ToInt32(TempData["ID_Usuario"] as string);
            TempData["ID_Usuario"] = Convert.ToString(usu.ID_Usuario);

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
                    ViewBag.n = "1";
                }
            }
            else
            {
                ViewBag.n = "1";
            }


            string ID = TempData["ID_Usuario"] as string;
            TempData["ID_Usuario"] = ID;

            string nombre = TempData["nombre"] as string;
            ViewData["Nombree"] = nombre;
            TempData["nombre"] = nombre;

            string Ap_Paterno = TempData["Ap_Paterno"] as string;
            ViewData["ApPat"] = Ap_Paterno;
            TempData["Ap_Paterno"] = Ap_Paterno;

            string Ap_Materno = TempData["Ap_Materno"] as string;
            ViewData["ApMat"] = Ap_Materno;
            TempData["Ap_Materno"] = Ap_Materno;

            string correo = TempData["correo"] as string;
            ViewData["Correo"] = correo;
            TempData["correo"] = correo;

            string contraseña = TempData["contraseña"] as string;
            ViewData["contraseña"] = contraseña;
            TempData["contraseña"] = contraseña;

            string Dirección = TempData["dirección"] as string;
            ViewData["dirección"] = Dirección;
            TempData["dirección"] = Dirección;

            return View();
        }

        // 1er Controlador/Vista para modificar la info del Usuario 
        public IActionResult EditarPerfilUsuario(string nombre, string Ap_Paterno, string Ap_Materno, string email, string Dirección)
        {
            ViewData["ID_PERFIL"] = TempData["ID_PERFIL"] as string;
            ViewData["nombre"] = TempData["nombre"] as string;
            ViewData["Ap_Paterno"] = TempData["Ap_Paterno"] as string;
            ViewData["Ap_Materno"] = TempData["Ap_Materno"] as string;
            ViewData["correo"] = TempData["correo"] as string;
            ViewData["contraseña"] = TempData["contraseña"] as string;
            ViewData["dirección"] = TempData["dirección"] as string;

            TempData["ID_PERFIL"] = ViewData["ID_PERFIL"];
            TempData["nombre"] = ViewData["nombre"];
            TempData["Ap_Paterno"] = ViewData["Ap_Paterno"];
            TempData["Ap_Materno"] = ViewData["Ap_Materno"];
            TempData["correo"] = ViewData["correo"];
            TempData["contraseña"] = ViewData["contraseña"];
            TempData["dirección"] = ViewData["dirección"];

            return View();
        }

        //************

        // Controladores/Vistas para modificar el CORREO del Usuario

            // Modificar CORREO
        public IActionResult ModificarCORREO(string email)
        {
            ViewData["mensajeC"] = TempData["mensajeC"] as string;

            ViewData["correo"] = TempData["correo"] as string;
            TempData["correo"] = ViewData["correo"];
            return View();
        }

            // Comprobar que el Usuario conosca la contraseña de su correo
        [HttpPost]
        public ActionResult ComprobarCORREO(string email, string contraseña)
        {
            try
            {
                string contra = TempData["contraseña"] as string;
                TempData["contraseña"] = contra;

                string correo = TempData["correo"] as string;
                TempData["correo"] = correo;

                if (contraseña == contra && email == correo)
                {
                    return RedirectToAction("NuevoCORREO", "Usuario");
                }
                else
                {
                    TempData["mensajeC"] = "Error en la contraseña";
                    return RedirectToAction("ModificarCORREO", "Usuario");
                }
            }
            catch { return RedirectToAction("Error", "Home"); }
        }

            // Ingresar el nuevos correo
        public IActionResult NuevoCORREO()
        {
            string mensaje = TempData["mensajeCorreo"] as string;

            if (mensaje == null)
            {
                ViewBag.MensajeCorreo = "";
            }

            return View();
        }

        // Guardar el nuevos correo en la BD
        [HttpPost]
        public ActionResult GuardarCORREO([FromBody] CorreoData correoData)
        {
            try
            {
                UsuarioModel usu = new UsuarioModel();

                bool valido = EsGmail(correoData.NuevCorreo);

                if (!valido)
                {
                    TempData["mensajeCorreo"] = "El correo debe terminar en @gmail.com";
                    return RedirectToAction("NuevoCORREO", "Usuario");
                }
                else
                {
                    bool correoExistente = VerificarCorreoExistente(usu, correoData.NuevCorreo);

                    if (correoExistente)
                    {
                        TempData["mensajeCorreo"] = "Correo en uso";
                        return RedirectToAction("NuevoCORREO", "Usuario");
                    }
                    else
                    {
                        string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

                        string ID = TempData["ID_Usuario"] as string;
                        TempData["ID_Usuario"] = ID;

                        usu.ID_Usuario = Convert.ToInt32(ID);

                        usu.GuardarNuevoCorreo(connectionString, correoData.NuevCorreo);

                        TempData["correo"] = correoData.NuevCorreo;

                        TempData["mensajeCorreo"] = "Correo guardado correctamente";
                        return RedirectToAction("NuevoCORREO", "Usuario");
                    }
                    
                }
                
            }
            catch { return RedirectToAction("Error", "Home"); }
        }

        public class CorreoData
        {
            public string NuevCorreo { get; set; }
        }

        //************

        // Controladores/Vistas para modificar la CONTRASEÑA del Usuario

        // Colocar COntraseña actual y la nueva contraseña
        public IActionResult ModificarCONTRASEÑA()
        {
            return View();
        }

        //Guardar la nueva contraseña en la BD
        [HttpPost]
        public ActionResult NuevaContraseña([FromBody] nuevaContraseña nC)
        {
            string contra = TempData["contraseña"] as string;
            TempData["contraseña"] = contra;

            string contraseña = nC.contraseña;
            string contraseña1 = nC.contraseña1;
            string contraseña2 = nC.contraseña2;

            if (contra == contraseña)
            {
                if (contraseña1 == contraseña2)
                {
                    UsuarioModel usu = new UsuarioModel();

                    string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

                    string ID = TempData["ID_Usuario"] as string;
                    TempData["ID_Usuario"] = ID;

                    usu.ID_Usuario = Convert.ToInt32(ID);

                    usu.GuardarNuevaContraseña(connectionString, contraseña2);

                    TempData["contraseña"] = contraseña2;

                    return RedirectToAction("EditarPerfilUsuario", "Usuario");
                }
            }
            else
            {
                // Manejar el caso donde las contraseñas no coinciden
            }

            // Manejar el caso donde la contraseña actual es incorrecta
            return RedirectToAction("EditarPerfilUsuario", "Usuario");
        }

        public class nuevaContraseña
        {
            public string contraseña { get; set; }
            public string contraseña1 { get; set; }
            public string contraseña2 { get; set; }
        }

        //************

        // Controladores/Vistas para modificar la IMAGEN del Usuario

        // Seleccionar IMAGEN
        public IActionResult ModificarImagen()
        {
            string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

            UsuarioModel usu = new UsuarioModel();

            usu.ID_Usuario = Convert.ToInt32(TempData["ID_Usuario"] as string);
            TempData["ID_Usuario"] = Convert.ToString(usu.ID_Usuario);

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
                    ViewBag.n = "1";
                }
            }
            else
            {
                ViewBag.n = "1";
            }

            return View();
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
                        UsuarioModel usu = new UsuarioModel();

                        usu.ID_Usuario = Convert.ToInt32(TempData["ID_Usuario"] as string);

                        usu.Imagen = ConvertToByteArray(archivoInput.OpenReadStream());

                        usu.GuardarIMG(connectionString);

                        TempData["ID_Usuario"] = Convert.ToString(usu.ID_Usuario);

                        //TempData["Imagen"] = archivoInput.OpenReadStream();

                    }
                    else
                    {
                        // El archivo no es una imagen válida
                    }
                }

                string GC = TempData["numeroGC"] as string;
                TempData["numeroGC"] = GC;

                if (GC != null) 
                {
                    return RedirectToAction("Home", "Home");
                }


                return RedirectToAction("EditarPerfilUsuario", "Usuario");

            }
            catch
            {
                return Content("error");
            }
            
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

        //************

        // Guardar los cambios en la BD (NOMBRE, APELLIDOS, DIRECCIÓN)

        [HttpPost]
        public IActionResult ActualizarPerfilUsuario([FromBody] UsuarioData usuarioData)
        {
            string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");
            string ID = TempData["ID_Usuario"] as string;
            TempData["ID_Usuario"] = ID;

            try
            {
                UsuarioModel usu = new UsuarioModel();
                bool actualizado = usu.ActualizarP(connectionString, ID, usuarioData.nombre, usuarioData.Ap_Paterno, usuarioData.Ap_Materno, usuarioData.email, usuarioData.Dirección);

                if (actualizado)
                {
                    TempData["nombre"] = usuarioData.nombre;
                    TempData["Ap_Paterno"] = usuarioData.Ap_Paterno;
                    TempData["Ap_Materno"] = usuarioData.Ap_Materno;
                    //TempData["correo"] = usuarioData.email;
                    TempData["dirección"] = usuarioData.Dirección;

                    return Ok(); // Indicar que la acción fue exitosa
                }
                else
                {
                    return BadRequest(); // Indicar que hubo un error
                }
            }
            catch
            {
                return StatusCode(500); // Indicar que hubo un error interno del servidor
            }
        }
        public class UsuarioData
        {
            public string nombre { get; set; }
            public string Ap_Paterno { get; set; }
            public string Ap_Materno { get; set; }
            public string email { get; set; }
            public string Dirección { get; set; }
        }

        [HttpPost]
        public ActionResult EliminarPerfilUsuario()
        {
            string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");
            string ID = TempData["ID_Usuario"] as string;
            TempData["ID_Usuario"] = ID;

            try
            {
                UsuarioModel usu = new UsuarioModel();

                usu.ID_Usuario = Convert.ToInt32(ID);

                usu.EliminarUsuario(connectionString);

                TempData.Remove("ID_PERFIL");
                TempData.Remove("nombre");
                ViewData.Remove("Nomb");
                TempData.Remove("Ap_Paterno");
                TempData.Remove("Ap_Materno");
                TempData.Remove("correo");
                TempData.Remove("contraseña");
                TempData.Remove("dirección");
                TempData.Remove("numeroGC");

                return Ok(); // Indicar que la acción fue exitosa
            }
            catch
            {
                return StatusCode(500); // Indicar que hubo un error interno del servidor
            }
        }

        /***************************************************************************************************/



        /* RECUPERAR CONTRASEÑA */

        // Controlador/Vista para colocar el numero al azar que se mando al correo
        public IActionResult RecuperarContraseña(UsuarioModel usu, string email)
        {
            try
            {
                var contraseña = Recuperar(usu, email);

                if (contraseña != null)
                {
                    TempData["correo"] = email;

                    return View();
                }
                else
                {
                    // Mostrar un mensaje de error al usuario
                    return RedirectToAction("Iniciar_Sesion", "Usuario");
                }
            }
            catch { return RedirectToAction("Error", "Home"); }
        }

            // Mandar numero al azar al correo del USUARIO
        [HttpPost]
        public ActionResult Recuperar(UsuarioModel usu, string email)
        {
            try
            {

                string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");
                bool correoExistente = VerificarCorreoExistente(usu, email);

                if (correoExistente)
                {
                    UsuarioModel usuario = usu.ObtenerIDPorEmail(connectionString, email);

                    if (usuario.ID_PERFIL == 1)
                    {
                        int codigoAleatorio = Convert.ToInt32(TempData["num"] as string);

                        if (codigoAleatorio == 0)
                        {
                            Random random = new Random();
                            codigoAleatorio = random.Next(10, 99);
                            TempData["num"] = Convert.ToString(codigoAleatorio);
                        }
                        else
                        {
                            TempData["num"] = Convert.ToString(codigoAleatorio);
                        }

                        using (MailMessage mail = new MailMessage())
                        {
                            mail.From = new MailAddress("PNImarket45@outlook.com");
                            mail.To.Add(email);
                            mail.Subject = "Recuperación de contraseña";
                            mail.Body = "Su código de recuperación de contraseña es: " + codigoAleatorio;
                            mail.IsBodyHtml = true;
                            mail.Priority = MailPriority.Normal;

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

                        return Content("Se ha enviado un código de recuperación a su dirección de correo electrónico.");
                    }
                    else
                    {
                        TempData["MensajeContraseña"] = "Contactenos";

                        return null;
                    }
                }
                else
                {
                    TempData["MensajeContraseña"] = "El correo no existe";

                    return null;
                }
                    
            }
            catch
            {
                return null;
            }
        }

            // Verificar que el numero al azar sea igual al numero que el Usuario ponga
        [HttpPost]
        public ActionResult ContraseñaRecuperada(string numAzar)
        {
            int aleatorio = Convert.ToInt32(TempData["num"] as string);
            string email = TempData["correo"] as string;

            if (Convert.ToInt32(numAzar) == aleatorio)
            {
                string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");
                UsuarioModel usuarioModel = new UsuarioModel();

                if (usuarioModel.RecuperarContraseña(connectionString, email))
                {
                    return RedirectToAction("Iniciar_Sesion", "Usuario");
                }
                else
                {
                    return null;
                }
            }
            else
            {
                TempData["num"] = Convert.ToString(aleatorio);
            }

            return null;
        }

            // Redirigir al Usuario a una vista segun lo que ocurra en "ContraseñaRecuperada"
        public IActionResult ContraseñaLista(string numAzar)
        {
            var contraseña = ContraseñaRecuperada(numAzar);

            if (contraseña != null)
            {
                TempData["MensajeContraseña"] = "Se envio la contraseña a su correo";
                return RedirectToAction("Iniciar_Sesion", "Usuario");
            }
            else
            {
                return RedirectToAction("Error", "Home");
            }
        }

        /***************************************************************************************************/


        public IActionResult Consultar(int? id)
        {
            try
            {
                string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

                if (id == null)
                {
                    return RedirectToAction("Error", "Home");
                }
                else if (id == 0)
                {
                    // Cargar y mostrar todos los usuarios
                    var usuarios = UsuarioModel.CargarUsuarios(connectionString);
                    ViewBag.ID = 0;
                    return View("Consultar", usuarios);
                }
                else
                {
                    switch (id)
                    {
                        case 1:
                            // Cargar y mostrar todas las escuelas
                            var escuelas = EscuelaModel.CargarEscuelas(connectionString);
                            ViewBag.ID = 1;
                            return View("Consultar", escuelas);
                        case 2:
                            // Cargar y mostrar todas las secciones
                            var secciones = SeccionesModel.CargarSecciones(connectionString);
                            ViewBag.ID = 2;
                            return View("Consultar", secciones);
                        case 3:
                            // Cargar y mostrar todos los perfiles
                            var perfiles = CAT_PERFILModel.CargarPerfiles(connectionString);
                            ViewBag.ID = 3;
                            return View("Consultar", perfiles);
                        case 4:
                            // Cargar y mostrar todos los usuarios
                            var usuarios = UsuarioModel.CargarUsuarios(connectionString);
                            ViewBag.ID = 4;
                            return View("Consultar", usuarios);
                        case 5:
                            // Cargar y mostrar todos los productos
                            var productos = ProductosModel.CargarProductos(connectionString);
                            ViewBag.ID = 5;
                            return View("Consultar", productos);
                        case 6:
                            // Cargar y mostrar el almacen
                            var almacen = AlmacenModel.CargarAlmacen(connectionString);
                            ViewBag.ID = 6;
                            return View("Consultar", almacen);
                        case 7:
                            // Cargar y mostrar todos los pedidos
                            var pedido = PedidoModel.CargarPedidos(connectionString);
                            ViewBag.ID = 7;
                            return View("Consultar", pedido);
                        case 8:
                            // Cargar y mostrar todos las ventas
                            var ventas = VentasModel.CargarVentas(connectionString);
                            ViewBag.ID = 8;
                            return View("Consultar", ventas);
                        default:
                            return RedirectToAction("Error", "Home");
                    }
                }
            }
            catch
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult VerUsuario(int? ID_Usuario)
        {
            try
            {
                if (ID_Usuario == null)
                {
                    return RedirectToAction("Consultar", "Usuario", new { id = 8 });
                }

                int id = Convert.ToInt32(ID_Usuario);
                string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

                UsuarioModel usu = new UsuarioModel();
                usu.ID_Usuario = id;
                usu = usu.ObtenerUsuarioPorID(connectionString);

                return View(usu);
            }
            catch
            {
                return RedirectToAction("Consultar", "Usuario", new { id = 8 });
            }
        }

        public IActionResult VerPedido(int? ID_Pedido)
        {
            try
            {

                if (ID_Pedido == null)
                {
                    return RedirectToAction("Consultar", "Usuario", new { id = 8 });
                }

                int id = Convert.ToInt32(ID_Pedido);
                string connectionString = _configuration.GetConnectionString("StringCONSQLlocal");

                PedidoModel ped = new PedidoModel();
                ped.ID_Pedido = id;
                ped = ped.ObtenerPedidoPorID(connectionString);

                UsuarioModel usu = new UsuarioModel();
                usu.ID_Usuario = ped.ID_Usuario;
                usu = usu.ObtenerUsuarioPorID(connectionString);
                ViewBag.Nombre_Usuario = usu.Nombre_Usuario + " " + usu.Ap_Paterno + " " + usu.Ap_Materno;

                return View(ped);
            }
            catch
            {
                return RedirectToAction("Consultar", "Usuario", new { id = 8 });
            }
        }
    }
}
