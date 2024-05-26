using Microsoft.AspNetCore.Mvc;

namespace PROINSA_GP_WEB.Controllers
{
    public class EncuestasController : Controller
    {
        [HttpGet]
        public IActionResult CrearEncuesta()
        {
            return View();
        }

        [HttpGet]
        public IActionResult LlenarEncuesta()
        {
            return View();
        }

        [HttpGet]
        public IActionResult InformeEncuesta()
        {
            return View();
        }
    }
}
