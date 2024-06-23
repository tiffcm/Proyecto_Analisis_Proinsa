using Microsoft.Extensions.Configuration;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Servicios;
using System.Configuration;

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
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Usuario/ConsultarDatosEmpleado?CORREO=" + correo;
            var solicitud = _httpClient.GetAsync(url).Result;

            if (solicitud.IsSuccessStatusCode)                
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;            
            else                
                return new Respuesta();
        }

        //----------------------------------------------------------------------------------------------------

        public Respuesta? ActualizarDatosUsuario(Usuario entidad)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Usuario/ActualizarDatosUsuario";
            JsonContent body = JsonContent.Create(entidad);
            var solicitud = _httpClient.PostAsync(url, body).Result;
            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
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
