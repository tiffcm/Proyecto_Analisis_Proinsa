using Microsoft.Extensions.Configuration;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Servicios;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace PROINSA_GP_WEB.Models
{
    public class DocumentoModel(HttpClient _httpClient, IConfiguration iConfiguration) : IDocumentoModel
    {

        public Respuesta? RegistrarDocumento(Documento entidad)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Documentos/RegistrarDocumento";
            JsonContent body = JsonContent.Create(entidad);
            var solicitud = _httpClient.PostAsync(url, body).Result;
            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }

        public Respuesta ConsultarTiposDocumento()
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Documentos/ConsultarTiposDocumento";                        
            var result = _httpClient.GetAsync(url).Result;
            if (result.IsSuccessStatusCode)
                return result.Content.ReadFromJsonAsync<Respuesta>().Result!;
            else
                return new Respuesta();
        }

        public Respuesta ConsultarDocumentosEmpleado(long EMPLEADO_ID)
        {

            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Documentos/ConsultarDocumentosEmpleado?EMPLEADO_ID=" + EMPLEADO_ID;
            var result = _httpClient.GetAsync(url).Result;
            if (result.IsSuccessStatusCode)                
                return result.Content.ReadFromJsonAsync<Respuesta>().Result!;
            else
                return new Respuesta();
        }

        public Respuesta ConsultarDocumentoEmpleado(long ID_EMPLEADODOCUMENTO)
        {

            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Documentos/ConsultarDocumentoEmpleado?ID_EMPLEADODOCUMENTO=" + ID_EMPLEADODOCUMENTO;
            var result = _httpClient.GetAsync(url).Result;
            if (result.IsSuccessStatusCode)
                return result.Content.ReadFromJsonAsync<Respuesta>().Result!;
            else
                return new Respuesta();
        }

        public Respuesta EliminarDocumento( long ID_EMPLEADODOCUMENTO)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Documentos/EliminarDocumento?ID_EMPLEADODOCUMENTO=" + ID_EMPLEADODOCUMENTO;
            var result = _httpClient.DeleteAsync(url).Result;
            if (result.IsSuccessStatusCode)
                return result.Content.ReadFromJsonAsync<Respuesta>().Result!;
            else
                return new Respuesta();
        }

        public Respuesta? ActualizarDocumento(Documento entidad)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Documentos/ActualizarDocumento";
            JsonContent body = JsonContent.Create(entidad);
            var solicitud = _httpClient.PutAsync(url, body).Result;
            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }

        public Respuesta ConsultarEmpleados()
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Usuario/ConsultarEmpleados";
            var result = _httpClient.GetAsync(url).Result;
            if (result.IsSuccessStatusCode)
                return result.Content.ReadFromJsonAsync<Respuesta>().Result!;
            else
                return new Respuesta();
        }
    }
}
