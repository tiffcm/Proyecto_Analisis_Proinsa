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

                var result = await context.ExecuteAsync("Registrar_Documento", parametros ,
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

                var request = (await contexto.QueryAsync<Documento>("Consultar_DocumentoEmpleado", parameters,
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
                var request = await contexto.QueryAsync<SelectListItem>("Consultar_TiposDocumento",
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

        [HttpPut]
        [Route("ActualizarDocumento")]
        public async Task<IActionResult> ActualizarDocumento(Documento entidad)
        {
            Respuesta respuesta = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@ID_EMPLEADODOCUMENTO", entidad.ID_EMPLEADODOCUMENTO);
                parametros.Add("@NOMBRE_DOCUMENTO", entidad.NOMBRE_DOCUMENTO);
                parametros.Add("@COMENTARIO", entidad.COMENTARIO);
                if (entidad.DOCUMENTO != null)
                {
                    parametros.Add("@DOCUMENTO", entidad.DOCUMENTO);
                }
                else
                {
                    byte[] bytes = new byte[] { 0x4E };
                    parametros.Add("@DOCUMENTO", bytes);
                }                
                parametros.Add("@TIPODOCUMENTO_ID", entidad.TIPODOCUMENTO_ID);

                var result = await context.ExecuteAsync("ActualizarDocumento", parametros,
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
                    respuesta.MENSAJE = "No puede actualizar ese documento debido a un error.";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

        [HttpDelete]
        [Route("EliminarDocumento")]
        public async Task<IActionResult> EliminarDocumento(long ID_EMPLEADODOCUMENTO)
        {
            Respuesta respuesta = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@ID_EMPLEADODOCUMENTO", ID_EMPLEADODOCUMENTO);

                var result = await context.ExecuteAsync("EliminarDocumento", parametros,
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
                    respuesta.MENSAJE = "No puede eliminar ese documento debido a un error.";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }
    }
}
