using Newtonsoft.Json;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Servicios;
using System.Net.Http;
using System.Text;

namespace PROINSA_GP_WEB.Models
{
    public class EmpleadoModel (HttpClient _httpClient) : IEmpleadoModel
    {
        /**
         * Consulta de tipo Get para enviar un ID por la ruta que se comunica con el API
         * Dependiendo de la respuesta que se obtenga se lee el archivo JSON que retorna el 
         * API
         **/
        public EmpleadoRespuesta? ConsultarEmpleado(string correo)
        {            
            string url = "https://localhost:7220/api/Usuario/ConsultarEmpleado?correo=" + correo;           
            var solicitud = _httpClient.GetAsync(url).Result;

            if (solicitud.IsSuccessStatusCode)
            {
                return solicitud.Content.ReadFromJsonAsync<EmpleadoRespuesta>().Result;
            }
            else
            {
                return null;
            }
        }

        public EmpleadoRespuesta? ObtenerDatosEmpleado(string CORREO)
        {
            string url = "https://localhost:7220/api/Usuario/ObtenerDatosEmpleado?CORREO=" + CORREO;
            var solicitud = _httpClient.GetAsync(url).Result;

            if (solicitud.IsSuccessStatusCode)
            {
                return solicitud.Content.ReadFromJsonAsync<EmpleadoRespuesta>().Result;
            }
            else
            {
                return null;
            }
        }
    }
    /*
    public void ActualizarUsuario(ActualizarUsuario AcU, IFormFile FOTO)
    {
            using (httpClient)
            {
                string url = "";
                JsonContent body = JsonContent.Create(AcU);
                var resp = httpClient.PutAsJsonAsync(url, body).Result;
            }
    }

    */
   
}
