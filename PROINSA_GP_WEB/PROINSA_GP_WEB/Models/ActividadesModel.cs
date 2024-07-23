using Microsoft.AspNetCore.Mvc.Rendering;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Servicios;
using System.Text.Json;

namespace PROINSA_GP_WEB.Models
{
    public class ActividadesModel(HttpClient _httpClient, IConfiguration iConfiguration) : IActividadesModel
    {
        /// <summary>
        /// PARTE DE LOS "CLIENTES" = EMPLEADOS
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public Respuesta? AgregarCliente(Actividades entidad)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Actividades/AgregarCliente";
            JsonContent body = JsonContent.Create(entidad);
            var solicitud = _httpClient.PostAsync(url, body).Result;
            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }


        public Respuesta? ModificarCliente(Actividades entidad)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Actividades/ModificarCliente";
            JsonContent body = JsonContent.Create(entidad);
            var solicitud = _httpClient.PutAsync(url, body).Result;
            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }

        public Respuesta? ListarClientes()
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Actividades/ListarClientes";
            var solicitud = _httpClient.GetAsync(url).Result;

            if (solicitud.IsSuccessStatusCode)
            {
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            }
            else
                return new Respuesta();
        }

        public Respuesta? DetallarCliente(long? IdCLIENTE)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Actividades/DetallarCliente?IdCLIENTE=" + IdCLIENTE;
            var solicitud = _httpClient.GetAsync(url).Result;

            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }

        public Respuesta? CambiarEstadoCliente(long? IdCLIENTE)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Actividades/CambiarEstadoCliente?IdCLIENTE=" + IdCLIENTE;
            JsonContent body = JsonContent.Create(IdCLIENTE);
            var solicitud = _httpClient.PutAsync(url, body).Result;
            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();

        }

        /// <summary>
        /// PARTE DE LOS PROYECTOS
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public Respuesta? AgregarProyecto(Actividades entidad)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Actividades/AgregarProyecto";
            JsonContent body = JsonContent.Create(entidad);
            var solicitud = _httpClient.PostAsync(url, body).Result;
            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }

        public Respuesta? ModificarProyecto(Actividades entidad)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Actividades/ModificarProyecto";
            JsonContent body = JsonContent.Create(entidad);
            var solicitud = _httpClient.PutAsync(url, body).Result;
            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }

        public Respuesta? ListarProyectos()
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Actividades/ListarProyectos";
            var solicitud = _httpClient.GetAsync(url).Result;

            if (solicitud.IsSuccessStatusCode)
            {
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            }
            else
                return new Respuesta();
        }

        public Respuesta? DetallarProyecto(long? IdPROYECTO)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Actividades/DetallarProyecto?IdPROYECTO=" + IdPROYECTO;
            var solicitud = _httpClient.GetAsync(url).Result;

            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }

        public Respuesta? CambiarEstadoProyecto(long? IdPROYECTO)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Actividades/CambiarEstadoProyecto?IdPROYECTO=" + IdPROYECTO;
            JsonContent body = JsonContent.Create(IdPROYECTO);
            var solicitud = _httpClient.PutAsync(url, body).Result;
            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();

        }

    }
}
