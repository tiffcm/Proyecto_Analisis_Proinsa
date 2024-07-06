
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Servicios;
using System.Data;
using System.Text.Json;

namespace PROINSA_GP_WEB.Models
{
    public class AprobacionModel(HttpClient _httpClient, IConfiguration iConfiguration) : IAprobacionModel
    {
        public Respuesta? ObtenerSolicitudesEmpleado(long? idEmpleado)
        {            

            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Aprobacion/ObtenerAprobacionPendiente?id_empleado=" + idEmpleado;
            var response = _httpClient.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<Respuesta>().Result;
            }
            else
                return new Respuesta();
        }

        public Respuesta? ObtenerAprobacionPendienteDetalle(long? idEmpleado, long ID_SOLICITUD)
        {

            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Aprobacion/ObtenerAprobacionPendienteDetalle?ID_SOLICITUD=" + ID_SOLICITUD + "&id_empleado=" + idEmpleado;
            var response = _httpClient.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<Respuesta>().Result;
            }
            else
                return new Respuesta();
        }

        public Respuesta? ObtenerAprobacionFlujo(long ID_SOLICITUD)
        {

            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Aprobacion/ObtenerAprobacionFlujo?ID_SOLICITUD=" + ID_SOLICITUD;
            var response = _httpClient.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                return response.Content.ReadFromJsonAsync<Respuesta>().Result;
            }
            else
                return new Respuesta();
        }
    }
}
