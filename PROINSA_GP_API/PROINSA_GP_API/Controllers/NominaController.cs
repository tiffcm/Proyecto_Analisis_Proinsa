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

        
        [HttpPost]
        [Route("CalculoNominaInicial")]
        public async Task<IActionResult> CalculoNominaInicial(Nomina ent )
        {
            Respuesta respuesta = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var result = await context.ExecuteAsync("CalculoNominaInicial",
                    new { ent.FECHA }, commandType: CommandType.StoredProcedure);


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

        
        [HttpPost]
        [Route("CalculoNominaFinal")]
        public async Task<IActionResult> CalculoNominaFinal( Nomina ent)
        {
            Respuesta respuesta = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var result = await context.ExecuteAsync("CalculoNominaFinal",
                    new { ent.FECHA }, commandType: CommandType.StoredProcedure);

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

        [HttpPost]
        [Route("RegistrarIngresosNominaDetalle")]
        public async Task<IActionResult> RegistrarIngresosNominaDetalle(List<IngresoNominaDetalle> ingresos)
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

                    
                    SqlParameter tableParameter = new SqlParameter
                    {
                        ParameterName = "@IngresosNominaDetalle",
                        SqlDbType = SqlDbType.Structured,
                        TypeName = "dbo.IngresoNominaDetalleType", 
                        Value = dataTable
                    };

                    SqlCommand command = new SqlCommand("RegistrarIngresosNominaDetalle", context)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.Add(tableParameter);

                    await context.OpenAsync();
                    var result = await command.ExecuteNonQueryAsync();

                    if (result > 0)
                    {
                        respuesta.CODIGO = 1;
                        respuesta.MENSAJE = "OK";
                        respuesta.CONTENIDO = true;
                    }
                    else
                    {
                        respuesta.CODIGO = 0;
                        respuesta.MENSAJE = "Ha ocurrido un error al procesar la solicitud";
                        respuesta.CONTENIDO = false;
                    }
                }
           

            return Ok(respuesta);
        }


        [HttpPost]
        [Route("RegistrarDeduccionNominaDetalle")]
        public async Task<IActionResult> RegistrarDeduccionNominaDetalle(List<DeduccionNominaDetalle> deducciones)
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

                    
                    SqlParameter tableParameter = new SqlParameter
                    {
                        ParameterName = "@DeduccionesNominaDetalle",
                        SqlDbType = SqlDbType.Structured,
                        TypeName = "dbo.DeduccionNominaDetalleType", 
                        Value = dataTable
                    };

                    SqlCommand command = new SqlCommand("RegistrarDeduccionesNominaDetalle", context)
                    {
                        CommandType = CommandType.StoredProcedure
                    };
                    command.Parameters.Add(tableParameter);

                    await context.OpenAsync();
                    var result = await command.ExecuteNonQueryAsync();

                    if (result > 0)
                    {
                        respuesta.CODIGO = 1;
                        respuesta.MENSAJE = "OK";
                        respuesta.CONTENIDO = true;
                    }
                    else
                    {
                        respuesta.CODIGO = 0;
                        respuesta.MENSAJE = "Ha ocurrido un error al procesar la solicitud";
                        respuesta.CONTENIDO = false;
                    }
                }
            return Ok(respuesta);
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

        [HttpPut]
        [Route("RevisionNomina")]
        public async Task<IActionResult> RevisionNomina(Nomina ent)
        {
            Respuesta resp = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
               
                var result = await context.ExecuteAsync("RevisionNomina",
                    new { ent.OBSERVACIONES}, commandType: CommandType.StoredProcedure);

                if (result > 0)
                {
                    resp.CODIGO = 1;
                    resp.MENSAJE = "OK";
                    resp.CONTENIDO= true;
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


        [HttpPut]
        [Route("AprobacionNomina")]
        public async Task<IActionResult> AprobacionNomina(Nomina ent)
        {
            Respuesta resp = new Respuesta();

            using (var context = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {

                var result = await context.ExecuteAsync("AprobarNominaPeriodo",
                    new { ent.ID_EMPLEADO}, commandType: CommandType.StoredProcedure);

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