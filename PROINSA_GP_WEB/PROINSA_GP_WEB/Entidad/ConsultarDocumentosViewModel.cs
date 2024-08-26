using Microsoft.AspNetCore.Mvc.Rendering;

namespace PROINSA_GP_WEB.Entidad
{
    public class ConsultarDocumentosViewModel
    {
        public List<SelectListItem>? Empleados { get; set; }
        public List<Documento>? Documentos { get; set; }
        public long? EMPLEADO_ID { get; set; }
        public bool BusquedaRealizada { get; set; }
    }
}
