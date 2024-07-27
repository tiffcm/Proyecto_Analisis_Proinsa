namespace PROINSA_GP_API.Entidad
{
    public class Nomina
    {
        public long ID_TIPONOMINA { get; set; }
        public string? DESCRIPCION { get; set; }
        public string? OBSERVACIONES { get; set; }

        public int TipoNomina { get; set; }

        public int CreadorID { get; set; }

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
