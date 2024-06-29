using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PROINSA_GP_API.Entidad;
using System.Data;

namespace PROINSA_GP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HorarioLaboralController(IConfiguration iConfiguration) : ControllerBase
    {

        [HttpGet]
        [Route("ConsultarHorarioDisponible")]
        public async Task<IActionResult> ConsultarHorarioDisponible(long id_empleado)
        {
            Respuesta respuesta = new Respuesta();

            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@id_empleado", id_empleado);

                var request = (await contexto.QueryAsync<HorarioLaboral>("ObtenerHorariosDisponibles", parameters, commandType: System.Data.CommandType.StoredProcedure)).ToList();

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

        [HttpPost]
        [Route("RegistrarSolicitudCambioHorario")]
        public async Task<IActionResult> RegistrarSolicitudCambioHorario(Solicitud entidad)
        {
            Respuesta respuesta = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var result = await context.ExecuteAsync("RegistrarSolicitudCambioHorario", new
                {
                    entidad.COMENTARIO,
                    entidad.DETALLE,
                    entidad.SOLICITANTE_ID,
                    entidad.ID_HORARIOLABORAL
                }, commandType: CommandType.StoredProcedure);

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