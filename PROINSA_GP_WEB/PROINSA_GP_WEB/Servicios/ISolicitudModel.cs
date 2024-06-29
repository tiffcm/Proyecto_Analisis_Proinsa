using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Servicios;
using System.Configuration;
using System.Data;
namespace PROINSA_GP_WEB.Servicios
{
    /// <summary>
    /// Es la interfaz del modelo de Solicitudes
    /// </summary>
    public interface ISolicitudModel
    {
        //Para registro solicitudes permiso, vacaciones.
        Respuesta? RegistrarSolicitud(Solicitud ent);
        List<SelectListItem> ObtenerTipoSolicitud();
        DataTable ObtenerSolicitudesEmpleado(int idEmpleado);
        Respuesta? ObtenerHorarioEmpleado(string correo);
        List<SelectListItem> ObtenerHorarioDisponibles(int idEmpleado);
        // para registro cambio de horario
        Respuesta? RegistrarSolicitudCambioHorario(Solicitud ent);
    }
}
