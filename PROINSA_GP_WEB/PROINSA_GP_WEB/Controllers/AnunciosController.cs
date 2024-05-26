using Microsoft.AspNetCore.Mvc;

namespace PROINSA_GP_WEB.Controllers
{
    public class AnunciosController : Controller
    {
        [HttpGet]
        public IActionResult Anuncios()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Registro()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DetalleEvento()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Mensajes()
        {
            return View();
        }
    }
}
