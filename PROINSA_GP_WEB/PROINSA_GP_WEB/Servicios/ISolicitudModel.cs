using Microsoft.Extensions.Configuration;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Servicios;
using System.Configuration;
namespace PROINSA_GP_WEB.Servicios
{
    /// <summary>
    /// Es la interfaz del modelo de Solicitudes
    /// </summary>
    public interface ISolicitudModel
    {
        Respuesta? RegistrarSolicitud(Solicitud ent);
    }
}
