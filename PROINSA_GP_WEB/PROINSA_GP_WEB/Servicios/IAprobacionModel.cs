using PROINSA_GP_WEB.Entidad;
using System.Data;

namespace PROINSA_GP_WEB.Servicios
{
    public interface IAprobacionModel
    {
        Respuesta? ObtenerSolicitudesEmpleado(long? idEmpleado);
        Respuesta? ObtenerAprobacionPendienteDetalle(long? idEmpleado, long ID_SOLICITUD);
        Respuesta? ObtenerAprobacionFlujo(long ID_SOLICITUD);
        Respuesta? ActualizarApro(ActualizacionAprobacion entidad);
    }
}
