#pragma checksum "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Home\Categorias.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "8a0896344a7dadece5e093d24785a264b09aaeb798de09f3a4c7446f210ffc42"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_Categorias), @"mvc.1.0.view", @"/Views/Home/Categorias.cshtml")]
namespace AspNetCore
{
    #line hidden
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Mvc;
    using global::Microsoft.AspNetCore.Mvc.Rendering;
    using global::Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\_ViewImports.cshtml"
using IPNMarket;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\_ViewImports.cshtml"
using IPNMarket.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA256", @"8a0896344a7dadece5e093d24785a264b09aaeb798de09f3a4c7446f210ffc42", @"/Views/Home/Categorias.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA256", @"6a2cee79ee38bdc425be605c12c6ce0927acdcfe755524434d2da9f4ecf9e942", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Home_Categorias : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<IPNMarket.Models.ProductosModel>>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("body"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<!DOCTYPE html>\r\n<html lang=\"es\">\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8a0896344a7dadece5e093d24785a264b09aaeb798de09f3a4c7446f210ffc423916", async() => {
                WriteLiteral(@"
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Tu Título Aquí</title>

    <!-- BODY -->
    <style>

        .body {
            background: rgb(27, 18, 18);
        }
    </style>

    <!-- Area de los productos -->

    <style>
        .area-productos {
            background: rgba(52, 52, 52, 0.3);
            margin-left: -20px; /* Margen negativo a la izquierda */
            margin-right: -20px; /* Margen negativo a la derecha */
        }

        /* Area de cada fila de productos */
        .productos {
            display: flex;
            flex-wrap: wrap;
            justify-content: space-between;
            margin-left: 20px; /* Añade un margen de 10px a la izquierda para compensar el margen negativo */
            margin-right: 20px;
        }

        /* Producto individual */
        .producto {
            /* text-align: center; */
            width: 30%; /* Cada producto ocupa un máximo de ");
                WriteLiteral(@"30% del ancho del contenedor padre */
            margin-bottom: 20px; /* Espacio entre productos */
            padding: 20px;
            background-color: lightgray;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            cursor: pointer;
            height: 350px;
            border-radius: 10px;
        }

            /* Imágenes en cada producto */
            .producto img {
                width: auto;
                height: 180px;
                display: block;
                margin: 0 auto 10px;
                border-radius: 20px;
            }

        /* h2 {
            color: white;
            margin-bottom: 30px;
            text-align: center;
        } */
    </style>

    <style>
        /* Estilos para el menú lateral */
        .menu-lateral {
            position: fixed;
            top: 0;
            left: 0;
            width: 200px; 
            height: 100%; 
            background-color: rgb(20, 10, 10");
                WriteLiteral(@", 0.3);
            padding-top: 20px; 
        }

            .menu-lateral button {
                display: block;
                padding: 15px;
                text-decoration: none;
                color: #fff;
                transition: background-color 0.3s;
                font-family: Arial, sans-serif; /* Cambia la fuente */
                font-size: 15px; /* Cambia el tamaño del texto */
            }

                .menu-lateral button:hover {
                    background-color: #555;
                    color: white;
                }

        /* Estilos para el área de contenido principal */
        .area-contenido {
            margin-left: 200px;
            padding: 20px;
        }

        /* Estilos para los elementos de menú */
        .menu-item {
            padding: 10px;
            cursor: pointer;
        }

        .btn {
            margin-top: 10px;
            width: 80%;
            height: 50px;
            border-radius: 10px;
       ");
                WriteLiteral(@"     display: flex;
            justify-content: center;
            align-items: center;
            font-weight: 500;
            gap: 10px;
            border: 1px solid transparent;
            background-color: transparent;
            color: white;
            cursor: pointer;
            transition: 0.2s ease-in-out;
        }

            .btn:hover {
                color: #8B0000;
            }


        /* Estilos para el iframe y el contenedor del iframe */
        .iframe-container {
            display: none; 
            position: fixed;
            top: 0;
            left: 0;
            width: 100%;
            height: 100%;
            background-color: rgba(0, 0, 0, 0.5);
            justify-content: center;
            align-items: center;
            z-index: 2010;
        }

            .iframe-container iframe {
                width: 100%;
                height: 100%;
                border: none;
            }

        .close-btn {
            p");
                WriteLiteral(@"osition: absolute;
            top: 10px;
            right: 10px;
            background-color: red;
            color: white;
            border: none;
            padding: 10px;
            cursor: pointer;
        }

        /* Area del Buscador */
        .Buscador-container {
            flex-grow: 1;
            text-align: center;
        }
        /* Buscador */
        .Buscador {
            display: inline-flex;
            align-items: center;
            position: relative;
        }
        /* Contenedor del Icono-Buscador */
        .Buscador-icon-container {
            background-color: #fff;
            border: 2px solid transparent;
            border-radius: 8px;
            padding: 0.5rem;
            display: flex;
            align-items: center;
            justify-content: center;
            margin-right: 0.5rem;
            width: 40px; /* Tamaño fijo */
            height: 40px; /* Tamaño fijo */
            transition: 0.3s ease;
            cur");
                WriteLiteral(@"sor: pointer;
        }

            .Buscador-icon-container:hover,
            .Buscador-icon-container:focus {
                border-color: rgba(234, 226, 183, 0.4);
                box-shadow: 0 0 0 4px rgb(234 226 183 / 10%);
            }
        /* Icono-Buscador */
        .Buscador-icon {
            fill: #000;
            width: 1.5rem; /* Tamaño del ícono */
            height: 1.5rem; /* Tamaño del ícono */
        }
        /* Area para Buscar */
        .Buscador-input {
            flex-grow: 1; /* El campo de búsqueda ocupa todo el espacio restante */
            min-width: 400px; /* Ancho mínimo del campo de búsqueda */
            height: 40px;
            line-height: 28px;
            padding: 0 1rem;
            border: 2px solid transparent;
            border-radius: 8px;
            outline: none;
            background-color: #f3f3f4;
            color: #0d0c22;
            transition: 0.3s ease;
        }
            /* Placeholder color */
            ");
                WriteLiteral(@".Buscador-input::placeholder {
                color: #9e9ea7;
            }
            /* Focus and hover states */
            .Buscador-input:focus,
            .Buscador-input:hover {
                outline: none;
                border-color: rgba(234, 226, 183, 0.4);
                background-color: #fff;
                box-shadow: 0 0 0 4px rgb(234 226 183 / 10%);
            }

        .close-btn {
            position: absolute;
            top: 10px;
            right: 10px;
            background-color: red;
            color: white;
            border: none;
            padding: 10px;
            cursor: pointer;
        }

        
    </style>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "8a0896344a7dadece5e093d24785a264b09aaeb798de09f3a4c7446f210ffc4211964", async() => {
                WriteLiteral(@"

    <!-- Menú lateral -->
    <div class=""menu-lateral"">
        <center>

            <hr style=""border: none; height: 8px; background-color: rgb(20, 10, 10, 0.5);"">

            <button class=""btn"" onclick=""REGRESAR()"">Regresar</button>

            <hr style=""border: none; height: 8px; background-color: rgb(20, 10, 10, 0.5);"">

            <button class=""btn"" onclick=""Catego0()"">TODO</button>

            <hr style=""border: none; height: 8px; background-color: rgb(20, 10, 10, 0.5);"">

            <p style=""color: white""><strong>SECCIONES</strong></p>

            <hr style=""border: none; height: 8px; background-color: rgb(20, 10, 10, 0.5);"">

            <button class=""btn"" onclick=""Catego1()"">ACADEMICA</button>
            <button class=""btn"" onclick=""Catego3()"">DEPORTIVA</button>
            <button class=""btn"" onclick=""Catego2()"">MEDICA</button>
            <button class=""btn"" onclick=""Catego4()"">OTROS...</button>
        </center>
    </div>

    <script>
        function ");
                WriteLiteral("REGRESAR() {\r\n            window.location.href = \'");
#nullable restore
#line 266 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Home\Categorias.cshtml"
                               Write(Url.Action("Home", "Home"));

#line default
#line hidden
#nullable disable
                WriteLiteral("\';\r\n        }\r\n\r\n        function Catego0() {\r\n            var idProducto = 0;\r\n            window.location.href = \'");
#nullable restore
#line 271 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Home\Categorias.cshtml"
                               Write(Url.Action("Categorias", "Home"));

#line default
#line hidden
#nullable disable
                WriteLiteral("?id=\' + idProducto;\r\n        }\r\n\r\n        function Catego1() {\r\n            var idProducto = 1;\r\n            window.location.href = \'");
#nullable restore
#line 276 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Home\Categorias.cshtml"
                               Write(Url.Action("Categorias", "Home"));

#line default
#line hidden
#nullable disable
                WriteLiteral("?id=\' + idProducto;\r\n        }\r\n\r\n        function Catego2() {\r\n            var idProducto = 2;\r\n            window.location.href = \'");
#nullable restore
#line 281 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Home\Categorias.cshtml"
                               Write(Url.Action("Categorias", "Home"));

#line default
#line hidden
#nullable disable
                WriteLiteral("?id=\' + idProducto;\r\n        }\r\n\r\n        function Catego3() {\r\n            var idProducto = 3;\r\n            window.location.href = \'");
#nullable restore
#line 286 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Home\Categorias.cshtml"
                               Write(Url.Action("Categorias", "Home"));

#line default
#line hidden
#nullable disable
                WriteLiteral("?id=\' + idProducto;\r\n        }\r\n\r\n        function Catego4() {\r\n            var idProducto = 4;\r\n            window.location.href = \'");
#nullable restore
#line 291 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Home\Categorias.cshtml"
                               Write(Url.Action("Categorias", "Home"));

#line default
#line hidden
#nullable disable
                WriteLiteral(@"?id=' + idProducto;
        }
    </script>

    <div class=""area-contenido"">

        <div class=""Buscador-container"">
            <div class=""Buscador"">
                <input class=""Buscador-input"" type=""search"" placeholder=""Buscar..."" id=""buscador-input"" onkeyup=""buscar(event)""");
                BeginWriteAttribute("value", " value=\"", 9167, "\"", 9188, 1);
#nullable restore
#line 299 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Home\Categorias.cshtml"
WriteAttributeValue("", 9175, ViewBag.nomb, 9175, 13, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n            </div>\r\n        </div>\r\n\r\n        <br />\r\n        <br />\r\n\r\n        <div class=\"area-productos\">\r\n            <br />\r\n\r\n");
#nullable restore
#line 309 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Home\Categorias.cshtml"
             if (!Model.Any())
            {

#line default
#line hidden
#nullable disable
                WriteLiteral("                <div style=\"text-align: center; color: white; font-weight: bold; font-size: large;\">\r\n                    No hay elementos para mostrar\r\n                </div>\r\n");
#nullable restore
#line 314 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Home\Categorias.cshtml"
            }
            else
            {

#line default
#line hidden
#nullable disable
                WriteLiteral("                <div class=\"productos\" id=\"productos-container\">\r\n");
#nullable restore
#line 318 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Home\Categorias.cshtml"
                     foreach (var producto in Model)
                    {

#line default
#line hidden
#nullable disable
                WriteLiteral("                        <div class=\"producto\"");
                BeginWriteAttribute("onclick", " onclick=\"", 9786, "\"", 9831, 3);
                WriteAttributeValue("", 9796, "showIframe(\'", 9796, 12, true);
#nullable restore
#line 320 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Home\Categorias.cshtml"
WriteAttributeValue("", 9808, producto.ID_Producto, 9808, 21, false);

#line default
#line hidden
#nullable disable
                WriteAttributeValue("", 9829, "\')", 9829, 2, true);
                EndWriteAttribute();
                WriteLiteral(" style=\"display: none;\">\r\n");
#nullable restore
#line 321 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Home\Categorias.cshtml"
                              
                                string imageData = "";
                                if (producto.Imagen != null && producto.Imagen.Length > 0)
                                {
                                    string base64 = Convert.ToBase64String(producto.Imagen);
                                    string extension = "jpg"; // Extensión predeterminada

                                    // Determinar la extensión de la imagen
                                    if (producto.Imagen.Take(4).SequenceEqual(new byte[] { 137, 80, 78, 71 }))
                                    {
                                        extension = "png";
                                    }
                                    else if (producto.Imagen.Take(2).SequenceEqual(new byte[] { 255, 216 }))
                                    {
                                        extension = "jpeg";
                                    }

                                    // Crear el esquema data URI
                                    imageData = $"data:image/{extension};base64,{base64}";
                                }
                            

#line default
#line hidden
#nullable disable
                WriteLiteral("                            <center>\r\n                                <h2>");
#nullable restore
#line 343 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Home\Categorias.cshtml"
                               Write(producto.Nombre_Producto);

#line default
#line hidden
#nullable disable
                WriteLiteral("</h2>\r\n");
#nullable restore
#line 344 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Home\Categorias.cshtml"
                                 if (!string.IsNullOrEmpty(imageData))
                                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                                    <img");
                BeginWriteAttribute("src", " src=\"", 11315, "\"", 11331, 1);
#nullable restore
#line 346 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Home\Categorias.cshtml"
WriteAttributeValue("", 11321, imageData, 11321, 10, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("alt", " alt=\"", 11332, "\"", 11363, 1);
#nullable restore
#line 346 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Home\Categorias.cshtml"
WriteAttributeValue("", 11338, producto.Nombre_Producto, 11338, 25, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" width=\"60\" height=\"60\">\r\n");
#nullable restore
#line 347 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Home\Categorias.cshtml"
                                }
                                else
                                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                                    <strong>Imagen no disponible</strong>\r\n");
#nullable restore
#line 351 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Home\Categorias.cshtml"
                                }

#line default
#line hidden
#nullable disable
                WriteLiteral("                                <p>");
#nullable restore
#line 352 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Home\Categorias.cshtml"
                              Write(producto.Descripción);

#line default
#line hidden
#nullable disable
                WriteLiteral("</p>\r\n                            </center>\r\n");
#nullable restore
#line 354 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Home\Categorias.cshtml"
                             if (ViewData["Nomb"] != null)
                            {

#line default
#line hidden
#nullable disable
                WriteLiteral(@"                                <div class=""contenedor""></div>
                                <!-- CONTENEDOR DEL IFRAME -->
                                <div class=""iframe-container"" id=""iframeContainer"">
                                    <iframe id=""iframe""></iframe>
                                </div>
                                <script>
                                    window.addEventListener('message', function (event) {
                                        if (event.data.action === 'productoAgregado') {
                                            document.body.style.overflow = 'visible';
                                            document.getElementById('iframeContainer').style.display = 'none';
                                        }
                                    });

                                    function showIframe(idProducto) {
                                        document.getElementById('iframeContainer').style.display = 'flex';
                  ");
                WriteLiteral(@"                      document.body.style.overflow = 'hidden';
                                        document.getElementById('iframe').src = ""/Home/VerProducto?ID_Producto="" + idProducto;
                                    }

                                    function hideIframe() {
                                        document.getElementById('iframeContainer').style.display = 'none';
                                        document.body.style.overflow = 'visible';
                                        document.getElementById('iframe').src = ""/Home/VerProducto?ID_Producto=null"";
                                    }
                                </script>
");
#nullable restore
#line 381 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Home\Categorias.cshtml"
                            }

#line default
#line hidden
#nullable disable
                WriteLiteral("                        </div>\r\n");
#nullable restore
#line 383 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Home\Categorias.cshtml"
                    }

#line default
#line hidden
#nullable disable
                WriteLiteral("                </div>\r\n");
                WriteLiteral("                <div id=\"load-more-container\" style=\"text-align: center; margin-top: 20px;\">\r\n                    <center><button class=\"btn\" id=\"load-more-btn\" onclick=\"loadMoreProductos()\">Cargar más</button></center>\r\n                </div>\r\n");
#nullable restore
#line 389 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Home\Categorias.cshtml"
            }

#line default
#line hidden
#nullable disable
                WriteLiteral(@"        </div>

        <!-- iframe donde se cargará la segunda vista -->
        <iframe id=""frameCentro"" name=""centro"" style=""width: 100%; height: 500px; border: none;""></iframe>
    </div>

    <script>
        //Atras adelante
        let currentIndex = 0;
        const productsPerPage = 21;

        document.addEventListener('DOMContentLoaded', function () {
            loadMoreProductos();
        });

        function loadMoreProductos() {
            const productos = document.querySelectorAll('.producto');
            const loadMoreBtn = document.getElementById('load-more-btn');

            for (let i = currentIndex; i < currentIndex + productsPerPage; i++) {
                if (productos[i]) {
                    productos[i].style.display = '';
                }
            }

            currentIndex += productsPerPage;

            // Realizar la búsqueda después de cargar más productos
            realizarBusqueda();

            if (currentIndex >= productos.lengt");
                WriteLiteral(@"h) {
                loadMoreBtn.style.display = 'none';
            } else {
                loadMoreBtn.style.display = 'block';
            }
        }

        // Buscador
        document.addEventListener('DOMContentLoaded', function () {
            const buscadorInput = document.querySelector('.Buscador-input');

            // Función para realizar la búsqueda
            function realizarBusqueda() {
                const searchTerm = buscadorInput.value.trim().toLowerCase();
                const productos = document.querySelectorAll('.producto');

                productos.forEach(producto => {
                    let productoVisible = false;

                    if (searchTerm === '') {
                        productoVisible = true; // Mostrar todos los productos si el término de búsqueda está vacío
                    } else {
                        const nombreProducto = producto.querySelector('h2').innerText.trim().toLowerCase();
                        const descripcio");
                WriteLiteral(@"nProducto = producto.querySelector('p').innerText.trim().toLowerCase();

                        if (nombreProducto.includes(searchTerm) || descripcionProducto.includes(searchTerm)) {
                            productoVisible = true;
                        }
                    }

                    if (productoVisible) {
                        producto.style.display = '';
                    } else {
                        producto.style.display = 'none';
                    }
                });
            }

            // Llama a realizarBusqueda() una vez que se carga la página
            realizarBusqueda();

            // Agrega el evento keyup para realizar la búsqueda dinámica
            buscadorInput.addEventListener('keyup', function () {
                realizarBusqueda();
            });
        });
    </script>

    
    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n</html>\r\n\r\n\r\n\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<IPNMarket.Models.ProductosModel>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
