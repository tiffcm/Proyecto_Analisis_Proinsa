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
    public class NominaController(IConfiguration iConfiguration) : ControllerBase
    {
        

        [HttpPost]
        [Route("RegistrarActualizarIngreso")]
        public async Task<IActionResult> RegistrarActualizarIngreso(Nomina entidad)
        {
            Respuesta respuesta = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var result = await context.ExecuteAsync("RegistrarNomina", new { entidad.DESCRIPCION, entidad.OBSERVACIONES, entidad.TipoNomina, entidad.CreadorID }, commandType: CommandType.StoredProcedure);

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
                    respuesta.MENSAJE = "Ha ocurrido un error al procesar la solicitud";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

        [HttpGet]
        [Route("ConsultarTiposNomina")]
        public async Task<IActionResult> ConsultarTiposNomina()
        {
            Respuesta respuesta = new Respuesta();

            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parameters = new DynamicParameters();
                

                var request = (await contexto.QueryAsync<Nomina>("ObtenerTipoNomina", commandType: System.Data.CommandType.StoredProcedure)).ToList();

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
    }
}