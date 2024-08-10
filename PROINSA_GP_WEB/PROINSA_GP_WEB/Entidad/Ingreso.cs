namespace PROINSA_GP_WEB.Entidad
{
    public class Ingreso
    {
        public long ID_INGRESO { get; set; }
        public string? NOMBRE_INGRESO { get; set; }
        public string? DESCRIPCION { get; set; }
        public decimal MONTO { get; set; }
        public decimal PORCENTAJE { get; set; }
    }
}
