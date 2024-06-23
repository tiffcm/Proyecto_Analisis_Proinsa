using Microsoft.Extensions.Configuration;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Servicios;
using System.Configuration;
using System.Net.Http;

namespace PROINSA_GP_WEB.Models
{
    public class SolicitudModel(HttpClient _httpClient, IConfiguration iConfiguration) : ISolicitudModel
    {

        public Respuesta? RegistrarSolicitud(Solicitud entidad)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Solicitud/RegistrarSolicitud";
            JsonContent body = JsonContent.Create(entidad);
            var solicitud = _httpClient.PostAsync(url, body).Result;
            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }
    }
}
