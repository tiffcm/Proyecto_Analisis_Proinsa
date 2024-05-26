using Microsoft.AspNetCore.Mvc;

namespace PROINSA_GP_WEB.Controllers
{
    public class DocumentosController : Controller
    {
        [HttpGet]
        public IActionResult DocusPlanos()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DocusPropios()
        {
            return View();
        }

        [HttpGet]
        public IActionResult GestionDocumentos()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DocumentoUsuario()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Documentoplano()
        {
            return View();
        }
    }
}
