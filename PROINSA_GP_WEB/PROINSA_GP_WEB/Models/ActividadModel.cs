using Microsoft.AspNetCore.Mvc.Rendering;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Servicios;
using System.Text.Json;
using static PROINSA_GP_WEB.Entidad.Actividad;

namespace PROINSA_GP_WEB.Models
{
    public class ActividadModel(HttpClient _httpClient, IConfiguration iConfiguration) : IActividadModel
    {
        /// <summary>
        /// PARTE DE LOS "CLIENTES" = EMPLEADOS
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        public Respuesta? AgregarCliente(Actividad entidad)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Actividades/AgregarCliente";
            JsonContent body = JsonContent.Create(entidad);
            var solicitud = _httpClient.PostAsync(url, body).Result;
            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }


        public Respuesta? ModificarCliente(Actividad entidad)
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
        public List<Cliente> ListaDeClientes()
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Actividades/ListaDeClientes";
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
            {
                var respuesta = resp.Content.ReadFromJsonAsync<Respuesta>().Result;
                if (respuesta!.CODIGO == 1)
                {
                    var jsonElement = (JsonElement)respuesta.CONTENIDO!;
                    var ClientesList = JsonSerializer.Deserialize<List<Cliente>>(jsonElement.GetRawText());
                    return ClientesList ?? new List<Cliente>();
                }
                
            }
            return new List<Cliente>();
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
        public Respuesta? AgregarProyecto(Actividad entidad)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Actividades/AgregarProyecto";
            JsonContent body = JsonContent.Create(entidad);
            var solicitud = _httpClient.PostAsync(url, body).Result;
            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }

        public Respuesta? ModificarProyecto(Actividad entidad)
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

        public List<SelectListItem> ListaDeProyectos()
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Actividades/ListaDeProyectos";
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
            {
                var respuesta = resp.Content.ReadFromJsonAsync<Respuesta>().Result;
                if (respuesta!.CODIGO == 1)
                {
                    var jsonElement = (JsonElement)respuesta.CONTENIDO!;
                    var ProyectoList = JsonSerializer.Deserialize<List<Actividad>>(jsonElement.GetRawText());
                    if (ProyectoList != null)
                    {
                        return ProyectoList.Select(t => new SelectListItem
                        {
                            Value = t.ID_PROYECTO.ToString(),
                            Text = t.NOMBRE
                        }).ToList();
                    }
                }
            }
            return new List<SelectListItem>();
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
