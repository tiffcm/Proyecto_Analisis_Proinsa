
using Dapper;
using Microsoft.AspNetCore.Mvc;
using PROINSA_GP_API.DbConnection;
using PROINSA_GP_API.Entidad;


namespace PROINSA_GP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;

        public SolicitudController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }


        [HttpGet]
        [Route("ConsultarTipoSolicitud")]
        public async Task<IActionResult> ConsultarTipoSolicitud()
        {
            SolicitudRespuesta respuesta = new SolicitudRespuesta();

            try
            {
                using (var contexto = _dbConnection.CreateConnection())
                {
                    var request = (await contexto.QueryAsync<Solicitud>("ObtenerTiposSolicitudes",
                       commandType: System.Data.CommandType.StoredProcedure)).ToList();

                    if (request != null && request.Count > 0)
                    {
                        respuesta.CODIGO = "00";
                        respuesta.MENSAJE = "OK";
                        respuesta.DATOS = request;
                    }
                    else
                    {
                        respuesta.CODIGO = "0";
                        respuesta.MENSAJE = "No hay registros";
                    }
                }
            }
            catch (Exception)
            {
                respuesta.CODIGO = "0";
                respuesta.MENSAJE = "Error";
            }
            return Ok(respuesta);
        }

        [HttpPost]
        [Route("RegistrarSolicitud")]
        public async Task<IActionResult> RegistrarSolicitud([FromBody] Solicitud solicitud)
        {
            SolicitudRespuesta respuesta = new SolicitudRespuesta();

            try
            {
                using (var contexto = _dbConnection.CreateConnection())
                {
                    var parameters = new DynamicParameters();
                    parameters.Add("@FECHA_INICIO", solicitud.FECHA_INICIO);
                    parameters.Add("@FECHA_FINAL", solicitud.FECHA_FINAL);
                    parameters.Add("@COMENTARIO", solicitud.COMENTARIO);
                    parameters.Add("@DETALLE", solicitud.DETALLE);
                    parameters.Add("@SOLICITANTE_ID", solicitud.SOLICITANTE_ID);
                    parameters.Add("@TIPOSOLICITUD_ID", solicitud.TIPOSOLICITUD_ID);

                    var result = await contexto.ExecuteAsync("RegistrarSolicitud", parameters, commandType: System.Data.CommandType.StoredProcedure);

                    if (result > 0)
                    {
                        respuesta.CODIGO = "00";
                        respuesta.MENSAJE = "Solicitud registrada exitosamente";
                    }
                    else
                    {
                        respuesta.CODIGO = "0";
                        respuesta.MENSAJE = "No se pudo registrar la solicitud";
                    }
                }
            }
            catch (Exception ex)
            {
                respuesta.CODIGO = "0";
                respuesta.MENSAJE = $"Error: {ex.Message}";
            }
            return Ok(respuesta);
        }
    }
}