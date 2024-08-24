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
    public class DeduccionController(IConfiguration iConfiguration) : ControllerBase
    {
        

        [HttpPost]
        [Route("RegistrarActualizarDeduccion")]
        public async Task<IActionResult> RegistrarActualizarDeduccion(Deduccion entidad)
        {
            Respuesta respuesta = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var result = await context.ExecuteAsync("RegitsrarActualizarDeduccion", new { entidad.NOMBRE_DEDUCCION, entidad.DESCRIPCION, entidad.PORCENTAJE }, commandType: CommandType.StoredProcedure);

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
        [Route("ObtenerDeducciones")]
        public async Task<IActionResult> ObtenerDeducciones()
        {
            Respuesta respuesta = new Respuesta();

            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parameters = new DynamicParameters();
                

                var request = (await contexto.QueryAsync<Deduccion>("ObtenerDeducciones", commandType: System.Data.CommandType.StoredProcedure)).ToList();

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
        [Route("ObtenerDeduccionDetalle")]
        public async Task<IActionResult> ObtenerDeduccionDetalle(long DEDUCCION_ID)
        {
            Respuesta respuesta = new Respuesta();

            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var request = (await contexto.QueryAsync<IngresosDeduccionesDetalle>("ObtenerDeduccionDetalle",
                    new { DEDUCCION_ID },
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
                    respuesta.MENSAJE = "No hay registros";
                    return Ok(respuesta);
                }
            }
        }

        [HttpDelete]
		[Route("EliminarDeduccionEmpleado")]
		public async Task<IActionResult> EliminarDeduccionEmpleado(long ID_DEDUCCION_NOMINADETALLE)
		{
			Respuesta respuesta = new Respuesta();

			using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
			{
				var result = await context.ExecuteAsync("EliminarDeduccionEmpleado",
					new { @ID_DEDUCCION_NOMINADETALLE }, commandType: CommandType.StoredProcedure);


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
					respuesta.MENSAJE = "No es permitido eliminar";
					respuesta.CONTENIDO = false;
					return Ok(respuesta);
				}
			}
		}

		[HttpPut]
		[Route("ActualizarDeduccionNomina")]
		public async Task<IActionResult> ActualizarDeduccionNomina(IngresosDeduccionesDetalle ent)
		{
			Respuesta resp = new Respuesta();

			using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
			{

				var result = await context.ExecuteAsync("ActualizarDeduccionEmpleado",
					new
					{
						ent.CONSECUTIVO,
						ent.MONTO,
						ent.DETALLE,
						ent.TIPO_ID
					}, commandType: CommandType.StoredProcedure);

				if (result > 0)
				{
					resp.CODIGO = 1;
					resp.MENSAJE = "OK";
					resp.CONTENIDO = true;
					return Ok(resp);
				}
				else
				{
					resp.CODIGO = 0;
					resp.MENSAJE = "Ha ocurrido un error al actualizar la nómina.";
					resp.CONTENIDO = false;
					return Ok(resp);
				}
			}
		}
	}
}