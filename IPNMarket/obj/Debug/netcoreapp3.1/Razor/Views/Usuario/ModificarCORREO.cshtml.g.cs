#pragma checksum "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Usuario\ModificarCORREO.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "e6f35196115725f0ba55cc57ae2a49bb407ee15adc42fbb15a9606ca9826323a"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Usuario_ModificarCORREO), @"mvc.1.0.view", @"/Views/Usuario/ModificarCORREO.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA256", @"e6f35196115725f0ba55cc57ae2a49bb407ee15adc42fbb15a9606ca9826323a", @"/Views/Usuario/ModificarCORREO.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA256", @"6a2cee79ee38bdc425be605c12c6ce0927acdcfe755524434d2da9f4ecf9e942", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Usuario_ModificarCORREO : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("rel", new global::Microsoft.AspNetCore.Html.HtmlString("stylesheet"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("href", new global::Microsoft.AspNetCore.Html.HtmlString("~/css/CrearCuentaV3.css"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("type", new global::Microsoft.AspNetCore.Html.HtmlString("submit"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("btn btn-primary"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "ComprobarCORREO", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Usuario", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("form"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("method", "post", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<!DOCTYPE html>\r\n<html lang=\"es\">\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e6f35196115725f0ba55cc57ae2a49bb407ee15adc42fbb15a9606ca9826323a6816", async() => {
                WriteLiteral("\r\n    <meta charset=\"UTF-8\">\r\n    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">\r\n    <title>IPNMarket - Tu tienda virtual</title>\r\n    <link rel=\"stylesheet\" href=\"styles.css\">\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("link", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.SelfClosing, "e6f35196115725f0ba55cc57ae2a49bb407ee15adc42fbb15a9606ca9826323a7321", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"

    <style>

        body {
            background-color: rgb(27, 18, 18);
        }

        h1 {
            color: white;
            font-size: 32px; /* Ajusta el tamaño del título */
            margin-bottom: 30px; /* Aumenta el espacio debajo del título */
        }

        .form {
            display: flex;
            flex-direction: column;
            gap: 20px; /* Aumenta el espacio entre los elementos del formulario */
            background-color: rgb(52, 52, 52, 0.95);
            padding: 30px;
            width: 100%; /* Haz que el formulario ocupe todo el ancho disponible */
            max-width: 500px; /* Establece un ancho máximo para el formulario */
            border-radius: 20px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1); /* Agrega una sombra suave al formulario */
            margin: auto; /* Centra el formulario horizontalmente en la página */
            position: absolute;
            top: 50%;
            left: 50%;
            transform: ");
                WriteLiteral(@"translate(-50%, -50%);
        }

        ::placeholder {
            font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, 'Open Sans', 'Helvetica Neue', sans-serif;
        }

        .form button {
            align-self: flex-end;
        }

        .flex-column > label {
            color: white;
            font-weight: 600;
        }

        .inputForm {
            border: 1.5px solid white;
            border-radius: 10px;
            height: 50px;
            display: flex;
            align-items: center;
            padding-left: 10px;
            transition: 0.2s ease-in-out;
        }

            .inputForm path {
                fill: white;
            }

                .inputForm path:focus {
                    background-color: #8B0000;
                }

        .input {
            margin-left: 10px;
            border-radius: 10px;
            border: none;
            width: 85%;
            height: 100%;");
                WriteLiteral(@"
            background-color: transparent;
            color: white;
        }

            .input:focus {
                outline: none;
            }

        .inputForm:focus-within {
            border: 1.5px solid #8B0000;
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

        .span {
            font-size: 14px;
            margin-left: 5px;
            color: #8B0000;
            font-weight: 500;
            cursor: pointer;
        }

        .button-submit {
            margin: 20px 0 10px 0;
            border: 1px solid white;
            background-color: white;
            color: #8B0000;
            font-size: 15px;
            font-weight: 500;
       ");
                WriteLiteral(@"     border-radius: 10px;
            height: 50px;
            width: 100%;
            cursor: pointer;
        }

            .button-submit:hover {
                border: 1px solid #8B0000;
                background-color: #8B0000;
                color: white;
            }

        .p {
            text-align: center;
            color: white;
            font-size: 14px;
            margin: 5px 0;
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
            cursor: pointer;
            transition: 0.2s ease-in-out;
        }

            .btn:hover {
                border: 1px solid #8B0000;
                background-color: ");
                WriteLiteral(@"#8B0000;
                color: white;
            }

        .mensaje {
            color: white;
        }

        .logo-container {
            position: fixed;
            top: 10px; /* Distancia desde la parte superior */
            right: 10px; /* Distancia desde la parte derecha */
            z-index: 9999; /* Asegura que el contenedor esté por encima de otros elementos */
            background-color: rgb(52, 52, 52);
            border: 1px solid #8B0000;
            border-radius: 10px;
        }

        /* Estilo para la imagen */
        .logo-img {
            width: 100px; /* Tamaño de la imagen, ajusta según sea necesario */
            height: auto; /* Permite que la altura se ajuste automáticamente */
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
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e6f35196115725f0ba55cc57ae2a49bb407ee15adc42fbb15a9606ca9826323a14243", async() => {
                WriteLiteral(@"

    <ul class=""circles"">
        <li class=""red""></li>
        <li class=""white""></li>
        <li class=""red""></li>
        <li class=""white""></li>
        <li class=""red""></li>
        <li class=""white""></li>
        <li class=""white""></li>
        <li class=""white""></li>
        <li class=""red""></li>
        <li class=""red""></li>
    </ul>

    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e6f35196115725f0ba55cc57ae2a49bb407ee15adc42fbb15a9606ca9826323a14912", async() => {
                    WriteLiteral(@"

        <h1> MODIFICAR CORREO </h1>

        <center>
            <div class=""flex-column"">
                <label>Coloca tu correo y contraseña</label>
            </div>
        </center>
        
        <!-- CORREO -->
        <div class=""flex-column"">
            <label>Email </label>
        </div>
        <center>
            <div class=""inputForm"">
                <svg height=""20"" viewBox=""0 0 32 32"" width=""20"" xmlns=""http://www.w3.org/2000/svg""><g id=""Layer_3"" data-name=""Layer 3""><path d=""m30.853 13.87a15 15 0 0 0 -29.729 4.082 15.1 15.1 0 0 0 12.876 12.918 15.6 15.6 0 0 0 2.016.13 14.85 14.85 0 0 0 7.715-2.145 1 1 0 1 0 -1.031-1.711 13.007 13.007 0 1 1 5.458-6.529 2.149 2.149 0 0 1 -4.158-.759v-10.856a1 1 0 0 0 -2 0v1.726a8 8 0 1 0 .2 10.325 4.135 4.135 0 0 0 7.83.274 15.2 15.2 0 0 0 .823-7.455zm-14.853 8.13a6 6 0 1 1 6-6 6.006 6.006 0 0 1 -6 6z""></path></g></svg>
                <input name=""email"" type=""email"" class=""input"" placeholder=""Ingresa tu Correo""");
                    BeginWriteAttribute("value", " value=\"", 6594, "\"", 6621, 1);
#nullable restore
#line 212 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Usuario\ModificarCORREO.cshtml"
WriteAttributeValue("", 6602, ViewData["correo"], 6602, 19, false);

#line default
#line hidden
#nullable disable
                    EndWriteAttribute();
                    WriteLiteral(@" required readonly>
            </div>
        </center>
        
        <!-- CONTRASEÑA -->
        <div class=""flex-column"">
            <label>contraseña </label>
        </div>
        <center>
            <div class=""inputForm"">
                <svg height=""20"" viewBox=""-64 0 512 512"" width=""20"" xmlns=""http://www.w3.org/2000/svg""><path d=""m336 512h-288c-26.453125 0-48-21.523438-48-48v-224c0-26.476562 21.546875-48 48-48h288c26.453125 0 48 21.523438 48 48v224c0 26.476562-21.546875 48-48 48zm-288-288c-8.8125 0-16 7.167969-16 16v224c0 8.832031 7.1875 16 16 16h288c8.8125 0 16-7.167969 16-16v-224c0-8.832031-7.1875-16-16-16zm0 0""></path><path d=""m304 224c-8.832031 0-16-7.167969-16-16v-80c0-52.929688-43.070312-96-96-96s-96 43.070312-96 96v80c0 8.832031-7.167969 16-16 16s-16-7.167969-16-16v-80c0-70.59375 57.40625-128 128-128s128 57.40625 128 128v80c0 8.832031-7.167969 16-16 16zm0 0""></path></svg>
                <input name=""contraseña"" type=""password"" class=""input"" placeholder=""Ingresa tu Contraseña""");
                    WriteLiteral(@" onkeypress=""return (event.charCode >= 33 && event.charCode <= 47) || (event.charCode >= 58 && event.charCode <= 64) || (event.charCode >= 91 && event.charCode <= 96) || (event.charCode >= 123 && event.charCode <= 126) || (event.charCode >= 48 && event.charCode <= 57) || (event.charCode >= 65 && event.charCode <= 90) || (event.charCode >= 97 && event.charCode <= 122)"" minlength=""8"" maxlength=""25"" required>
            </div>
        </center>

        <div class=""mensaje""><strong>");
#nullable restore
#line 227 "E:\ESCUELA\6to_Semestre\Proyecto\IPNMarket\28_07_24\v1.2-beta\IPNMarket\IPNMarket\Views\Usuario\ModificarCORREO.cshtml"
                                Write(ViewData["mensajeC"]);

#line default
#line hidden
#nullable disable
                    WriteLiteral("</strong></div>\r\n\r\n        \r\n        <div class=\"flex-row\">\r\n            <button id=\"CANCELAR\" type=\"button\" class=\"btn btn-primary\">REGRESAR</button>\r\n            ");
                    __tagHelperExecutionContext = __tagHelperScopeManager.Begin("button", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "e6f35196115725f0ba55cc57ae2a49bb407ee15adc42fbb15a9606ca9826323a18734", async() => {
                        WriteLiteral("CONTINUAR");
                    }
                    );
                    __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormActionTagHelper>();
                    __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
                    __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_3);
                    __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.Action = (string)__tagHelperAttribute_4.Value;
                    __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
                    __Microsoft_AspNetCore_Mvc_TagHelpers_FormActionTagHelper.Controller = (string)__tagHelperAttribute_5.Value;
                    __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
                    await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                    if (!__tagHelperExecutionContext.Output.IsContentModified)
                    {
                        await __tagHelperExecutionContext.SetOutputContentAsync();
                    }
                    Write(__tagHelperExecutionContext.Output);
                    __tagHelperExecutionContext = __tagHelperScopeManager.End();
                    WriteLiteral("\r\n        </div>\r\n        \r\n\r\n    ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Method = (string)__tagHelperAttribute_7.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_7);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral(@"

    <script>
        document.addEventListener('DOMContentLoaded', function () {
            document.getElementById(""CANCELAR"").addEventListener(""click"", function () {
                var confirmacion = confirm(""¿Regresar?"");
                if (confirmacion) {
                    window.location.href = ""/Usuario/EditarPerfilUsuario"";
                }
            });
        });
    </script>

");
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
            WriteLiteral("\r\n\r\n</html>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591
