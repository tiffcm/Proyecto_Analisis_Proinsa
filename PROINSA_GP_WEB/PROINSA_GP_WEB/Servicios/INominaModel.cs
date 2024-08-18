using PROINSA_GP_WEB.Entidad;

namespace PROINSA_GP_WEB.Servicios
{
    public interface INominaModel
    {
        Respuesta? RegistrarNomina(Nomina entidad);
        Respuesta? CalculoNominaInicial(DateTime Fecha);
        Respuesta? CalculoNominaFinal(DateTime Fecha);
        Respuesta? ConsultarTiposNomina();
        Respuesta? ObtenerIngresos();
        Respuesta? ObtenerDeducciones();
        Respuesta? RegistrarIngresosNominaDetalle(List<IngresoNominaDetalle> entidad);
        Respuesta? RegistrarDeduccionNominaDetalle(List<DeduccionNominaDetalle> entidad);
        Respuesta? ObtenerNominaEmpleado(int EMPLEADO_ID);
        Respuesta? ObtenerNominaMensualEmpleados(DateTime fechapago);
        Respuesta? RevisionNomina(Nomina entidad);
        Respuesta? AprobacionNomina(Nomina entidad);
        Respuesta? ConsultarNombreEmpleado(long ID_EMPLEADO);
    }
}
