﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Primitives;
using PROINSA_GP_WEB.Entidad;

namespace PROINSA_GP_WEB.Servicios
{
    /// <summary>
    /// Es la interfaz del modelo de Usuario que se expone a la red.
    /// </summary>
    /// <version>1.2</version>
    public interface IUsuarioModel
    {
        Respuesta? ConsultarDatosEmpleado(string correo);
        Respuesta? ActualizarDatosUsuario(Usuario entidad);
        Respuesta? ObtenerTelefonosUsuario(long? idEmpleado);
        Respuesta? MostrarInfoVistaAdmin();
        Respuesta? EditarDatosVistaAdmin(Usuario usuario);
        Respuesta? MostrarEmpleadoVistaAdmin(long? idEmpleado);
        Respuesta? CambiarEstadoUsuarioAdmin(long? idEmpleado);
        List<SelectListItem> MostrarTodosCargos();
        List<SelectListItem> MostrarTodosHorarios();
        List<SelectListItem> MostrarTodosRoles();
        List<SelectListItem> MostrarTodosDepartamentos();        
    }
}
