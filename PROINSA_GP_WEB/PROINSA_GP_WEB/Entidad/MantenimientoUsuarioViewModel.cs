namespace PROINSA_GP_WEB.Entidad
{
	public class MantenimientoUsuarioViewModel
	{
        public Usuario Usuario { get; set; } = new Usuario();
        public Dictionary<int, string> RolesDisponibles { get; set; } = new Dictionary<int, string>();

    }
}
