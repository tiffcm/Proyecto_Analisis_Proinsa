using PROINSA_GP_WEB.Entidad;
using System.Data;

namespace PROINSA_GP_WEB.Servicios
{
    public interface IDocumentoModel
    {
        Respuesta? RegistrarDocumento(Documento entidad);
        Respuesta ConsultarTiposDocumento();
        DataTable ConsultarDocumentosEmpleado(int idEmpleado);
    }
}