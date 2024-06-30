using Azure;
using Microsoft.AspNetCore.Mvc.Rendering;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Servicios;
using System.Text.Json;

namespace PROINSA_GP_WEB.Models
{
    /// <summary>
    /// Este módulo se encarga de enviar las consultas a la base de datos.
    /// </summary>
    /// <param name="_httpClient">Inyección de dependencias</param>
    public class UsuarioModel(HttpClient _httpClient, IConfiguration iConfiguration) : IUsuarioModel
    {
        /// <summary>
        /// Se envía el correo del usuario para poder traer la información completa.
        /// </summary>
        /// <param name="correo">Correo del usuario recuperado con Identity</param>
        /// <returns>Regresa la información deserializada o vacía dependiendo del código</returns>
        public Respuesta? ConsultarDatosEmpleado(string correo)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Usuario/ConsultarDatosEmpleado?correo=" + correo;
            var solicitud = _httpClient.GetAsync(url).Result;

            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }

        /// <summary>
        /// Este procedimiento funciona para actualizar la información permitida para un solo usuario
        /// </summary>
        /// <param name="entidad">Se envía el objeto junto con los datos de ese usuario</param>
        /// <returns>Confirmación de éxito de DB</returns>
        public Respuesta? ActualizarDatosUsuario(Usuario entidad)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Usuario/ActualizarDatosUsuario";
            JsonContent body = JsonContent.Create(entidad);
            var solicitud = _httpClient.PutAsync(url, body).Result;
            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }

        //----------------------------------------------------------------------------------------------------

        public Respuesta? ObtenerTelefonosUsuario(long? idEmpleado)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Usuario/ObtenerTelefonosUsuario?idEmpleado=" + idEmpleado;
            var solicitud = _httpClient.GetAsync(url).Result;

            if (solicitud.IsSuccessStatusCode)
            {
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            }
            else
                return new Respuesta();
        }

        public Respuesta? MostrarInfoVistaAdmin()
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Usuario/MostrarInfoVistaAdmin";
            var solicitud = _httpClient.GetAsync(url).Result;

            if (solicitud.IsSuccessStatusCode)
            {
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            }
            else
                return new Respuesta();
        }

        public Respuesta? EditarDatosVistaAdmin(Usuario datos)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Usuario/EditarDatosVistaAdmin";
            JsonContent body = JsonContent.Create(datos);
            var solicitud = _httpClient.PutAsync(url, body).Result;
            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }

        public Respuesta? MostrarEmpleadoVistaAdmin(long? idEmpleado)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Usuario/MostrarEmpleadoVistaAdmin?idEmpleado=" + idEmpleado;
            var solicitud = _httpClient.GetAsync(url).Result;

            if (solicitud.IsSuccessStatusCode)
            {
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            }
            else
                return new Respuesta();
        }

		public Respuesta? CambiarEstadoUsuarioAdmin(long? idEmpleado)
        {
			string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Usuario/CambiarEstadoUsuarioAdmin?idEmpleado=" + idEmpleado;
			JsonContent body = JsonContent.Create(idEmpleado);
			var solicitud = _httpClient.PutAsync(url, body).Result;
			if (solicitud.IsSuccessStatusCode)
				return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
			else
				return new Respuesta();

		}

        public List<SelectListItem> MostrarTodosCargos()
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Usuario/MostrarTodosCargos";
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
            {
                var respuesta = resp.Content.ReadFromJsonAsync<Respuesta>().Result;
                if (respuesta!.CODIGO == 1)
                {
                    var jsonElement = (JsonElement)respuesta.CONTENIDO!;
                    var CargoList = JsonSerializer.Deserialize<List<Cargo>>(jsonElement.GetRawText());
                    if (CargoList != null)
                    {
                        return CargoList.Select(t => new SelectListItem
                        {
                            Value = t.ID_CARGO.ToString(),
                            Text = t.CARGO
                        }).ToList();
                    }
                }
            }
            return new List<SelectListItem>();
        }
        public List<SelectListItem> MostrarTodosHorarios()
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Usuario/MostrarTodosHorarios";
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
            {
                var respuesta = resp.Content.ReadFromJsonAsync<Respuesta>().Result;
                if (respuesta!.CODIGO == 1)
                {
                    var jsonElement = (JsonElement)respuesta.CONTENIDO!;
                    var HLList = JsonSerializer.Deserialize<List<Horario>>(jsonElement.GetRawText());
                    if (HLList != null)
                    {
                        return HLList.Select(t => new SelectListItem
                        {
                            Value = t.ID_HORARIOLABORAL.ToString(),
                            Text = t.NOMBRE_HL
                        }).ToList();
                    }
                }
            }
            return new List<SelectListItem>();
        }
        public List<SelectListItem> MostrarTodosRoles()
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Usuario/MostrarTodosRoles";
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
            {
                var respuesta = resp.Content.ReadFromJsonAsync<Respuesta>().Result;
                if (respuesta!.CODIGO == 1)
                {
                    var jsonElement = (JsonElement)respuesta.CONTENIDO!;
                    var rolList = JsonSerializer.Deserialize<List<Rol>>(jsonElement.GetRawText());
                    if (rolList != null)
                    {
                        return rolList.Select(t => new SelectListItem
                        {
                            Value = t.IDROL.ToString(),
                            Text = t.NOMBREROL
                        }).ToList();
                    }
                }
            }
            return new List<SelectListItem>();

        }



        public List<SelectListItem> MostrarTodosDepartamentos()
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Usuario/MostrarTodosDepartamentos";
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
            {
                var respuesta = resp.Content.ReadFromJsonAsync<Respuesta>().Result;
                if (respuesta!.CODIGO == 1)
                {
                    var jsonElement = (JsonElement)respuesta.CONTENIDO!;
                    var departamentoList = JsonSerializer.Deserialize<List<Departamento>>(jsonElement.GetRawText());
                    if (departamentoList != null)
                    {
                        return departamentoList.Select(t => new SelectListItem
                        {
                            Value = t.ID_DEPARTAMENTO.ToString(),
                            Text = t.DEPARTAMENTO
                        }).ToList();
                    }
                }
            }
            return new List<SelectListItem>();

        }


    }
}