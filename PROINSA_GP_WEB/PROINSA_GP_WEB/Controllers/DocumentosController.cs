using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using P_WebMartes.Models;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Models;
using PROINSA_GP_WEB.Servicios;
using System.Data;
using System.Text.Json;

namespace PROINSA_GP_WEB.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [Authorize]
    public class DocumentosController(IDocumentoModel iDocumentoModel) : Controller
    {
        [Seguridad]
        [HttpGet]
        public IActionResult RegistrarDocumento()
        {
            var tiposDocumentos = iDocumentoModel.ConsultarTiposDocumento();
            ViewBag.tiposDocumentos = JsonSerializer.Deserialize<List<SelectListItem>>((JsonElement)tiposDocumentos.CONTENIDO!);
            return View();
        }

        [Seguridad]
        [HttpPost]
        public IActionResult RegistrarDocumento(Documento entidad)
        {
            long? IdEmpleado = HttpContext.Session.GetInt32("ID_EMPLEADO");
            IFormFile archivo = Request.Form.Files["subirDocumento"]!;
            entidad.DOCUMENTO = ConvertirPDFBytes(archivo);
            string base64 = Convert.ToBase64String(entidad!.DOCUMENTO!);
            entidad.VER_DOCUMENTO = $"data:image/pdf;base64,{base64}";
            entidad.EMPLEADO_ID = IdEmpleado;
            var respuesta = iDocumentoModel.RegistrarDocumento(entidad);
            return RedirectToAction("HistorialDocumentos", "Documentos");
        }

        [Seguridad]
        [HttpGet]
        public IActionResult RegistrarDocumentoAdmin()
        {
            var tiposDocumentos = iDocumentoModel.ConsultarTiposDocumento();
            ViewBag.tiposDocumentos = JsonSerializer.Deserialize<List<SelectListItem>>((JsonElement)tiposDocumentos.CONTENIDO!);
            var empleadosEmpresa = iDocumentoModel.ConsultarEmpleados();
            ViewBag.empleadosEmpresa = JsonSerializer.Deserialize<List<SelectListItem>>((JsonElement)empleadosEmpresa.CONTENIDO!);
            return View();
        }

        [Seguridad]
        [HttpPost]
        public IActionResult RegistrarDocumentoAdmin(Documento entidad)
        {
            IFormFile archivo = Request.Form.Files["subirDocumento"]!;
            entidad.DOCUMENTO = ConvertirPDFBytes(archivo);
            string base64 = Convert.ToBase64String(entidad!.DOCUMENTO!);
            entidad.VER_DOCUMENTO = $"data:image/pdf;base64,{base64}";
            var respuesta = iDocumentoModel.RegistrarDocumento(entidad);
            return RedirectToAction("ConsultarDocumentos", "Documentos");
        }

        [Seguridad]
        [HttpGet]
        public IActionResult HistorialDocumentos()
        {
            var idEmpleadoSession = HttpContext.Session.GetInt32("ID_EMPLEADO");
            int idEmpleado = idEmpleadoSession ?? 0;
            var documentos = iDocumentoModel.ConsultarDocumentosEmpleado(idEmpleado)!;
            if (documentos.CODIGO == 1)
            {
                var datos = JsonSerializer.Deserialize<List<Documento>>((JsonElement)documentos.CONTENIDO!);
                foreach (var item in datos!)
                {
                    string base64 = Convert.ToBase64String(item.DOCUMENTO!);
                    item.VER_DOCUMENTO = $"data:application/pdf;base64,{base64}";
                }
                return View(datos);
            }
            return View(new List<Documento>());
        }

        [Seguridad]
        [HttpGet]
        public IActionResult ConsultarDocumentos(long? empleadoId)
        {
            var respuesta = iDocumentoModel.ConsultarEmpleados();
            if (respuesta.CODIGO == 1)
            {
                var empleados = JsonSerializer.Deserialize<List<SelectListItem>>((JsonElement)respuesta.CONTENIDO!);
                var viewModel = new ConsultarDocumentosViewModel
                {
                    Empleados = empleados,
                    Documentos = new List<Documento>(),
                    EMPLEADO_ID = empleadoId ?? 0
                };

                if (empleadoId.HasValue)
                {
                    var documentos = iDocumentoModel.ConsultarDocumentosEmpleado(empleadoId.Value);
                    if (documentos.CODIGO == 1)
                    {
                        var datos = JsonSerializer.Deserialize<List<Documento>>((JsonElement)documentos.CONTENIDO!);
                        foreach (var item in datos!)
                        {
                            string base64 = Convert.ToBase64String(item.DOCUMENTO!);
                            item.VER_DOCUMENTO = $"data:application/pdf;base64,{base64}";
                        }
                        viewModel.Documentos = datos!;
                    }
                    viewModel.BusquedaRealizada = true;
                }

                return View(viewModel);
            }
            return RedirectToAction("Principal", "Home");
        }

        [Seguridad]
        [HttpPost]
        public IActionResult ConsultarDocumentos(ConsultarDocumentosViewModel model)
        {
            if (model.EMPLEADO_ID > 0)
            {
                return RedirectToAction("ConsultarDocumentos", new { empleadoId = model.EMPLEADO_ID });
            }

            return RedirectToAction("ConsultarDocumentos");
        }

        [Seguridad]
        [HttpGet]
        public IActionResult EliminarDocumento(long q)
        {
            var respuesta = iDocumentoModel.EliminarDocumento(q);
            if (respuesta.CODIGO == 1)
                return RedirectToAction("HistorialDocumentos", "Documentos");
            else
                return View();
        }

        [Seguridad]
        [HttpGet]
        public IActionResult EliminarDocumentoAdmin(long q)
        {
            var respuesta = iDocumentoModel.EliminarDocumento(q);
            if (respuesta.CODIGO == 1)
                return RedirectToAction("ConsultarDocumentos", "Documentos");
            else
                return View();
        }

        [Seguridad]
        [HttpGet]
        public IActionResult ActualizarDocumento(long q)
        {
            var tiposDocumentos = iDocumentoModel.ConsultarTiposDocumento();
            ViewBag.tiposDocumentos = JsonSerializer.Deserialize<List<SelectListItem>>((JsonElement)tiposDocumentos.CONTENIDO!);
            var resultado = iDocumentoModel.ConsultarDocumentoEmpleado(q);
            if (resultado.CODIGO == 1)
            {
                var documento = JsonSerializer.Deserialize<Documento>((JsonElement)resultado.CONTENIDO!);
                string base64 = Convert.ToBase64String(documento!.DOCUMENTO!);
                documento.VER_DOCUMENTO = $"data:application/pdf;base64,{base64}";
                return View(documento);
            }
            return RedirectToAction("HistorialDocumentos", "Documentos");
        }

        [Seguridad]
        [HttpPost]
        public IActionResult ActualizarDocumento(Documento entidad)
        {
            IFormFile archivo = Request.Form.Files["subirDocumento"]!;
            if (archivo != null)
            {
                entidad.DOCUMENTO = ConvertirPDFBytes(archivo);
                string base64 = Convert.ToBase64String(entidad!.DOCUMENTO!);
                entidad.VER_DOCUMENTO = $"data:image/pdf;base64,{base64}";
                var respuesta = iDocumentoModel.ActualizarDocumento(entidad);
                if (respuesta!.CODIGO == 1)
                    return RedirectToAction("HistorialDocumentos", "Documentos");
            }
            else
            {
                var respuesta = iDocumentoModel.ActualizarDocumento(entidad);
                if (respuesta!.CODIGO == 1)
                    return RedirectToAction("HistorialDocumentos", "Documentos");
            }
            return View();
        }

        [Seguridad]
        [HttpGet]
        public IActionResult ActualizarDocumentoAdmin(long q)
        {
            var tiposDocumentos = iDocumentoModel.ConsultarTiposDocumento();
            ViewBag.tiposDocumentos = JsonSerializer.Deserialize<List<SelectListItem>>((JsonElement)tiposDocumentos.CONTENIDO!);
            var resultado = iDocumentoModel.ConsultarDocumentoEmpleado(q);
            if (resultado.CODIGO == 1)
            {
                var documento = JsonSerializer.Deserialize<Documento>((JsonElement)resultado.CONTENIDO!);
                string base64 = Convert.ToBase64String(documento!.DOCUMENTO!);
                documento.VER_DOCUMENTO = $"data:application/pdf;base64,{base64}";
                return View(documento);
            }
            return RedirectToAction("ConsultarDocumentos", "Documentos");
        }


        [Seguridad]
        [HttpPost]
        public IActionResult ActualizarDocumentoAdmin(Documento entidad)
        {
            IFormFile archivo = Request.Form.Files["subirDocumento"]!;
            if (archivo != null)
            {
                entidad.DOCUMENTO = ConvertirPDFBytes(archivo);
                string base64 = Convert.ToBase64String(entidad!.DOCUMENTO!);
                entidad.VER_DOCUMENTO = $"data:image/pdf;base64,{base64}";
                var respuesta = iDocumentoModel.ActualizarDocumento(entidad);
                if (respuesta!.CODIGO == 1)
                    return RedirectToAction("ConsultarDocumentos", "Documentos");
            }
            else
            {
                var respuesta = iDocumentoModel.ActualizarDocumento(entidad);
                if (respuesta!.CODIGO == 1)
                    return RedirectToAction("ConsultarDocumentos", "Documentos");
            }
            return View();
        }

        private byte[] ConvertirPDFBytes(IFormFile pdf)
        {
            using (var ms = new MemoryStream())
            {
                pdf.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
