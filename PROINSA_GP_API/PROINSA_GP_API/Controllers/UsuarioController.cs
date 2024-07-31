using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using PROINSA_GP_API.Entidad;

namespace PROINSA_GP_API.Controllers
{
    /// <summary>
    /// Este controlador se encargará de administrar las solicitudes de los usuarios.
    /// </summary>
    /// <param name="iConfiguration">Inyección de dependencia para manejo cadenas de conexión</param>
    /// <author>Brandon Ruiz Miranda</author>
    /// <version>1.6</version>
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController(IConfiguration iConfiguration) : ControllerBase
    {
        /// <summary>
        /// Esta acción se encargará de pasarle a la base de datos el correo para poder hacer la consulta
        /// del rol y de los datos que el usuario necesita para el apartado de "Mi cuenta".
        /// </summary>        
        /// <param name="correo">Correo del usuario recuperado con Identity</param>
        /// <returns>Devuele la respuesta de la acción</returns>
        [HttpGet]
        [Route("ConsultarDatosEmpleado")]
        public async Task<IActionResult> ConsultarDatosEmpleado(string correo)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var request = (await contexto.QueryAsync("ConsultarDatosEmpleado",
                    new { correo },
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
                    respuesta.MENSAJE = "La información del empleado no se encuentra registrada";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }


        /// <summary>
        /// Se encarga de enviar la información que se quiere actualizar en un paquete.
        /// </summary>
        /// <param name="entidad">Datos del usuario enviados como un objeto</param>
        /// <returns>Confirmación de éxito o no en la actualización de la información</returns>
        [HttpPut]
        [Route("ActualizarDatosUsuario")]
        public async Task<IActionResult> ActualizarDatosUsuario(Usuario usuario)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@ID_EMPLEADO", usuario.ID_EMPLEADO);
                parametros.Add("@DIRRECCION", usuario.DIRRECION);
                parametros.Add("@FOTO", usuario.FOTO);
                parametros.Add("@TIPO_FOTO", usuario.TIPO_FOTO);
                if (usuario.TELEFONOS != null && usuario.TELEFONOS.Any())
                {
                    if (usuario.TELEFONOS.Count == 1)
                    {
                        parametros.Add($"@ID_TELEFONO{1}", usuario.TELEFONOS[0].ID_TELEFONO);
                        parametros.Add($"@TELEFONO{1}", usuario.TELEFONOS[0].TELEFONO);
                        parametros.Add($"@ID_TELEFONO{2}", usuario.TELEFONOS[0].ID_TELEFONO);
                        parametros.Add($"@TELEFONO{2}", usuario.TELEFONOS[0].TELEFONO);
                    }
                    else
                    {
                        for (int i = 0; i < usuario.TELEFONOS.Count; i++)
                        {
                            parametros.Add($"@ID_TELEFONO{i + 1}", usuario.TELEFONOS[i].ID_TELEFONO);
                            parametros.Add($"@TELEFONO{i + 1}", usuario.TELEFONOS[i].TELEFONO);
                        }
                    }
                }

                var request = await contexto.ExecuteAsync("ActualizarDatosUsuario", parametros,
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
                    respuesta.MENSAJE = "Se presentó un inconveniente actualizando su información";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

        /// <summary>
        /// Esta acción se encarga de traer todos los teléfonos que tiene registrados un empleado
        /// utilizando para ello el Id.
        /// </summary>        
        /// <param name="idEmpleado">Id  del usuario recuperado de otro SP</param>
        /// <returns>Devuele la respuesta de la acción</returns>
        [HttpGet]
        [Route("ObtenerTelefonosUsuario")]
        public async Task<IActionResult> ObtenerTelefonosUsuario(long idEmpleado)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@ID_EMPLEADO", idEmpleado);
                var request = await contexto.QueryAsync<Telefono>("ObtenerTelefonosUsuario", parametros,
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

        [HttpGet]
        [Route("ConsultarEmpleados")]
        public async Task<IActionResult> ConsultarEmpleados()
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var request = await contexto.QueryAsync<SelectListItem>("ConsultarEmpleados",
                    new { },
                    commandType: System.Data.CommandType.StoredProcedure);
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
                    respuesta.MENSAJE = "No hay tipos de documento registrados";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

        //---------------------
        /// ADMINISTRADOR
        /// VER LISTA DE USUARIOS
        [HttpGet] // funcional
        [Route("MostrarInfoVistaAdmin")]
        public async Task<IActionResult> MostrarInfoVistaAdmin()
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var request = await contexto.QueryAsync("MostrarInfoVistaAdmin", new { },
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

        /// ACTUALIZAR 
        [HttpPut]
        [Route("EditarDatosVistaAdmin")]
        public async Task<IActionResult> EditarDatosVistaAdmin(Usuario usuario)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
               
                    var parametros = new DynamicParameters();
                    parametros.Add("@ID_EMPLEADO", usuario.ID_EMPLEADO);
                    parametros.Add("@IDENTIFICACION", usuario.IDENTIFICACION);
                    parametros.Add("@NOMBRECOMPLETO", usuario.NOMBRECOMPLETO);
                    parametros.Add("@SALARIO", usuario.SALARIO);
                    
                    parametros.Add("@CORREO_ID", usuario.CORREO_ID);
                    parametros.Add("@CARGO_ID", usuario.ID_CARGO);
                    parametros.Add("@HORARIOLABORAL_ID", usuario.ID_HORARIOLABORAL);
                    parametros.Add("@DEPARTAMENTO_ID", usuario.ID_DEPARTAMENTO);
                    parametros.Add("@ROL_ID", usuario.IDROL);
                    parametros.Add("@DIRRECCION", usuario.DIRRECION);
                    if (usuario.TELEFONOS != null && usuario.TELEFONOS.Any())
                    {
                        if (usuario.TELEFONOS.Count == 1)
                        {
                            parametros.Add($"@ID_TELEFONO{1}", usuario.TELEFONOS[0].ID_TELEFONO);
                            parametros.Add($"@TELEFONO{1}", usuario.TELEFONOS[0].TELEFONO);
                            parametros.Add($"@ID_TELEFONO{2}", usuario.TELEFONOS[0].ID_TELEFONO);
                            parametros.Add($"@TELEFONO{2}", usuario.TELEFONOS[0].TELEFONO);
                        }
                        else
                        {
                            for (int i = 0; i < usuario.TELEFONOS.Count; i++)
                            {
                                parametros.Add($"@ID_TELEFONO{i + 1}", usuario.TELEFONOS[i].ID_TELEFONO);
                                parametros.Add($"@TELEFONO{i + 1}", usuario.TELEFONOS[i].TELEFONO);
                            }
                        }
                    }

                    var request = await contexto.ExecuteAsync("EditarDatosVistaAdmin", parametros,
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
                        respuesta.MENSAJE = "Se presentó un inconveniente actualizando su información";
                        respuesta.CONTENIDO = false;
                        return Ok(respuesta);
                    }
               
            }
        }

        [HttpGet] // funcional
        [Route("MostrarEmpleadoVistaAdmin")]
        public async Task<IActionResult> MostrarEmpleadoVistaAdmin(long? idEmpleado)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@ID_EMPLEADO", idEmpleado);
                var request = (await contexto.QueryAsync("MostrarEmpleadoVistaAdmin", parametros,
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
                    respuesta.MENSAJE = "La información del empleado no se encuentra registrada";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

		[HttpGet]
        [Route("MostrarTodosCargos")] // Se debe crear procedimiento en SQL
        public async Task<IActionResult> MostrarTodosCargos()
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var request = await contexto.QueryAsync("MostrarTodosCargos", new { },
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
		 
		
        [HttpPut]
        [Route("CambiarEstadoUsuarioAdmin")] // Se debe crear procedimiento en SQL
		public async Task<IActionResult> CambiarEstadoUsuarioAdmin(long IdEmpleado)
		{
			Respuesta respuesta = new Respuesta();
			using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
			{
				var parametros = new DynamicParameters();
				parametros.Add("@ID_EMPLEADO", IdEmpleado);
				var request = await contexto.ExecuteAsync("CambiarEstadoUsuarioAdmin", parametros,
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
					respuesta.MENSAJE = "Se presentó un inconveniente actualizando su información";
					respuesta.CONTENIDO = false;
					return Ok(respuesta);
				}
			}
		}
        
		
        [HttpGet]
        [Route("MostrarTodosDepartamentos")] // Se debe crear procedimiento en SQL
		public async Task<IActionResult> MostrarTodosDepartamentos()
		{
			Respuesta respuesta = new Respuesta();
			using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
			{
				var request = await contexto.QueryAsync("MostrarTodosDepartamentos", new { },
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
					respuesta.MENSAJE = "No hay departamentos";
					respuesta.CONTENIDO = false;
					return Ok(respuesta);
				}
			}
		}


        [HttpGet]
        [Route("MostrarTodosHorarios")] // Se debe crear procedimiento en SQL
        public async Task<IActionResult> MostrarTodosHorarios()
		{
			Respuesta respuesta = new Respuesta();
			using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
			{
				var request = await contexto.QueryAsync("MostrarTodosHorarios", new { },
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
					respuesta.MENSAJE = "No hay horarios";
					respuesta.CONTENIDO = false;
					return Ok(respuesta);
				}
			}
		}
        
		
        [HttpGet]
        [Route("MostrarTodosRoles")] // Se debe crear procedimiento en SQL
        public async Task<IActionResult> MostrarTodosRoles()
		{
			Respuesta respuesta = new Respuesta();
			using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
			{
				var request = await contexto.QueryAsync("MostrarTodosRoles", new { },
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
					respuesta.MENSAJE = "No hay roles";
					respuesta.CONTENIDO = false;
					return Ok(respuesta);
				}
			}
		}
        
        //++++++++

		[HttpGet]
        [Route("ObtenerHorarioLaboralEmpleado")]
        public async Task<IActionResult> ObtenerHorarioLaboralEmpleado(string correo)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var request = (await contexto.QueryAsync("ObtenerHorarioLaboralEmpleado",
                    new { correo },
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
                    respuesta.MENSAJE = "La información del empleado no se encuentra registrada";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }
    }
}
