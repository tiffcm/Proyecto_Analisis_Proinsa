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

        [HttpPost]
        [Route("RegistrarIngresosNominaDetalle")]
        public async Task<IActionResult> RegistrarIngresosNominaDetalle([FromBody] List<IngresoNominaDetalle> ingresos)
        {
            Respuesta respuesta = new Respuesta();




            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var dataTable = new DataTable();
                dataTable.Columns.Add("MONTO", typeof(decimal));
                dataTable.Columns.Add("DETALLE", typeof(string));
                dataTable.Columns.Add("INGRESO_ID", typeof(long));
                dataTable.Columns.Add("EMPLEADO_ID", typeof(long));

                foreach (var ingreso in ingresos)
                {
                    dataTable.Rows.Add(ingreso.MONTO, ingreso.DETALLE, ingreso.INGRESO_ID, ingreso.EMPLEADO_ID);
                }

                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@IngresosNominaDetalle", dataTable.AsTableValuedParameter("dbo.IngresoNominaDetalleType"));

                try
                {
                    await context.OpenAsync();
                    var result = await context.ExecuteAsync(
                        "RegistrarIngresosNominaDetalle",
                        dynamicParameters,
                        commandType: CommandType.StoredProcedure
                    );

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
                        respuesta.MENSAJE = "Ha ocurrido un error al procesar la solicitud";
                        respuesta.CONTENIDO = false;
                        return Ok(respuesta);
                    }
                }
                catch (Exception ex)
                {
                    respuesta.CODIGO = 0;
                    respuesta.MENSAJE = $"Error: {ex.Message}";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }


        [HttpPost]
        [Route("RegistrarDeduccionNominaDetalle")]
        public async Task<IActionResult> RegistrarDeduccionNominaDetalle([FromBody] List<DeduccionNominaDetalle> deducciones)
        {
            Respuesta respuesta = new Respuesta();




            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var dataTable = new DataTable();
                dataTable.Columns.Add("MONTO", typeof(decimal));
                dataTable.Columns.Add("DETALLE", typeof(string));
                dataTable.Columns.Add("DEDUCCION_ID", typeof(long));
                dataTable.Columns.Add("EMPLEADO_ID", typeof(long));

                foreach (var deduccion in deducciones)
                {
                    dataTable.Rows.Add(deduccion.MONTO, deduccion.DETALLE, deduccion.DEDUCCION_ID, deduccion.EMPLEADO_ID);
                }

                var dynamicParameters = new DynamicParameters();
                dynamicParameters.Add("@DeduccionesNominaDetalle", dataTable.AsTableValuedParameter("dbo.DeduccionNominaDetalleType"));

                try
                {
                    await context.OpenAsync();
                    var result = await context.ExecuteAsync(
                        "RegistrarDeduccionesNominaDetalle",
                        dynamicParameters,
                        commandType: CommandType.StoredProcedure
                    );

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
                        respuesta.MENSAJE = "Ha ocurrido un error al procesar la solicitud";
                        respuesta.CONTENIDO = false;
                        return Ok(respuesta);
                    }
                }
                catch (Exception ex)
                {
                    respuesta.CODIGO = 0;
                    respuesta.MENSAJE = $"Error: {ex.Message}";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }





    }
}