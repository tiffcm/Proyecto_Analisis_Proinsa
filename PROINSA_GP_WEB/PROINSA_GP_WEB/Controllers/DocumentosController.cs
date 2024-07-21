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
    public class DocumentosController (IDocumentoModel iDocumentoModel) : Controller
    {        
        [Seguridad][HttpGet]
        public IActionResult RegistrarDocumento()
        {
            var tiposDocumentos = iDocumentoModel.ConsultarTiposDocumento();
            ViewBag.tiposDocumentos = JsonSerializer.Deserialize<List<SelectListItem>>((JsonElement)tiposDocumentos.CONTENIDO!);
            return View();
        }

        [Seguridad][HttpPost]
        public IActionResult RegistrarDocumento(Documento entidad)
        {
            long? IdEmpleado = HttpContext.Session.GetInt32("ID_EMPLEADO");
            IFormFile archivo = Request.Form.Files["subirDocumento"]!;
            entidad.DOCUMENTO = ConvertirPDFBytes(archivo);
            string base64 = Convert.ToBase64String(entidad!.DOCUMENTO!);            
            entidad.VER_DOCUMENTO = $"data:image/pdf;base64,{base64}";
            entidad.EMPLEADO_ID = IdEmpleado;
            var respuesta = iDocumentoModel.RegistrarDocumento(entidad);
            return RedirectToAction("Principal","Home");
        }

        [Seguridad][HttpGet]
        public IActionResult HistorialDocumentos()
        {
            var idEmpleadoSession = HttpContext.Session.GetInt32("ID_EMPLEADO");
            int idEmpleado = idEmpleadoSession ?? 0;
            DataTable documentos = iDocumentoModel.ConsultarDocumentosEmpleado(idEmpleado)!;
            return View(documentos);            
        }

        [Seguridad][HttpGet]
        public IActionResult ConsultarDocumentos()
        {
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
