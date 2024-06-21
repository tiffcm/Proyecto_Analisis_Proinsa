namespace PROINSA_GP_WEB.Entidad
{
    /// <summary>
    /// Esta es la entidad que se encargará de traer la respuesta que devuelva
    /// la base de datos.Dependiendo del tipo de solicitud se obtendrá un 
    /// código, un mensaje y un contenido.
    /// </summary>
    /// <author>Brandon Ruiz Miranda</author>
    /// <version>1.0</version>
    public class Respuesta
    {
        public int CODIGO {  get; set; }
        public string? MENSAJE {  get; set; }
        public object? CONTENIDO { get; set; }
    }
}
