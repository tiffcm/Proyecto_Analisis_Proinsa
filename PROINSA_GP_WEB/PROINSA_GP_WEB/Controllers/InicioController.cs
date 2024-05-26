using Microsoft.AspNetCore.Mvc;

namespace PROINSA_GP_WEB.Controllers
{
    public class InicioController : Controller
    {
        [HttpGet]
        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RecuperarContrasenna()
        {
            return View();
        }
    }
}
