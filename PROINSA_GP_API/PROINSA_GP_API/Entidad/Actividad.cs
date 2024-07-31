namespace PROINSA_GP_WEB.Entidad
{
    public class Actividad
    {
        public long ID_CLIENTE {  get; set; }
        public string? NOMBRE {  get; set; }
        public string? DETALLE { get; set; }
        public string? PAIS {  get; set; }
        public string? SECTOR { get; set; }
        public string? ESTADO { get; set; }
        public decimal? TOTAL_HORAS { get; set; }
        public string? INICIO_FECHA { get; set; }
        public string? FINAL_FECHA { get; set; }
        public string? COD_PROYECTO { get; set; }
        public string? COMENTARIO {  get; set; }
        public long? CONTACTO_ID { get; set; }
        public long? ID_PROYECTO { get; set; }
        public long? PROYECTO_ID { get; set; }


        public string? NOMBRE_CONTACTO { get; set; }
        public string? NOMBRE_CLIENTE { get; set; }


		////
		///
		public string? FECHA_INICIO { get; set; }
        public string? FECHA_FIN {  get; set; }
        public decimal? TOTALHORAS { get; set; }
        public long EMPLEADO_ID { get; set; }

        public string? PROYECTO_NOMBRE { get; set; }

        public long ID_REGISTROACTIVIDAD { get; set; }

    }
}
