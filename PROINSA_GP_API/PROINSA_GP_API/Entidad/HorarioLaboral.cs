namespace PROINSA_GP_API.Entidad
{
    public class HorarioLaboral
    {
        public long ID_HORARIOLABORAL {  get; set; }
        public string? NOMBREHL { get; set; }
        public string? DESCRIPCION { get; set; }
        public int HORA_INGRESO {  get; set; }
        public int HORA_SALIDA { get; set; }
    }
}
