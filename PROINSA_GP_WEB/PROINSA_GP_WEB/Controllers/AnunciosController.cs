using Microsoft.AspNetCore.Mvc;
using P_WebMartes.Models;
using PROINSA_GP_WEB.Models;

namespace PROINSA_GP_WEB.Controllers
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class AnunciosController : Controller
    {
        [Seguridad]
        [Administrador]
        [HttpGet]
        public IActionResult Anuncios()
        {
            return View();
        }

        [Seguridad]
        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [Seguridad]
        [HttpGet]
        public IActionResult DetalleEvento()
        {
            return View();
        }

        [Seguridad]
        [HttpGet]
        public IActionResult Mensajes()
        {
            return View();
        }
    }
}
