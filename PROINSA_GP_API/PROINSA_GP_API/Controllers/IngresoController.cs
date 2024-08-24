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
    public class IngresoController(IConfiguration iConfiguration) : ControllerBase
    {        
        [HttpPost]
        [Route("RegistrarActualizarIngreso")]
        public async Task<IActionResult> RegistrarActualizarIngreso(Ingreso entidad)
        {
            Respuesta respuesta = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var result = await context.ExecuteAsync("RegistrarActualizarIngreso", new { entidad.NOMBRE_INGRESO, entidad.DESCRIPCION, entidad.MONTO, entidad.PORCENTAJE }, commandType: CommandType.StoredProcedure);

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
        [Route("ObtenerIngresos")]
        public async Task<IActionResult> ObtenerIngresos()
        {
            Respuesta respuesta = new Respuesta();

            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parameters = new DynamicParameters();
                

                var request = (await contexto.QueryAsync<Ingreso>("ObtenerIngresos", commandType: System.Data.CommandType.StoredProcedure)).ToList();

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
        [Route("ObtenerIngresoDetalle")]
        public async Task<IActionResult> ObtenerIngresoDetalle(long INGRESO_ID)
        {
            Respuesta respuesta = new Respuesta();

            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var request = await contexto.QueryFirstOrDefaultAsync<IngresosDeduccionesDetalle>("ObtenerIngresoDetalle",
                    new { INGRESO_ID },
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
                    respuesta.MENSAJE = "No hay registros";
                    return Ok(respuesta);
                }
            }
        }

        [HttpDelete]
		[Route("EliminarIngresoEmpleado")]
		public async Task<IActionResult> EliminarIngresoEmpleado(long ID_INGRESONOMINADETALLE)
		{
			Respuesta respuesta = new Respuesta();


			using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
			{
				var result = await context.ExecuteAsync("EliminarIngresoEmpleado",
					new { ID_INGRESONOMINADETALLE }, commandType: CommandType.StoredProcedure);

				if (result > 0)
				{
					respuesta.CODIGO = 1;
					respuesta.MENSAJE = "OK";
					respuesta.CONTENIDO = true;
					return Ok(respuesta);
				}
				else
				{
					respuesta.CODIGO = 0;
					respuesta.MENSAJE = "No es permitido eliminar";
					respuesta.CONTENIDO = false;
					return Ok(respuesta);
				}
			}
		}

		[HttpPut]
		[Route("ActualizarIngresoNomina")]
		public async Task<IActionResult> ActualizarIngresoNomina(IngresosDeduccionesDetalle ent)
		{
			Respuesta resp = new Respuesta();

			using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
			{

				var result = await context.ExecuteAsync("ActualizarIngresoEmpleado",
					new
					{
						ent.CONSECUTIVO,
						ent.MONTO,
						ent.DETALLE,
						ent.CANTIDAD,
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