using Microsoft.AspNetCore.Mvc.Rendering;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Servicios;
using System.Text.Json;

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
<<<<<<< Updated upstream
=======
        
        public List<SelectListItem> ListarClientesNombres()
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Actividades/ListarClientesNombres";
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
            {
                var respuesta = resp.Content.ReadFromJsonAsync<Respuesta>().Result;
                if (respuesta!.CODIGO == 1)
                {
                    var jsonElement = (JsonElement)respuesta.CONTENIDO!;
                    var ClientesList = JsonSerializer.Deserialize<List<Actividad>>(jsonElement.GetRawText());
                    if (ClientesList != null)
                    {
                        return ClientesList.Select(t => new SelectListItem
                        {
                            Value = t.ID_CLIENTE.ToString(),
                            Text = t.NOMBRE
                        }).ToList();
                    }
                }
            }
            return new List<SelectListItem>();
        }
>>>>>>> Stashed changes

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
<<<<<<< Updated upstream

        public Respuesta? DetallarProyecto(long? IdPROYECTO)
=======
        public Respuesta? ListarEmpleadosPorProyecto()
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Actividades/ListarEmpleadosPorProyecto";
            var solicitud = _httpClient.GetAsync(url).Result;

            if (solicitud.IsSuccessStatusCode)
            {
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            }
            else
                return new Respuesta();
        }

        public List<SelectListItem> ListarProyectosNombres()
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Actividades/ListarProyectosNombres";
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

        public List<SelectListItem> MostrarTodosEmpleados()
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Actividades/MostrarTodosEmpleados";
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
            {
                var respuesta = resp.Content.ReadFromJsonAsync<Respuesta>().Result;
                if (respuesta!.CODIGO == 1)
                {
                    var jsonElement = (JsonElement)respuesta.CONTENIDO!;
                    var EmpleadoList = JsonSerializer.Deserialize<List<Actividad>>(jsonElement.GetRawText());
                    if (EmpleadoList != null)
                    {
                        return EmpleadoList.Select(t => new SelectListItem
                        {
                            Value = t.EMPLEADO_ID.ToString(),
                            Text = t.NOMBRE
                        }).ToList();
                    }
                }
            }
            return new List<SelectListItem>();
        }

		//public List<SelectListItem> MostrarTodosEmpleadosContactos()
		//{
		//	string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Actividades/MostrarTodosEmpleadosContactos";
		//	var resp = _httpClient.GetAsync(url).Result;

		//	if (resp.IsSuccessStatusCode)
		//	{
		//		var respuesta = resp.Content.ReadFromJsonAsync<Respuesta>().Result;
		//		if (respuesta!.CODIGO == 1)
		//		{
		//			var jsonElement = (JsonElement)respuesta.CONTENIDO!;
		//			var EmpleadoContactList = JsonSerializer.Deserialize<List<Actividad>>(jsonElement.GetRawText());
		//			if (EmpleadoContactList != null)
		//			{
		//				return EmpleadoContactList.Select(t => new SelectListItem
		//				{
		//					Value = t.CONTACTO_ID.ToString(),
		//					Text = t.NOMBRE_CONTACTO
		//				}).ToList();
		//			}
		//		}
		//	}
		//	return new List<SelectListItem>();
		//}

		public Respuesta? DetallarProyecto(long? IdPROYECTO)
>>>>>>> Stashed changes
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

        public Respuesta? AgregarEmpleadoProyecto(Actividad entidad)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Actividades/AgregarEmpleadoProyecto";
            JsonContent body = JsonContent.Create(entidad);
            var solicitud = _httpClient.PostAsync(url, body).Result;
            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }


        /////////////////
        /// USUARIO
        /// 

        public Respuesta? IngresorRegistroActividad(Actividad entidad)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Actividades/IngresorRegistroActividad";
            JsonContent body = JsonContent.Create(entidad);
            var solicitud = _httpClient.PostAsync(url, body).Result;
            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }

        public List<SelectListItem> ListarProyectosPorEmpleado(long? ID_EMPLEADO)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Actividades/ListarProyectosPorEmpleado?iDEmpleado=" + ID_EMPLEADO;
            var resp = _httpClient.GetAsync(url).Result;

            if (resp.IsSuccessStatusCode)
            {
                var respuesta = resp.Content.ReadFromJsonAsync<Respuesta>().Result;
                if (respuesta!.CODIGO == 1)
                {
                    var jsonElement = (JsonElement)respuesta.CONTENIDO!;
                    var EmpleadoProyecList = JsonSerializer.Deserialize<List<Actividad>>(jsonElement.GetRawText());
                    if (EmpleadoProyecList != null)
                    {
                        return EmpleadoProyecList.Select(t => new SelectListItem
                        {
                            Value = t.ID_PROYECTO.ToString(),
                            Text = t.PROYECTO_NOMBRE
                        }).ToList();
                    }
                }
            }
            return new List<SelectListItem>();
        }

        //ListarActividadesPorEmpleado  se hace similar a la de ListarProyectosPorEmpleado, pero aun no funciona, cuando funcione se implementa esta

        public Respuesta? ModificarRegistroActividad(Actividad entidad)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Actividades/ModificarRegistroActividad";
            JsonContent body = JsonContent.Create(entidad);
            var solicitud = _httpClient.PutAsync(url, body).Result;
            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }

        public Respuesta? ListarTodasActividades()
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Actividades/ListarTodasActividades";
            var solicitud = _httpClient.GetAsync(url).Result;

            if (solicitud.IsSuccessStatusCode)
            {
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            }
            else
                return new Respuesta();
        }

        

        public Respuesta? DetallarRegistroActividadPorID(long? ID_REGISTROACTIVIDAD)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Actividades/DetallarRegistroActividadPorID?ID_REGISTROACTIVIDAD=" + ID_REGISTROACTIVIDAD;
            var solicitud = _httpClient.GetAsync(url).Result;

            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }

        public Respuesta? CambiarEstadoRegistroActividad(long? ID_REGISTROACTIVIDAD)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Actividades/CambiarEstadoRegistroActividad?ID_REGISTROACTIVIDAD=" + ID_REGISTROACTIVIDAD;
            JsonContent body = JsonContent.Create(ID_REGISTROACTIVIDAD);
            var solicitud = _httpClient.PutAsync(url, body).Result;
            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();

        }





    }
}
