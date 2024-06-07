using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

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
