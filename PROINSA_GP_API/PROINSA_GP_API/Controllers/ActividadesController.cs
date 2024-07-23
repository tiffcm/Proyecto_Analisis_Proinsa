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
        public async Task<IActionResult> AgregarCliente(Actividades entidad)
        {
            Respuesta respuesta = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var result = await context.ExecuteAsync("AgregarCliente", new { entidad.NOMBRE, entidad.DETALLE, entidad.PAIS, entidad.SECTOR }, commandType: CommandType.StoredProcedure);

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
        public async Task<IActionResult> ModificarCliente(Actividades entidad)
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
                parametros.Add("@ESTADO", entidad.ESTADO);

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
        public async Task<IActionResult> CambiarEstadoCliente(long IdCLIENTE)
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

        /// <summary>
        /// SPs para proyecto
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>

        [HttpPost]
        [Route("AgregarProyecto")]
        public async Task<IActionResult> AgregarProyecto(Actividades entidad)
        {
            Respuesta respuesta = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var result = await context.ExecuteAsync("AgregarProyecto", new { entidad.NOMBRE, entidad.TOTAL_HORAS, entidad.INICIO_FECHA, entidad.COD_PROYECTO, entidad.COMENTARIO, entidad.CONTACTO_ID, entidad.ID_CLIENTE }, commandType: CommandType.StoredProcedure);

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
        public async Task<IActionResult> ModificarProyecto(Actividades entidad)
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
                parametros.Add("@ESTADO", entidad.ESTADO);

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
        public async Task<IActionResult> CambiarEstadoProyecto(long ID_PROYECTO)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@ID_PROYECTO", ID_PROYECTO);
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
        [Route("DetallarProyecto")]
        public async Task<IActionResult> DetallarProyecto(long ID_PROYECTO)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@ID_PROYECTO", ID_PROYECTO);
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

        /// <summary>
        /// SPs para el empleado - ACTIVIDADES
        /// </summary>
        /// <param name="entidad"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("IngresoActividad")]
        public async Task<IActionResult> IngresoActividad(Actividades entidad)
        {
            Respuesta respuesta = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var result = await context.ExecuteAsync("IngresoActividad", new { entidad.FECHA_INICIO, entidad.FECHA_FIN, entidad.TOTALHORAS, entidad.DETALLE, }, commandType: CommandType.StoredProcedure);

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

    }
}
