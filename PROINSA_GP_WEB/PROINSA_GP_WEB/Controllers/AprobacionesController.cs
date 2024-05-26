using Microsoft.AspNetCore.Mvc;

namespace PROINSA_GP_WEB.Controllers
{
    public class AprobacionesController : Controller
    {
        [HttpGet]
        public IActionResult Aprobaciones()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AproVacaciones()
        {
            return View();
        }
    }
}
