using Microsoft.AspNetCore.Mvc;

namespace PROINSA_GP_WEB.Controllers
{
    public class ReportesController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult AgregarReporte()
        {
            return View();
        }

        [HttpGet]
        public IActionResult ModificarReporte()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DetallarReporte()
        {
            return View();
        }

        [HttpGet]
        public IActionResult EliminarReporte()
        {
            return View();
        }
    }
}
