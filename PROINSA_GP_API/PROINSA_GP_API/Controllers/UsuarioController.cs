using Azure;
using Azure.Core;
using Dapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using PROINSA_GP_API.DbConnection;
using PROINSA_GP_API.Entidad;
using System.Reflection;

namespace PROINSA_GP_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IDbConnection _dbConnection;

        public UsuarioController(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }
        //Hay arreglarlo, no está funcional
        [HttpPut][Route("ActualizarDatosUsuario")]
        public async Task<IActionResult> ActualizarDatosUsuario(Empleado entidad)
        {
            Respuesta respuesta = new Respuesta();

            using (var contexto = _dbConnection.CreateConnection())
            {
                var request = await contexto.ExecuteAsync("ActualizarDatosUsuario",
                   new { entidad.IDENTIFICACION, entidad.NOMBRECOMPLETO },
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
                    respuesta.MENSAJE = "La información del usuario ya se encuentra registrada";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

        /**
         * Esta acción permite consultar un empleado por su ID
         * En este caso, respuesta.Dato se usa para almacenar la información que devuelve 
         * la Base de dato. Es necesario que en Entidad --> Empleado, el campo de Dato esté
         * como tipo Objet y en el proyecto Web esté como Empleado?
         * 
         **/
        [HttpGet][Route("ConsultarEmpleado")]
        public async Task<IActionResult> ConsultarEmpleado(string correo)
        {
            EmpleadoRespuesta respuesta = new EmpleadoRespuesta();

            try
            {
                using (var contexto = _dbConnection.CreateConnection())
                {
                    var request = (await contexto.QueryAsync("ConsultarEmpleado",
                       new { correo },
                       commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();
                    if (request != null)
                    {
                        respuesta.CODIGO = "00";
                        respuesta.MENSAJE = "OK";
                        respuesta.DATO = request;
                    }
                    else
                    {
                        respuesta.CODIGO = "0";
                        respuesta.MENSAJE = "La información del empleado no se encuentra registrada";
                    }
                }
            }
            catch (Exception)
            {
                respuesta.CODIGO = "0";
                respuesta.MENSAJE = "Error";
            }
            return Ok(respuesta);
        }

        [HttpGet]
        [Route("ObtenerDatosEmpleado")]
        public async Task<IActionResult> ObtenerDatosEmpleado(string CORREO)
        {
            EmpleadoRespuesta respuesta = new EmpleadoRespuesta();

            try
            {
                using (var contexto = _dbConnection.CreateConnection())
                {
                    var request = (await contexto.QueryAsync("ObtenerDatosEmpleado",
                       new { CORREO },
                       commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();
                    if (request != null)
                    {
                        respuesta.CODIGO = "00";
                        respuesta.MENSAJE = "OK";
                        respuesta.DATO = request;
                    }
                    else
                    {
                        respuesta.CODIGO = "0";
                        respuesta.MENSAJE = "La información del empleado no se encuentra registrada";
                    }
                }
            }
            catch (Exception)
            {
                respuesta.CODIGO = "0";
                respuesta.MENSAJE = "Error";
            }
            return Ok(respuesta);
        }
        
        // Verificar si es get o post
        [HttpGet]
        [Route("ConsultarUsuarioAdministrador")]
        public async Task<IActionResult> ConsultarUsuarioAdministrador(long ID_EMPLEADO)

        {
            EmpleadoRespuesta respuesta = new EmpleadoRespuesta();

            using (var contexto = _dbConnection.CreateConnection())
            {
                var empleado = (await contexto.QueryAsync("ConsultarUsuarioAdministrador", new { ID_EMPLEADO }, commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();

                if (empleado != null)
                {
                    respuesta.CODIGO = "00";
                    respuesta.MENSAJE = "OK";
                    respuesta.DATO = empleado;
                    return Ok(empleado); // nose puede hacer un respuesta.DATO
                }
                else
                {
                    respuesta.CODIGO = "0";
                    respuesta.MENSAJE = "La información del empleado no se encuentra registrada";
                    return Ok(empleado);
                }

                // return Ok(empleado);

            }
        }

        [HttpPut]
        [Route("ActualizarUsuarioAdministrador")]
        public async Task<IActionResult> ActualizarUsuarioAdministrador(DatosUsuario AcU, IFormFile FOTO)

        {
            Respuesta respuesta = new Respuesta();

            using (var contexto = _dbConnection.CreateConnection())
            {

                if (FOTO != null && FOTO.Length > 0)
                {
                    // No permitir imagenes de producto con mas de 1MB
                    if (FOTO.Length > 1048576)
                        return BadRequest();

                    using (var memoryStream = new MemoryStream())
                    {
                        await FOTO.CopyToAsync(memoryStream);

                        string mimeType = FOTO.ContentType;
                        // Crea el texto Base64 incluyendo el tipo de dato base 64 (MIME)
                        string base64String = $"data:{mimeType};base64," + Convert.ToBase64String(memoryStream.ToArray());
                        AcU.FOTO = base64String;
                    }
                }

                var request = await contexto.ExecuteAsync("ActualizarUsuarioAdministrador",
                   new { AcU.IDENTIFICACION, AcU.NOMBRECOMPLETO, AcU.CORREO, AcU.TEMPORAL, AcU.DIRECCION, AcU.TELEFONO, AcU.SALARIO, AcU.FOTO, AcU.NOMBRE_CARGO, AcU.NOMBRE_DEPARTAMENTO, AcU.ESTADO, AcU.NOMBRE_ROL, AcU.NOMBREHL },
                   commandType: System.Data.CommandType.StoredProcedure);
                if (request > 0)
                {
                    respuesta.CODIGO = 1;
                    respuesta.MENSAJE = "OK";
                    respuesta.CONTENIDO = true;
                    return Ok(request);
                }
                else
                {
                    respuesta.CODIGO = 0;
                    respuesta.MENSAJE = "La información del usuario ya se encuentra registrada";
                    respuesta.CONTENIDO = false;
                    return Ok(request);
                }
            }

        }
    }
}
