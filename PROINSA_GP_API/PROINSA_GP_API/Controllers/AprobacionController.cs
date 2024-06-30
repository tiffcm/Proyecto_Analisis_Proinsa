using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PROINSA_GP_API.Entidad;
using System.Data;

namespace PROINSA_GP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AprobacionController(IConfiguration iConfiguration) : ControllerBase
    {


        [HttpGet]
        [Route("ObtenerAprobacionPendiente")]
        public async Task<IActionResult> ObtenerAprobacionPendiente(long id_empleado)
        {
            Respuesta respuesta = new Respuesta();

            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@id_empleado", id_empleado);

                var request = (await contexto.QueryAsync<Aprobacion>("ObtenerAprobacionPendiente", parameters, commandType: System.Data.CommandType.StoredProcedure)).ToList();

                if (request != null && request.Count > 0)
                {
                    respuesta.CODIGO = 1;
                    respuesta.MENSAJE = "OK";
                    respuesta.CONTENIDO = request;
                    return Ok(respuesta);
                }
                else
                {
                    respuesta.CODIGO = 0;
                    respuesta.MENSAJE = "No hay registros";
                    return Ok(respuesta);
                }
            }
        }



        [HttpGet]
        [Route("ObtenerAprobacionPendienteDetalle")]
        public async Task<IActionResult> ObtenerAprobacionPendienteDetalle( long ID_SOLICITUD, long id_empleado)
        {
            Respuesta respuesta = new Respuesta();

            
                using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
                {
                    var parameters = new DynamicParameters();
                    
                    parameters.Add("@ID_SOLICITUD", ID_SOLICITUD);
                    parameters.Add("@id_empleado", id_empleado);

                    var request = (await contexto.QueryAsync<Aprobacion>("ObtenerAprobacionPendienteDetalle", parameters, commandType: System.Data.CommandType.StoredProcedure)).ToList();

                    if (request != null && request.Count > 0)
                    {
                        respuesta.CODIGO = 1;
                        respuesta.MENSAJE = "OK";
                        respuesta.CONTENIDO = request;
                        return Ok(respuesta);
                    }
                    else
                    {
                        respuesta.CODIGO = 0;
                        respuesta.MENSAJE = "No hay registros";
                        return Ok(respuesta);
                    }
                }
            }


        [HttpGet]
        [Route("ObtenerAprobacionFlujo")]
        public async Task<IActionResult> ObtenerAprobacionFlujo(long ID_SOLICITUD)
        {
            Respuesta respuesta = new Respuesta();


            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parameters = new DynamicParameters();

                parameters.Add("@ID_SOLICITUD", ID_SOLICITUD);

                var request = (await contexto.QueryAsync<Aprobacion>("ObtenerAprobacionFlujo", parameters, commandType: System.Data.CommandType.StoredProcedure)).ToList();

                if (request != null && request.Count > 0)
                {
                    respuesta.CODIGO = 1;
                    respuesta.MENSAJE = "OK";
                    respuesta.CONTENIDO = request;
                    return Ok(respuesta);
                }
                else
                {
                    respuesta.CODIGO = 0;
                    respuesta.MENSAJE = "No hay registros";
                    return Ok(respuesta);
                }
            }
        }


        [HttpPut]
        [Route("ActualizarAprobacionFlujo")]
        public async Task<IActionResult> ActualizarDatosUsuario(Aprobacion aprobacion)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@id_solicitud", aprobacion.ID_SOLICITUD);
                parametros.Add("@id_empleado", aprobacion.ID_EMPLEADO);
                parametros.Add("@respuesta", aprobacion.RESPUESTA);
                parametros.Add("@comentario", aprobacion.COMENTARIO);
                var request = await contexto.ExecuteAsync("ActualizarFlujoAprobacion", parametros,
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



    }

    }

