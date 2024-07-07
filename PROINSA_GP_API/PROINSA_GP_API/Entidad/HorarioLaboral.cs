namespace PROINSA_GP_API.Entidad
{
    public class HorarioLaboral
    {
        public long ID_HORARIOLABORAL { get; set; }       
        public string? HORARIO { get; set; }
        public int HORA_INGRESO { get; set; }
        public string? HORA_SALIDA { get; set; }
    }
}
