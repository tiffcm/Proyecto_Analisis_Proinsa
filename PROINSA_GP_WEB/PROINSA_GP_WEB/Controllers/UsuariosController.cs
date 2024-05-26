using Microsoft.AspNetCore.Mvc;

namespace PROINSA_GP_WEB.Controllers
{
    public class UsuariosController : Controller
    {
        [HttpGet]
        public IActionResult MiCuenta()
        {
            return View();
        }

        [HttpGet]
        public IActionResult RegistrarUsuario()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AdministrarUsuarios()
        {
            return View();
        }

        [HttpGet]
        public IActionResult MantenimientoUsuario()
        {
            return View();
        }
    }
}
