using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PROINSA_GP_API.Entidad;
using System.Data;
using Dapper;
using PROINSA_GP_WEB.Entidad;


namespace PROINSA_GP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActividadesController(IConfiguration iConfiguration) : ControllerBase
    {
        /// <summary>
        /// SPs para el cliente
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("AgregarCliente")]
        public async Task<IActionResult> AgregarCliente(Actividad entidad)
        {
            Respuesta respuesta = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var result = await context.ExecuteAsync("AgregarCliente", new { entidad.NOMBRE,  entidad.PAIS, entidad.SECTOR, entidad.DETALLE, }, commandType: CommandType.StoredProcedure);

                if (result > 0)
                {
                    respuesta.CODIGO = 1;
                    respuesta.MENSAJE = "OK";
                    respuesta.CONTENIDO = true;
                    return Ok(respuesta);
                }
                else
                {
                    respuesta.CONTENIDO = 0;
                    respuesta.MENSAJE = "No se logro agregar el cliente";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

        [HttpPut]
        [Route("ModificarCliente")]
        public async Task<IActionResult> ModificarCliente(Actividad entidad)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@ID_CLIENTE", entidad.ID_CLIENTE);
                parametros.Add("@NOMBRE", entidad.NOMBRE);
                parametros.Add("@DETALLE", entidad.DETALLE);
                parametros.Add("@PAIS", entidad.PAIS);
                parametros.Add("@SECTOR", entidad.SECTOR);
                //parametros.Add("@ESTADO", entidad.ESTADO);

                var request = await contexto.ExecuteAsync("ModificarCliente", parametros,
                   commandType: System.Data.CommandType.StoredProcedure);
                if (request > 0)
                {
                    respuesta.CODIGO = 1;
                    respuesta.MENSAJE = "OK";
                    respuesta.CONTENIDO = true;
                    return Ok(respuesta);
                }
                else
                {
                    respuesta.CODIGO = 0;
                    respuesta.MENSAJE = "Se presentó un inconveniente actualizando la información";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

        [HttpGet]
        [Route("ListarClientes")]
        public async Task<IActionResult> ListarClientes()
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var request = await contexto.QueryAsync("ListarClientes", new { },
                      commandType: System.Data.CommandType.StoredProcedure);
                if (request != null)
                {
                    respuesta.CODIGO = 1;
                    respuesta.MENSAJE = "OK";
                    respuesta.CONTENIDO = request.ToList();
                    return Ok(respuesta);
                }
                else
                {
                    respuesta.CODIGO = 0;
                    respuesta.MENSAJE = "No hay clientes registrados";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

<<<<<<< Updated upstream
=======
        [HttpGet]
        [Route("ListarClientesNombres")]
        public async Task<IActionResult> ListarClientesNombres()
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var request = await contexto.QueryAsync("ListarClientesNombres", new { },
                      commandType: System.Data.CommandType.StoredProcedure);
                if (request != null)
                {
                    respuesta.CODIGO = 1;
                    respuesta.MENSAJE = "OK";
                    respuesta.CONTENIDO = request.ToList();
                    return Ok(respuesta);
                }
                else
                {
                    respuesta.CODIGO = 0;
                    respuesta.MENSAJE = "No hay clientes registrados";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }
>>>>>>> Stashed changes

        [HttpGet]
        [Route("DetallarCliente")]
        public async Task<IActionResult> DetallarCliente(long? IdCLIENTE)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@ID_CLIENTE", IdCLIENTE);
                var request = (await contexto.QueryAsync("DetallarCliente",  parametros ,
                    commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();
                if (request != null)
                {
                    respuesta.CODIGO = 1;
                    respuesta.MENSAJE = "OK";
                    respuesta.CONTENIDO = request;
                    return Ok(respuesta);
                }
                else
                {
                    respuesta.CODIGO = 0;
                    respuesta.MENSAJE = "No hay mas detalle acerca del empleado";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

        [HttpPut]
        [Route("CambiarEstadoCliente")]
        public async Task<IActionResult> CambiarEstadoCliente(long? IdCLIENTE)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@ID_CLIENTE", IdCLIENTE);
                var request = await contexto.ExecuteAsync("CambiarEstadoCliente", parametros,
                   commandType: System.Data.CommandType.StoredProcedure);
                if (request > 0)
                {
                    respuesta.CODIGO = 1;
                    respuesta.MENSAJE = "OK";
                    respuesta.CONTENIDO = true;
                    return Ok(respuesta);
                }
                else
                {
                    respuesta.CODIGO = 0;
                    respuesta.MENSAJE = "Se presentó un inconveniente actualizando el estado";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

        [HttpPut]
        [Route("CambiarEstadoClienteLista")]
        public async Task<IActionResult> CambiarEstadoClienteLista(long? IdCliente)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@ID_CLIENTE", IdCliente);
                var request = await contexto.ExecuteAsync("CambiarEstadoClienteLista", parametros,
                   commandType: System.Data.CommandType.StoredProcedure);
                if (request > 0)
                {
                    respuesta.CODIGO = 1;
                    respuesta.MENSAJE = "OK";
                    respuesta.CONTENIDO = true;
                    return Ok(respuesta);
                }
                else
                {
                    respuesta.CODIGO = 0;
                    respuesta.MENSAJE = "Se presentó un inconveniente actualizando el estado";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }
        /// <summary>
        /// SPs para proyecto
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("AgregarProyecto")]
        public async Task<IActionResult> AgregarProyecto(Actividad entidad)
        {
            Respuesta respuesta = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var result = await context.ExecuteAsync("AgregarProyecto", new { entidad.NOMBRE, entidad.TOTAL_HORAS, entidad.INICIO_FECHA, entidad.FINAL_FECHA, entidad.COD_PROYECTO, entidad.COMENTARIO, entidad.CONTACTO_ID, entidad.ID_CLIENTE }, commandType: CommandType.StoredProcedure);

                if (result > 0)
                {
                    respuesta.CODIGO = 1;
                    respuesta.MENSAJE = "OK";
                    respuesta.CONTENIDO = true;
                    return Ok(respuesta);
                }
                else
                {
                    respuesta.CONTENIDO = 0;
                    respuesta.MENSAJE = "No se logro agregar el cliente";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

        [HttpPut]
        [Route("ModificarProyecto")]
        public async Task<IActionResult> ModificarProyecto(Actividad entidad)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@ID_PROYECTO", entidad.ID_PROYECTO);
                parametros.Add("@NOMBRE", entidad.NOMBRE);
                parametros.Add("@TOTAL_HORAS", entidad.TOTAL_HORAS);
                parametros.Add("@INICIO_FECHA", entidad.INICIO_FECHA);
                parametros.Add("@FINAL_FECHA", entidad.FINAL_FECHA);
                parametros.Add("@COD_PROYECTO", entidad.COD_PROYECTO);
                parametros.Add("@COMENTARIO", entidad.COMENTARIO);
                parametros.Add("@CONTACTO_ID", entidad.CONTACTO_ID);
                parametros.Add("@CLIENTE_ID", entidad.ID_CLIENTE);

                var request = await contexto.ExecuteAsync("ModificarProyecto", parametros,
                   commandType: System.Data.CommandType.StoredProcedure);
                if (request > 0)
                {
                    respuesta.CODIGO = 1;
                    respuesta.MENSAJE = "OK";
                    respuesta.CONTENIDO = true;
                    return Ok(respuesta);
                }
                else
                {
                    respuesta.CODIGO = 0;
                    respuesta.MENSAJE = "Se presentó un inconveniente actualizando la información";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

        [HttpPut]
        [Route("CambiarEstadoProyecto")]
        public async Task<IActionResult> CambiarEstadoProyecto(long? IdPROYECTO)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@ID_PROYECTO", IdPROYECTO);
                var request = await contexto.ExecuteAsync("CambiarEstadoProyecto", parametros,
                   commandType: System.Data.CommandType.StoredProcedure);
                if (request > 0)
                {
                    respuesta.CODIGO = 1;
                    respuesta.MENSAJE = "OK";
                    respuesta.CONTENIDO = true;
                    return Ok(respuesta);
                }
                else
                {
                    respuesta.CODIGO = 0;
                    respuesta.MENSAJE = "Se presentó un inconveniente actualizando el estado";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

        [HttpGet]
        [Route("ListarProyectos")]
        public async Task<IActionResult> ListarProyectos()
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var request = await contexto.QueryAsync("ListarProyectos", new { },
                      commandType: System.Data.CommandType.StoredProcedure);
                if (request != null)
                {
                    respuesta.CODIGO = 1;
                    respuesta.MENSAJE = "OK";
                    respuesta.CONTENIDO = request.ToList();
                    return Ok(respuesta);
                }
                else
                {
                    respuesta.CODIGO = 0;
                    respuesta.MENSAJE = "No hay proyectos registrados";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

        [HttpGet]
<<<<<<< Updated upstream
=======
        [Route("ListarProyectosNombres")]
        public async Task<IActionResult> ListarProyectosNombres()
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var request = await contexto.QueryAsync("ListarProyectosNombres", new { },
                      commandType: System.Data.CommandType.StoredProcedure);
                if (request != null)
                {
                    respuesta.CODIGO = 1;
                    respuesta.MENSAJE = "OK";
                    respuesta.CONTENIDO = request.ToList();
                    return Ok(respuesta);
                }
                else
                {
                    respuesta.CODIGO = 0;
                    respuesta.MENSAJE = "No hay proyectos registrados";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

        [HttpGet]
        [Route("MostrarTodosEmpleados")]
        public async Task<IActionResult> MostrarTodosEmpleados()
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var request = await contexto.QueryAsync("MostrarTodosEmpleados", new { },
                      commandType: System.Data.CommandType.StoredProcedure);
                if (request != null)
                {
                    respuesta.CODIGO = 1;
                    respuesta.MENSAJE = "OK";
                    respuesta.CONTENIDO = request.ToList();
                    return Ok(respuesta);
                }
                else
                {
                    respuesta.CODIGO = 0;
                    respuesta.MENSAJE = "No hay empleados registrados";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

		[HttpGet]
		[Route("MostrarTodosEmpleadosContactos")]
		public async Task<IActionResult> MostrarTodosEmpleadosContactos()
		{
			Respuesta respuesta = new Respuesta();
			using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
			{
				var request = await contexto.QueryAsync("MostrarTodosEmpleadosContactos", new { },
					  commandType: System.Data.CommandType.StoredProcedure);
				if (request != null)
				{
					respuesta.CODIGO = 1;
					respuesta.MENSAJE = "OK";
					respuesta.CONTENIDO = request.ToList();
					return Ok(respuesta);
				}
				else
				{
					respuesta.CODIGO = 0;
					respuesta.MENSAJE = "No hay contactos/empleados registrados";
					respuesta.CONTENIDO = false;
					return Ok(respuesta);
				}
			}
		}

		[HttpGet]
>>>>>>> Stashed changes
        [Route("DetallarProyecto")]
        public async Task<IActionResult> DetallarProyecto(long IdPROYECTO)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@ID_PROYECTO", IdPROYECTO);
                var request = (await contexto.QueryAsync("DetallarProyecto",
                    parametros ,
                    commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();
                if (request != null)
                {
                    respuesta.CODIGO = 1;
                    respuesta.MENSAJE = "OK";
                    respuesta.CONTENIDO = request;
                    return Ok(respuesta);
                }
                else
                {
                    respuesta.CODIGO = 0;
                    respuesta.MENSAJE = "No hay mas detalle acerca del proyecto";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }


        [HttpPost]
        [Route("AgregarEmpleadoProyecto")]
        public async Task<IActionResult> AgregarEmpleadoProyecto(Actividad entidad)
        {
            Respuesta respuesta = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var result = await context.ExecuteAsync("AgregarEmpleadoProyecto", new { entidad.EMPLEADO_ID, entidad.PROYECTO_ID }, commandType: CommandType.StoredProcedure);

                if (result > 0)
                {
                    respuesta.CODIGO = 1;
                    respuesta.MENSAJE = "OK";
                    respuesta.CONTENIDO = true;
                    return Ok(respuesta);
                }
                else
                {
                    respuesta.CONTENIDO = 0;
                    respuesta.MENSAJE = "No se logro agregar el cliente";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

      
        [HttpGet]
        [Route("ListarEmpleadosPorProyecto")]
        public async Task<IActionResult> ListarEmpleadosPorProyecto(Actividad entidad )
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@PROYECTO_ID", entidad.ID_PROYECTO);
                var request = await contexto.QueryAsync("ListarEmpleadosPorProyecto", parametros,
                    commandType: System.Data.CommandType.StoredProcedure);
                if (request != null)
                {
                    respuesta.CODIGO = 1;
                    respuesta.MENSAJE = "OK";
                    respuesta.CONTENIDO = request.ToList();
                    return Ok(respuesta);
                }
                else
                {
                    respuesta.CODIGO = 0;
                    respuesta.MENSAJE = "La información del empleado no se encuentra registrada";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }




        /// <summary>
        /// SPs para el empleado - ACTIVIDADES
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("IngresorRegistroActividad")]
        public async Task<IActionResult> IngresorRegistroActividad(Actividad entidad)
        {
            Respuesta respuesta = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var result = await context.ExecuteAsync("IngresorRegistroActividad", new { entidad.FECHA_INICIO, entidad.FECHA_FIN, entidad.TOTALHORAS, entidad.DETALLE, entidad.EMPLEADO_ID, entidad.PROYECTO_ID}, commandType: CommandType.StoredProcedure);

                if (result > 0)
                {
                    respuesta.CODIGO = 1;
                    respuesta.MENSAJE = "OK";
                    respuesta.CONTENIDO = true;
                    return Ok(respuesta);
                }
                else
                {
                    respuesta.CONTENIDO = 0;
                    respuesta.MENSAJE = "No se logro registrar la actividad";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

        [HttpGet]
        [Route("ListarProyectosPorEmpleado")]
        public async Task<IActionResult> ListarProyectosPorEmpleado(long iDEmpleado)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@EMPLEADO_ID", iDEmpleado);
                
                var request = (await contexto.QueryAsync<Actividad>("ListarProyectosPorEmpleado",
                    parametros,
                    commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();
                if (request != null)
                {
                    respuesta.CODIGO = 1;
                    respuesta.MENSAJE = "OK";
                    respuesta.CONTENIDO = request;
                    return Ok(respuesta);
                }
                else
                {
                    respuesta.CODIGO = 0;
                    respuesta.MENSAJE = "No hay mas detalle acerca del proyecto";
                    return Ok(respuesta);
                }
            }
        }

        [HttpGet]
        [Route("ListarTodasActividades")]
        public async Task<IActionResult> ListarTodasActividades()
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var request = await contexto.QueryAsync("ListarTodasActividades", new { },
                      commandType: System.Data.CommandType.StoredProcedure);
                if (request != null)
                {
                    respuesta.CODIGO = 1;
                    respuesta.MENSAJE = "OK";
                    respuesta.CONTENIDO = request.ToList();
                    return Ok(respuesta);
                }
                else
                {
                    respuesta.CODIGO = 0;
                    respuesta.MENSAJE = "No hay proyectos registrados";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

        [HttpGet]
        [Route("ListarActividadesPorEmpleado")]
        public async Task<IActionResult> ListarActividadesPorEmpleado(long IdEMPLEADO)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var request = await contexto.QueryAsync("ListarActividadesPorEmpleado", new { },
                      commandType: System.Data.CommandType.StoredProcedure);
                if (request != null)
                {
                    respuesta.CODIGO = 1;
                    respuesta.MENSAJE = "OK";
                    respuesta.CONTENIDO = request.ToList();
                    return Ok(respuesta);
                }
                else
                {
                    respuesta.CODIGO = 0;
                    respuesta.MENSAJE = "No hay proyectos registrados";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

        [HttpPut]
        [Route("ModificarRegistroActividad")]
        public async Task<IActionResult> ModificarRegistroActividad(Actividad entidad)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@ID_REGISTROACTIVIDAD", entidad.ID_PROYECTO);
                parametros.Add("@FECHA_INICIO", entidad.NOMBRE);
                parametros.Add("@FECHA_FIN", entidad.TOTAL_HORAS);
                parametros.Add("@TOTALHORAS", entidad.INICIO_FECHA);
                parametros.Add("@DETALLE", entidad.FINAL_FECHA);
                parametros.Add("@ESTADO", entidad.COD_PROYECTO);
                parametros.Add("@EMPLEADO_ID", entidad.COMENTARIO);
                

                var request = await contexto.ExecuteAsync("ModificarRegistroActividad", parametros,
                   commandType: System.Data.CommandType.StoredProcedure);
                if (request > 0)
                {
                    respuesta.CODIGO = 1;
                    respuesta.MENSAJE = "OK";
                    respuesta.CONTENIDO = true;
                    return Ok(respuesta);
                }
                else
                {
                    respuesta.CODIGO = 0;
                    respuesta.MENSAJE = "Se presentó un inconveniente actualizando la información";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }
        [HttpGet]
        [Route("DetallarRegistroActividadPorID")]
        public async Task<IActionResult> DetallarRegistroActividadPorID(long ID_REGISTROACTIVIDAD)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@ID_REGISTROACTIVIDAD", ID_REGISTROACTIVIDAD);
                var request = (await contexto.QueryAsync("DetallarRegistroActividadPorID",
                    parametros,
                    commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();
                if (request != null)
                {
                    respuesta.CODIGO = 1;
                    respuesta.MENSAJE = "OK";
                    respuesta.CONTENIDO = request;
                    return Ok(respuesta);
                }
                else
                {
                    respuesta.CODIGO = 0;
                    respuesta.MENSAJE = "No hay mas detalle acerca del proyecto";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

        [HttpPut]
        [Route("CambiarEstadoRegistroActividad")]
        public async Task<IActionResult> CambiarEstadoRegistroActividad(long? ID_REGISTROACTIVIDAD)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@ID_REGISTROACTIVIDAD", ID_REGISTROACTIVIDAD);
                var request = await contexto.ExecuteAsync("CambiarEstadoRegistroActividad", parametros,
                   commandType: System.Data.CommandType.StoredProcedure);
                if (request > 0)
                {
                    respuesta.CODIGO = 1;
                    respuesta.MENSAJE = "OK";
                    respuesta.CONTENIDO = true;
                    return Ok(respuesta);
                }
                else
                {
                    respuesta.CODIGO = 0;
                    respuesta.MENSAJE = "Se presentó un inconveniente actualizando el estado";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }
    }
}
