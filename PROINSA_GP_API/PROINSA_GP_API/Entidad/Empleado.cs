namespace PROINSA_GP_API.Entidad
{
    public class Usuario
    {
        public long ID_EMPLEADO {  get; set; }
        public string? IDENTIFICACION { get; set; }
        public string? NOMBRECOMPLETO { get; set; }
        public string? FECHA_NACIMIENTO { get; set; }
        public string? FECHA_INGRESO { get; set; }
        public string? FOTO { get; set; }
        public double? SALARIO { get; set; }
        public double? SALDO_VACACIONES { get; set; }
        public double? ESTADO { get; set; }
        public double? TEMPORAL { get; set; }
        public double? VENCIMIENTO { get; set; }
        public double? CARGO_ID { get; set; }
        public double? ROL_ID { get; set; }
        public double? CORREO_ID { get; set; }
        public double? HORARIOLABORAL_ID { get; set; }
        public double? DEPARTAMENTO_ID { get; set; }
    }
}
