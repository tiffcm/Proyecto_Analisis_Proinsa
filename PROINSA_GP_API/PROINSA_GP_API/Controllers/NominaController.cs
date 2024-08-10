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
        [Route("RegistrarNomina")]
        public async Task<IActionResult> RegistrarNomina(Nomina entidad)
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

        //En los POST se tiene que mandar objetos
        //Este hay que revisarlo porque es solo ejecutar y en el Web no puede ser POST porque se tiene que enviar datos
        [HttpPost]
        [Route("CalculoNominaInicial")]
        public async Task<IActionResult> CalculoNominaInicial()
        {
            Respuesta respuesta = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var result = await context.ExecuteAsync("CalculoNominaInicial", commandType: CommandType.StoredProcedure);

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
                    respuesta.MENSAJE = "Error al procesar el calculo";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

        //En los POST se tiene que mandar objetos
        //Este hay que revisarlo porque es solo ejecutar y en el Web no puede ser POST porque se tiene que enviar datos
        [HttpPost]
        [Route("CalculoNominaFinal")]
        public async Task<IActionResult> CalculoNominaFinal()
        {
            Respuesta respuesta = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var result = await context.ExecuteAsync("CalculoNominaFinal", commandType: CommandType.StoredProcedure);

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
                    respuesta.MENSAJE = "Error al procesar el calculo";
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

        //En los POST se tiene que mandar objetos
        //Este hay que revisarlo FROMBody?
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
                dataTable.Columns.Add("CANTIDAD", typeof(long));

                foreach (var ingreso in ingresos)
                {
                    dataTable.Rows.Add(ingreso.MONTO, ingreso.DETALLE, ingreso.INGRESO_ID, ingreso.EMPLEADO_ID, ingreso.CANTIDAD);
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

        //En los POST se tiene que mandar objetos
        //Este hay que revisarlo FROMBody?
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

        [HttpGet]
        [Route("ObtenerNominaEmpleado")]
        public async Task<IActionResult> ObtenerNominaEmpleado(int EMPLEADO_ID)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@EMPLEADO_ID", EMPLEADO_ID);

                var request = (await contexto.QueryAsync<Nomina>("ObtenerNominaMensualEmpleado", parameters, commandType: System.Data.CommandType.StoredProcedure)).ToList();

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
        [Route("ObtenerNominaMensualEmpleados")]
        public async Task<IActionResult> ObtenerNominaMensualEmpleados(DateTime fechapago)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parameters = new DynamicParameters();
                parameters.Add("@fechapago", fechapago);

                var request = (await contexto.QueryAsync<Nomina>("ObtenerNominaMensualEmpleados", parameters, commandType: System.Data.CommandType.StoredProcedure)).ToList();

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

        //En los PUT se tiene que mandar objetos
        [HttpPut]
        [Route("RevisionNomina")]
        public async Task<IActionResult> RevisionNomina(string Observaciones)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@Observaciones", Observaciones);
                var request = await contexto.ExecuteAsync("RevisionNomina", parametros,
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
                    respuesta.MENSAJE = "Error en el proceso.";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }
    }
}