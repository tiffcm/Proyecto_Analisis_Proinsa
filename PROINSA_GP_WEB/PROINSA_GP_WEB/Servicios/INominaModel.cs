using PROINSA_GP_WEB.Entidad;

namespace PROINSA_GP_WEB.Servicios
{
    public interface INominaModel
    {
		// CONFIGURACIONES GENERALES NÓMINA
		Respuesta? RegistrarNomina(Nomina entidad);
        Respuesta? CalculoNominaInicial(DateTime Fecha);
        Respuesta? CalculoNominaFinal(DateTime Fecha);
		Respuesta? RevisionNomina(Nomina entidad);
		Respuesta? AprobacionNomina(Nomina entidad);

		// GESTIÓN DE INGRESOS		
        Respuesta? ObtenerIngresos();
		Respuesta? ObtenerIngresoDetalle(long INGRESO_ID);
        Respuesta? RegistrarIngresosNominaDetalle(List<IngresoNominaDetalle> entidad);
		Respuesta? ActualizarIngresoNomina(IngresosDeduccionesDetalle entidad);
		Respuesta EliminarIngresoEmpleado(long ID_INGRESONOMINADETALLE);

		// GESTIÓN DE DEDUCCIONES
		Respuesta? ObtenerDeducciones();
		Respuesta? ObtenerDeduccionDetalle(long DEDUCCION_ID);
        Respuesta? RegistrarDeduccionNominaDetalle(List<DeduccionNominaDetalle> entidad);
		Respuesta? ActualizarDeduccionNomina(IngresosDeduccionesDetalle entidad);
		Respuesta EliminarDeduccionEmpleado(long ID_DEDUCCION_NOMINADETALLE);

		// CONSULTAS GENERALES DE NÓMINA
		Respuesta? ObtenerNominaEmpleado(int EMPLEADO_ID);
        Respuesta? ObtenerNominaMensualEmpleados(DateTime fechapago);
        Respuesta? ConsultarNombreEmpleado(long ID_EMPLEADO);
		Respuesta? ConsultarTiposNomina();
	}
}
