using Microsoft.Extensions.Configuration;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Servicios;
using System.Configuration;
using System.Net.Http;

namespace PROINSA_GP_WEB.Models
{
    public class ReportModel(HttpClient _httpClient, IConfiguration iConfiguration) : IReportModel)
    {
        

        public Respuesta DatosEmpleadoNominaReporte(long EMPLEADO_ID)
        {

            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Report/DatosEmpleadoNominaReporte?EMPLEADO_ID=" + EMPLEADO_ID;
            var result = _httpClient.GetAsync(url).Result;
            if (result.IsSuccessStatusCode)
                return result.Content.ReadFromJsonAsync<Respuesta>().Result!;
            else
                return new Respuesta();
        }
        
        
        public Respuesta DatosNominaEmpleadoDeduccionesReporte(Report report)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Report/DatosNominaEmpleadoDeduccionesReporte";
            var result = _httpClient.PostAsJsonAsync(url, report).Result;
            if (result.IsSuccessStatusCode)
                return result.Content.ReadFromJsonAsync<Respuesta>().Result!;
            else
                return new Respuesta();
        }
        public Respuesta DatosNominaEmpleadoIngresosReporte(Report report)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Report/DatosNominaEmpleadoIngresosReporte";
            var result = _httpClient.PostAsJsonAsync(url, report).Result;
            if (result.IsSuccessStatusCode)
                return result.Content.ReadFromJsonAsync<Respuesta>().Result!;
            else
                return new Respuesta();
        }
        public Respuesta DatosNominaEmpleadoReporte(Report report)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Report/DatosNominaEmpleadoReporte";
            var result = _httpClient.PostAsJsonAsync(url, report).Result;
            if (result.IsSuccessStatusCode)
                return result.Content.ReadFromJsonAsync<Respuesta>().Result!;
            else
                return new Respuesta();
        }

        public Respuesta DatosNominaGeneralReporte(string fechaSeleccionada)
        {

            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Report/DatosNominaGeneralReporte?FechaSeleccionada=" + fechaSeleccionada;
            var result = _httpClient.GetAsync(url).Result;
            if (result.IsSuccessStatusCode)
                return result.Content.ReadFromJsonAsync<Respuesta>().Result!;
            else
                return new Respuesta();
        }


        public Respuesta EmpleadosReporte()
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Documentos/EmpleadosReporte";
            var result = _httpClient.GetAsync(url).Result;
            if (result.IsSuccessStatusCode)
                return result.Content.ReadFromJsonAsync<Respuesta>().Result!;
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
        public Respuesta NominaGeneralReporte(string fechaSeleccionada)
        {

            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Report/NominaGeneralReporte?FechaSeleccionada=" + fechaSeleccionada;
            var result = _httpClient.GetAsync(url).Result;
            if (result.IsSuccessStatusCode)
                return result.Content.ReadFromJsonAsync<Respuesta>().Result!;
            else
                return new Respuesta();
        }
        public Respuesta ObtenerSolicitudEmpleadoPeriodoReporte(Report report)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Report/ObtenerSolicitudEmpleadoPeriodoReporte";
            var result = _httpClient.PostAsJsonAsync(url, report).Result;
            if (result.IsSuccessStatusCode)
                return result.Content.ReadFromJsonAsync<Respuesta>().Result!;
            else
                return new Respuesta();
        }
        public Respuesta ConsultarNombreEmpleado(long EMPLEADO_ID)
        {

            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Report/ConsultarNombreEmpleado?EMPLEADO_ID=" + EMPLEADO_ID;
            var result = _httpClient.GetAsync(url).Result;
            if (result.IsSuccessStatusCode)
                return result.Content.ReadFromJsonAsync<Respuesta>().Result!;
            else
                return new Respuesta();
        }

    }
}
