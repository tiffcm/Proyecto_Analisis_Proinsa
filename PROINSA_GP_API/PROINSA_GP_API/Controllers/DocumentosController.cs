using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;
using PROINSA_GP_API.Entidad;
using System.Data;

namespace PROINSA_GP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentosController (IConfiguration iConfiguration) : ControllerBase
    {
        [HttpPost]
        [Route("RegistrarDocumento")]
        public async Task<IActionResult> RegistrarDocumento(Documento entidad)
        {
            Respuesta respuesta = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@NOMBRE_DOCUMENTO", entidad.NOMBRE_DOCUMENTO);
                parametros.Add("@COMENTARIO", entidad.COMENTARIO);
                parametros.Add("@TIPODOCUMENTO_ID", entidad.TIPODOCUMENTO_ID);
                parametros.Add("@EMPLEADO_ID", entidad.EMPLEADO_ID);
                parametros.Add("@DOCUMENTO", entidad.DOCUMENTO);

                var result = await context.ExecuteAsync("REGISTRAR_DOCUMENTO", parametros ,
                commandType: CommandType.StoredProcedure);

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
                    respuesta.MENSAJE = "No puede registrar ese documento debido a un error.";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

        [HttpGet][Route("ConsultarDocumentosEmpleado")]
        public async Task<IActionResult> ConsultarDocumentosEmpleado(long EMPLEADO_ID)
        {
            Respuesta respuesta = new Respuesta();

            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@EMPLEADO_ID", EMPLEADO_ID);

                var request = (await contexto.QueryAsync<Documento>("CONSULTAR_DOCUMENTOEMPLEADO", parameters,
                    commandType: System.Data.CommandType.StoredProcedure)).ToList();

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

        [HttpGet][Route("ConsultarTiposDocumento")]
        public async Task<IActionResult> ConsultarTiposDocumento()
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var request = await contexto.QueryAsync<SelectListItem>("CONSULTAR_TIPOSDOCUMENTO",
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
    }
}
