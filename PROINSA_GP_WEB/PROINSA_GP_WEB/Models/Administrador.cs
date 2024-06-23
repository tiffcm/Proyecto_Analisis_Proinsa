using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace P_WebMartes.Models
{
    /// <summary>
    /// Es un filtro de seguridad para que solo las personas con rol administrador puedan acceder
    /// a ciertas acciones del controlador.
    /// </summary>
    public class Administrador : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;
            if (session.GetString("IdRol") != "1")
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary
                {
                    { "controller", "Home" },
                    { "action", "Principal" }
                });
            }
            base.OnActionExecuting(context);
        }
    }
}