namespace PROINSA_GP_API.Entidad
{
    public class Nomina
    {
        public long ID_TIPONOMINA { get; set; }
        public string? DESCRIPCION { get; set; }
        public string? OBSERVACIONES { get; set; }

        public int TipoNomina { get; set; }

        public int CreadorID { get; set; }

        public decimal MONTO { get; set; }
        public string? DETALLE { get; set; }

        public string? NOMBRE { get; set; }

        public int CANTIDAD{ get; set; }

        public string? NOMINA { get; set; }

        public string? FECHA_PAGO { get; set; }

        public string? EMPLEADO { get; set; }

        public string? IDENTIFICACION { get; set; }

        public string? CARGO { get; set; }

        public decimal SALARIO { get; set; }

    }


    public class IngresoNominaDetalle
    {
        public decimal MONTO { get; set; }
        public string? DETALLE { get; set; }
        public long INGRESO_ID { get; set; }
        public long EMPLEADO_ID { get; set; }

        public int CANTIDAD { get; set; }
    }


    public class DeduccionNominaDetalle
    {
        public decimal MONTO { get; set; }
        public string? DETALLE { get; set; }
        public long DEDUCCION_ID { get; set; }
        public long EMPLEADO_ID { get; set; }
    }
}
