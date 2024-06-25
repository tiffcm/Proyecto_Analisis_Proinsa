using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Servicios;
using System.Text.Json;

namespace PROINSA_GP_WEB.Models
{
    /// <summary>
    /// Este módulo se encarga de enviar las consultas a la base de datos.
    /// </summary>
    /// <param name="_httpClient">Inyección de dependencias</param>
    public class UsuarioModel (HttpClient _httpClient, IConfiguration iConfiguration) : IUsuarioModel
    {
        /// <summary>
        /// Se envía el correo del usuario para poder traer la información completa.
        /// </summary>
        /// <param name="correo">Correo del usuario recuperado con Identity</param>
        /// <returns>Regresa la información deserializada o vacía dependiendo del código</returns>
        public Respuesta? ConsultarDatosEmpleado(string correo)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Usuario/ConsultarDatosEmpleado?correo=" + correo;
            var solicitud = _httpClient.GetAsync(url).Result;

            if (solicitud.IsSuccessStatusCode)                
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;            
            else                
                return new Respuesta();
        }

        /// <summary>
        /// Este procedimiento funciona para actualizar la información permitida para un solo usuario
        /// </summary>
        /// <param name="entidad">Se envía el objeto junto con los datos de ese usuario</param>
        /// <returns>Confirmación de éxito de DB</returns>
        public Respuesta? ActualizarDatosUsuario(Usuario entidad)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Usuario/ActualizarDatosUsuario";
            JsonContent body = JsonContent.Create(entidad);
            var solicitud = _httpClient.PutAsync(url, body).Result;
            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }

        //----------------------------------------------------------------------------------------------------

        public Respuesta? ObtenerTelefonosUsuario(long? idEmpleado)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Usuario/ObtenerTelefonosUsuario?idEmpleado=" + idEmpleado;
            var solicitud = _httpClient.GetAsync(url).Result;

            if (solicitud.IsSuccessStatusCode)
            {
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            }
            else
                return new Respuesta();
        }

        public Respuesta? MostrarInfoVistaAdmin()
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Usuario/MostrarInfoVistaAdmin";
            var solicitud = _httpClient.GetAsync(url).Result;

            if (solicitud.IsSuccessStatusCode)
            {
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            }
            else
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
