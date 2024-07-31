using PROINSA_GP_WEB.Entidad;
using System.Data;

namespace PROINSA_GP_WEB.Servicios
{
    public interface IDocumentoModel
    {
        Respuesta? RegistrarDocumento(Documento entidad);
        Respuesta ConsultarTiposDocumento();
        Respuesta ConsultarEmpleados();
        Respuesta ConsultarDocumentosEmpleado(long EMPLEADO_ID);
        Respuesta ConsultarDocumentoEmpleado(long ID_EMPLEADODOCUMENTO);
        Respuesta EliminarDocumento(long ID_EMPLEADODOCUMENTO);
        Respuesta? ActualizarDocumento(Documento entidad);
    }
}