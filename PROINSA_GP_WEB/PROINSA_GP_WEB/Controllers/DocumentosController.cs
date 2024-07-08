using Microsoft.AspNetCore.Mvc;
using P_WebMartes.Models;
using PROINSA_GP_WEB.Models;

namespace PROINSA_GP_WEB.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class DocumentosController : Controller
    {
        // SE DEBE REALIZAR LA VALIDACION DE VISTAS PARA USUARIO Y ADMIN
        [Seguridad]
        [HttpGet]
        public IActionResult DocusPlanos()
        {
            return View();
        }

        [Seguridad]
        [HttpGet]
        public IActionResult DocusPropios()
        {
            return View();
        }

        // ESTOS 2 DE ACA ABAJO CREO QUE NO SE VAN A NECESITAR SE HARIAN SUBOPCIONES EN EL MENU (PLANOS Y PROPIOS/USUARIOS)
        //[HttpGet]
        //public IActionResult GestionMisDocumentos()
        //{
        //    return View();
        //}

        //[Seguridad]
        //[Administrador]
        //[HttpGet]
        //public IActionResult GestionDocumentos()
        //{
        //    return View();
        //}

        [Seguridad]
        [Administrador]
        [HttpGet]
        public IActionResult DocumentoUsuario()
        {
            return View();
        }

        [Seguridad]
        [Administrador]
        [HttpGet]
        public IActionResult Documentoplano()
        {
            return View();
        }

        
    }
}
