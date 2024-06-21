using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Servicios;

namespace PROINSA_GP_WEB.Models
{
    /// <summary>
    /// Este módulo se encarga de enviar las consultas a la base de datos.
    /// </summary>
    /// <param name="_httpClient">Inyección de dependencias</param>
    public class UsuarioModel (HttpClient _httpClient) : IUsuarioModel
    {
        /// <summary>
        /// Se envía el correo del usuario para poder traer la información completa.
        /// </summary>
        /// <param name="correo">Correo del usuario recuperado con Identity</param>
        /// <returns>Regresa la información deserializada o vacía dependiendo del código</returns>
        public Respuesta? ConsultarDatosEmpleado(string correo)
        {            
            string url = "https://localhost:44358/api/Usuario/ConsultarDatosEmpleado?correo=" + correo;           
            var solicitud = _httpClient.GetAsync(url).Result;

            if (solicitud.IsSuccessStatusCode)
                //Deserializa la información provieniente de la DB
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;            
            else
                //Como el API no obtuvo una respuesta exitoso se devuelve vacío
                return new Respuesta();
        }

        //public void ActualizarUsuario(ActualizarUsuario AcU, IFormFile FOTO)
        //{
        //    using (httpClient)
        //    {
        //        string url = "";
        //        JsonContent body = JsonContent.Create(AcU);
        //        var resp = httpClient.PutAsJsonAsync(url, body).Result;
        //    }
        //}
    }
}
