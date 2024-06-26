using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Servicios;
using System.Configuration;
using System.Net.Http;
using System.Text.Json;

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


        public List<SelectListItem>  ObtenerTipoSolicitud()
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Solicitud/ConsultarTipoSolicitud";
            var response = _httpClient.GetAsync(url).Result;
            
            if (response.IsSuccessStatusCode)
            {
                var respuesta = response.Content.ReadFromJsonAsync<Respuesta>().Result;
                if (respuesta.CODIGO == 1)
                {
                    var jsonElement = (JsonElement)respuesta.CONTENIDO;
                    var solicitudes = JsonSerializer.Deserialize<List<Solicitud>>(jsonElement.GetRawText());
                    if (solicitudes != null)
                    {
                        return solicitudes.Select(t => new SelectListItem
                        {
                            Value = t.ID.ToString(),
                            Text = t.DESCRIPCION
                        }).ToList();
                    }
                }
            }
            return new List<SelectListItem>();
        }
    }
}

