using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace PROINSA_GP_API.Controllers
{
    /// <summary>
    /// Este controlador se encargará de automatizar la captura de errores que presente el sistema 
    /// por medio de Middleware y enviará a la base de datos una copia de dicho error.
    /// </summary>
    /// <remarks>
    /// Se debe usar <see cref="ApiExplorerSettingsAttribute"/> con IgnoreApi porque solo es para 
    /// uso interno y no debe ser expuesto a la red cuando se ejecute.
    /// </remarks>
    /// <author>Brandon Ruiz Miranda</author>
    /// <version>1.0</version>
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("api/[controller]")][ApiController]
    public class ErrorController : ControllerBase
    {
        [Route("CatchException")]
        public IActionResult CatchException()
        {
            var error = HttpContext.Features.Get<IExceptionHandlerFeature>();            
             // Acá se debe poner un procedimiento que permita almacenar los errores en la base de datos.            
            return Problem(detail: "", title: "");
        }
    }
}
