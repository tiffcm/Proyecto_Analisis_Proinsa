namespace PROINSA_GP_API.Entidad
{
    public class Report
    {
        // usados para la validacion
        public long? EMPLEADO_ID { get; set; }
        public string? FechaSeleccionada { get; set; }
        public string? FECHA_INICIO { get; set; }
        public string? FECHA_FINAL { get; set; }

        // otros

        public long? IDENTIFICACION { get; set; }
        public string? NOMBRE { get; set; }
        public string Nombre { get; set; }
        public string? Cargo { get; set; }
        public string? CARGO { get; set; }
        public long ID_CARGO { get; set; }
        public string? SALDO_VACACIONES { get; set; }
        public string? TIPO { get; set; }
        public string? DEDU_DETALLE { get; set; }
        public string? DEDU_MONTO { get; set; }
        public string? CANTIDAD { get; set; }
        public string? INGRE_DETALLE { get; set; }
        public string? INGRE_MONTO { get; set; }
        public string? fechapago { get; set; }
        public string? periodonomina { get; set; }
        public string? fechaprobacion { get; set; }
        public string? fechacreacion { get; set; }
        public string? fecha_solicitud { get; set; }

        public decimal? SALARIO_NETO { get; set; }
        public decimal? SALARIO_BRUTO { get; set; }
        public string? CREADOR { get; set; }
        public string? APROBADOR { get; set; }
        public decimal SALARIO { get; set; }
        public string? FOTO { get; set; }
        public string? CORREO { get; set; }
        public string? NOMBRE_HL { get; set; }
        public long ID_HORARIOLABORAL { get; set; }
        public string? DEPARTAMENTO { get; set; }
        public long ID_DEPARTAMENTO { get; set; }
        public string? DIRRECION { get; set; }
        public bool? ESTADO { get; set; }
        public string? NOMBREROL { get; set; }
        public string? NOMBRE_TIPO_SOLICITUD { get; set; }

        public string? dias { get; set; }
        public string? COMENTARIO { get; set; }
        public string? detalle { get; set; }

    }
}
