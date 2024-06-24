using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using PROINSA_GP_API.Entidad;
using System.Data;


namespace PROINSA_GP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudController (IConfiguration iConfiguration) : ControllerBase
    {
        [HttpGet]
        [Route("ConsultarTipoSolicitud")]
        public async Task<IActionResult> ConsultarTipoSolicitud()
        {
            Respuesta respuesta = new Respuesta();

            try
            {
                using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
                {
                    var request = (await contexto.QueryAsync<Solicitud>("ObtenerTiposSolicitudes",
                       commandType: System.Data.CommandType.StoredProcedure)).ToList();

                    if (request != null && request.Count > 0)
                    {
                        respuesta.CODIGO = 1;
                        respuesta.MENSAJE = "OK";
                        respuesta.CONTENIDO = request;
                    }
                    else
                    {
                        respuesta.CODIGO = 0;
                        respuesta.MENSAJE = "No hay registros";
                    }
                }
            }
            catch (Exception)
            {
                respuesta.CODIGO = 0;
                respuesta.MENSAJE = "Error";
            }
            return Ok(respuesta);
        }


        [HttpPost]
        [Route("RegistrarSolicitud")]
        public async Task<IActionResult> RegistrarUsuario(Solicitud entidad)
        {
            Respuesta respuesta = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var result = await context.ExecuteAsync("RegistrarSolicitud", new { entidad.FECHA_INICIO, entidad.FECHA_FINAL, entidad.COMENTARIO, entidad.DETALLE, entidad.SOLICITANTE_ID ,entidad.TIPOSOLICITUD_ID }, commandType: CommandType.StoredProcedure);

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
                    respuesta.MENSAJE = "La información del usuario ya se encuentra registrada";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }


    }
}