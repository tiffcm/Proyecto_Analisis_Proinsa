using PROINSA_GP_WEB.Entidad;

namespace PROINSA_GP_WEB.Servicios
{
    // Interfaz de empleadoModel para exponer la mínima información a la red
    public interface IEmpleadoModel
    {
        public EmpleadoRespuesta? ConsultarEmpleado();
    }
}
