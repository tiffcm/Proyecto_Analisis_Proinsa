using Microsoft.Extensions.Configuration;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Servicios;
using System.Configuration;
using System.Net.Http;

namespace PROINSA_GP_WEB.Models
{
    public class SolicitudModel(HttpClient httpClient, IConfiguration iConfiguration) : ISolicitudModel
    {

        public Respuesta RegistrarSolicitud(Solicitud ent)
        {
            using (httpClient)
            {
                string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Solicitud/RegistrarSolicitud";
                JsonContent body = JsonContent.Create(ent);
                var resp = httpClient.PostAsync(url, body).Result;

                if (resp.IsSuccessStatusCode)
                    return resp.Content.ReadFromJsonAsync<Respuesta>().Result!;
                else
                    return new Respuesta();
            }
        }
    }
}
