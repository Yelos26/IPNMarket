#pragma checksum "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Productos\Carrusel.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "d43277891290c387f3338492afb5d4cfce91b79f4147f8f3c3498dea7dcbb84d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Productos_Carrusel), @"mvc.1.0.view", @"/Views/Productos/Carrusel.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA256", @"d43277891290c387f3338492afb5d4cfce91b79f4147f8f3c3498dea7dcbb84d", @"/Views/Productos/Carrusel.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA256", @"6a2cee79ee38bdc425be605c12c6ce0927acdcfe755524434d2da9f4ecf9e942", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Productos_Carrusel : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<IPNMarket.Models.CarruselModel>>
    #nullable disable
    {
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
            WriteLiteral("\r\n<!DOCTYPE html>\r\n<html lang=\"en\">\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d43277891290c387f3338492afb5d4cfce91b79f4147f8f3c3498dea7dcbb84d3583", async() => {
                WriteLiteral(@"
    <meta charset=""UTF-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"">
    <title>Lista de Productos</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background-color: rgb(27, 18, 18);
        }

        .container {
            max-width: 800px;
            width: 100%;
            margin: auto;
            padding: 20px;
            background-color: rgb(52, 52, 52, 0.95);
            border-radius: 20px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
            position: absolute;
            top: 50%;
            left: 50%;
            transform: translate(-50%, -50%);
            gap: 20px;
            display: flex;
            flex-direction: column;
        }

        h1 {
            margin-top: 0;
            color: white;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            border-spacin");
                WriteLiteral(@"g: 0;
            margin-top: 20px;
        }

        th,
        td {
            padding: 12px;
            text-align: left;
            border-bottom: 1px solid white;
            text-align: center;
            margin: 0 auto;
        }

        td{
            color: white;
        }

        th {
            background-color: #f2f2f2;
        }

        img {
            height: 60px; /* Fijar la altura a 60px */
            width: auto; /* Ancho automático */
            border-radius: 5px;
            box-shadow: 0 0 5px rgba(0, 0, 0, 0.1);
            margin: 0 auto;
        }

        .btn {
            margin-top: 10px;
            width: 100%;
            height: 50px;
            border-radius: 10px;
            display: flex;
            justify-content: center;
            align-items: center;
            font-weight: 500;
            gap: 10px;
            border: 1px solid white;
            background-color: white;
            color: #8B0000;
     ");
                WriteLiteral(@"       cursor: pointer;
            transition: 0.2s ease-in-out;
        }

            .btn:hover {
                border: 1px solid #8B0000;
                background-color: #8B0000;
                color: white;
            }

        .flex-row {
            display: flex;
            flex-direction: row;
            align-items: center;
            gap: 10px;
            justify-content: space-between;
        }

            .flex-row > div > label {
                font-size: 14px;
                color: black;
                font-weight: 400;
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
            WriteLiteral("\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d43277891290c387f3338492afb5d4cfce91b79f4147f8f3c3498dea7dcbb84d7310", async() => {
                WriteLiteral("\r\n    <div class=\"container\">\r\n        <center><h1>IMAGENES DEL CARRUSEL</h1></center>\r\n        \r\n        <div class=\"flex-row\">\r\n            <button class=\"btn btn-secondary\"");
                BeginWriteAttribute("onclick", " onclick=\"", 2940, "\"", 2993, 3);
                WriteAttributeValue("", 2950, "location.href=\'", 2950, 15, true);
#nullable restore
#line 116 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Productos\Carrusel.cshtml"
WriteAttributeValue("", 2965, Url.Action("Home", "Home"), 2965, 27, false);

#line default
#line hidden
#nullable disable
                WriteAttributeValue("", 2992, "\'", 2992, 1, true);
                EndWriteAttribute();
                WriteLiteral(">Regresar</button>\r\n            <button class=\"btn btn-primary\"");
                BeginWriteAttribute("onclick", " onclick=\"", 3057, "\"", 3129, 3);
                WriteAttributeValue("", 3067, "location.href=\'", 3067, 15, true);
#nullable restore
#line 117 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Productos\Carrusel.cshtml"
WriteAttributeValue("", 3082, Url.Action("AgregarIMGCarrusel", "Productos"), 3082, 46, false);

#line default
#line hidden
#nullable disable
                WriteAttributeValue("", 3128, "\'", 3128, 1, true);
                EndWriteAttribute();
                WriteLiteral(@">Agregar</button>
        </div>

        <table>
            <thead>
                <tr>
                    <th>Imagen</th>
                    <th>Controlador</th>
                    <th>Acción</th>
                    <th>Editar</th>
                    <th>Eliminar</th>
                </tr>
            </thead>
            <tbody>
");
#nullable restore
#line 131 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Productos\Carrusel.cshtml"
                 foreach (var carru in Model)
                {

#line default
#line hidden
#nullable disable
                WriteLiteral("                    <tr>\r\n                        <td>\r\n");
#nullable restore
#line 135 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Productos\Carrusel.cshtml"
                              
                                string imageData = "";
                                if (carru.Imagen != null && carru.Imagen.Length > 0)
                                {
                                    string base64 = Convert.ToBase64String(carru.Imagen);
                                    string extension = "jpg"; // Extensión predeterminada
                                    if (carru.Imagen.Take(4).SequenceEqual(new byte[] { 137, 80, 78, 71 }))
                                    {
                                        extension = "png";
                                    }
                                    else if (carru.Imagen.Take(2).SequenceEqual(new byte[] { 255, 216 }))
                                    {
                                        extension = "jpeg";
                                    }
                                    imageData = $"data:image/{extension};base64,{base64}";
                                }
                            

#line default
#line hidden
#nullable disable
#nullable restore
#line 152 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Productos\Carrusel.cshtml"
                             if (!string.IsNullOrEmpty(imageData))
                            {

#line default
#line hidden
#nullable disable
                WriteLiteral("                                <img");
                BeginWriteAttribute("src", " src=\"", 4784, "\"", 4800, 1);
#nullable restore
#line 154 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Productos\Carrusel.cshtml"
WriteAttributeValue("", 4790, imageData, 4790, 10, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                BeginWriteAttribute("alt", " alt=\"", 4801, "\"", 4825, 1);
#nullable restore
#line 154 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Productos\Carrusel.cshtml"
WriteAttributeValue("", 4807, carru.ID_Carrusel, 4807, 18, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(">\r\n");
#nullable restore
#line 155 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Productos\Carrusel.cshtml"
                            }
                            else
                            {

#line default
#line hidden
#nullable disable
                WriteLiteral("                                <span>No disponible</span>\r\n");
#nullable restore
#line 159 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Productos\Carrusel.cshtml"
                            }

#line default
#line hidden
#nullable disable
                WriteLiteral("                        </td>\r\n                        <td>");
#nullable restore
#line 161 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Productos\Carrusel.cshtml"
                       Write(carru.Controlador);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                        <td>");
#nullable restore
#line 162 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Productos\Carrusel.cshtml"
                       Write(carru.Accion);

#line default
#line hidden
#nullable disable
                WriteLiteral("</td>\r\n                        <td>\r\n                            <button class=\"btn btn-primary\"");
                BeginWriteAttribute("onclick", " onclick=\"", 5237, "\"", 5286, 3);
                WriteAttributeValue("", 5247, "editarIMGCarrusel(\'", 5247, 19, true);
#nullable restore
#line 164 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Productos\Carrusel.cshtml"
WriteAttributeValue("", 5266, carru.ID_Carrusel, 5266, 18, false);

#line default
#line hidden
#nullable disable
                WriteAttributeValue("", 5284, "\')", 5284, 2, true);
                EndWriteAttribute();
                WriteLiteral(">Editar</button>\r\n                        </td>\r\n                        <td>\r\n                            <button class=\"btn btn-primary\"");
                BeginWriteAttribute("onclick", " onclick=\"", 5425, "\"", 5465, 3);
                WriteAttributeValue("", 5435, "Eliminar(\'", 5435, 10, true);
#nullable restore
#line 167 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Productos\Carrusel.cshtml"
WriteAttributeValue("", 5445, carru.ID_Carrusel, 5445, 18, false);

#line default
#line hidden
#nullable disable
                WriteAttributeValue("", 5463, "\')", 5463, 2, true);
                EndWriteAttribute();
                WriteLiteral(">Eliminar</button>\r\n                        </td>\r\n                    </tr>\r\n");
#nullable restore
#line 170 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Productos\Carrusel.cshtml"
                }

#line default
#line hidden
#nullable disable
                WriteLiteral("            </tbody>\r\n        </table>\r\n    </div>\r\n\r\n    <script>\r\n        function editarIMGCarrusel(idProducto) {\r\n            window.location.href = \'");
#nullable restore
#line 177 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Productos\Carrusel.cshtml"
                               Write(Url.Action("EditarIMGCarrusel", "Productos"));

#line default
#line hidden
#nullable disable
                WriteLiteral("?id=\' + idProducto;\r\n        }\r\n\r\n        function Eliminar(idProducto) {\r\n            if (confirm(\"¿Seguro que desea eliminar este Imagen del carrusel?\")) {\r\n                if (confirm(\"¿Seguro?\")) {\r\n                    window.location.href = \'");
#nullable restore
#line 183 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Productos\Carrusel.cshtml"
                                       Write(Url.Action("EliminarIMGCarrusel", "Productos"));

#line default
#line hidden
#nullable disable
                WriteLiteral("?id=\' + idProducto;\r\n                }\r\n            }\r\n        }\r\n    </script>\r\n\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</html>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<IPNMarket.Models.CarruselModel>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
