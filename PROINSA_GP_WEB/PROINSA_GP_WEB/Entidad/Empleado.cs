namespace PROINSA_GP_WEB.Entidad
{
    //Falta arreglar, solo traer los datos que se necesitan, no los datos de la tabla
    public class Empleado
    {
        public long ID_EMPLEADO { get; set; }
        public int? IDENTIFICACION { get; set; }
        public string? NOMBRECOMPLETO { get; set; }
        public DateTime? FECHA_NACIMIENTO { get; set; }
        public DateTime? FECHA_INGRESO { get; set; }
        public string? FOTO { get; set; }
        public double? SALARIO { get; set; }
        public int? SALDO_VACACIONES { get; set; }
        public string? NOMBRE_CARGO { get; set; }
        public string? CORREO { get; set; }
        public long? HORARIOLABORAL_ID { get; set; }
        public string? NOMBRE_DEPARTAMENTO { get; set; }
        public string? NOMBREROL { get; set; }
    }
    public class EmpleadoRespuesta
    {
        public EmpleadoRespuesta()
        {
            CODIGO = "00";
            MENSAJE = string.Empty;
            DATO = null;
            DATOS = null;
        }

        public string CODIGO { get; set; }
        public string? MENSAJE { get; set; }
        public Empleado? DATO { get; set; }
        public List<Empleado>? DATOS { get; set; }
    }
}

