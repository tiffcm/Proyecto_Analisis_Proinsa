using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient;
using PROINSA_GP_API.Entidad;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace PROINSA_GP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportesController(IConfiguration iConfiguration) : ControllerBase
    {
        [HttpGet]
        [Route("DatosEmpleadoNominaReporte")]
        public async Task<IActionResult> DatosEmpleadoNominaReporte(long? IdEMPLEADO)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@EMPLEADO_ID", IdEMPLEADO);
                var request = (await contexto.QueryAsync("DatosEmpleadoNominaReporte", parametros,
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
                    respuesta.MENSAJE = "No se encuentran los datos del empleado";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

        [HttpGet]
        [Route("DatosNominaEmpleadoDeduccionesReporte")]
        public async Task<IActionResult> DatosNominaEmpleadoDeduccionesReporte(Reporte report)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@FechaSeleccionada", report.FechaSeleccionada);
                parametros.Add("@EMPLEADO_ID", report.EMPLEADO_ID);
                var request = (await contexto.QueryAsync("DatosNominaEmpleadoDeduccionesReporte", parametros,
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
                    respuesta.MENSAJE = "No se encuentran los datos del reporte Deducciones";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

        [HttpGet]
        [Route("DatosNominaEmpleadoIngresosReporte")]
        public async Task<IActionResult> DatosNominaEmpleadoIngresosReporte(Reporte report)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@FechaSeleccionada", report.FechaSeleccionada);
                parametros.Add("@EMPLEADO_ID", report.EMPLEADO_ID);
                var request = (await contexto.QueryAsync("DatosNominaEmpleadoIngresosReporte", parametros,
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
                    respuesta.MENSAJE = "No se encuentran los datos del reporte Ingresos";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }



        [HttpGet]
        [Route("DatosNominaEmpleadoReporte")]
        public async Task<IActionResult> DatosNominaEmpleadoReporte(Reporte report)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@FechaSeleccionada", report.FechaSeleccionada);
                parametros.Add("@EMPLEADO_ID", report.EMPLEADO_ID);
                var request = (await contexto.QueryAsync("DatosNominaEmpleadoReporte", parametros,
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
                    respuesta.MENSAJE = "No se encuentran los datos del reporte Nomina";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

        [HttpGet]
        [Route("DatosNominaGeneralReporte")]
        public async Task<IActionResult> DatosNominaGeneralReporte(string FechaSeleccionada)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@FechaSeleccionada", FechaSeleccionada);
                var request = (await contexto.QueryAsync("DatosNominaGeneralReporte", parametros,
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
                    respuesta.MENSAJE = "No se encuentran los datos del reporte Nomina General";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

        [HttpGet]
        [Route("DatosNominaReporte")]
        public async Task<IActionResult> DatosNominaReporte()
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {

                var request = await contexto.QueryAsync<SelectListItem>("DatosNominaReporte", new { },
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
                    respuesta.MENSAJE = "No se encuentran los datos";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

        [HttpGet]
        [Route("EmpleadosReporte")]
        public async Task<IActionResult> EmpleadosReporte()
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var request = await contexto.QueryAsync<SelectListItem>("EmpleadosReporte", new { },
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
                    respuesta.MENSAJE = "No se encuentran los datos del reporte Nomina Empleados";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

        [HttpGet]
        [Route("NominaGeneralReporte")]
        public async Task<IActionResult> NominaGeneralReporte(string FechaSeleccionada)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@FechaSeleccionada", FechaSeleccionada);
                var request = await contexto.QueryAsync("NominaGeneralReporte", parametros,
                    commandType: System.Data.CommandType.StoredProcedure);
                if (request != null)
                {
                    respuesta.CODIGO = 1;
                    respuesta.MENSAJE = "OK";
                    respuesta.CONTENIDO = request.ToList();
                    return Ok(respuesta);
                }
                else
                {
                    respuesta.CODIGO = 0;
                    respuesta.MENSAJE = "No se encuentran los datos del reporte Nomina General";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }
        [HttpGet]
        [Route("ObtenerSolicitudEmpleadoPeriodoReporte")]
        public async Task<IActionResult> ObtenerSolicitudEmpleadoPeriodoReporte(Reporte report)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@EMPLEADO_ID", report.EMPLEADO_ID);
                parametros.Add("@FECHA_INICIO", report.FECHA_INICIO);
                parametros.Add("@FECHA_FINAL", report.FECHA_FINAL);
                var request = (await contexto.QueryAsync("ObtenerSolicitudEmpleadoPeriodoReporte", parametros,
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
                    respuesta.MENSAJE = "No se encuentran los datos del reporte Nomina General";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }
        [HttpGet]
        [Route("ConsultarNombreEmpleado")]
        public async Task<IActionResult> ConsultarNombreEmpleado(long IdEmpleado)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@ID_EMPLEADO", IdEmpleado);
                var request = (await contexto.QueryAsync("ConsultarNombreEmpleado", parametros,
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
                    respuesta.MENSAJE = "No se encuentran los datos del reporte Nomina General";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }
    }
}
