namespace PROINSA_GP_API.Entidad
{
    public class Deduccion
    {

        public long ID_DEDUCCION { get; set; }

        public string? NOMBRE_DEDUCCION { get; set; }
        public string? DESCRIPCION { get; set; }
        
        public decimal PORCENTAJE { get; set; }

    }
}
