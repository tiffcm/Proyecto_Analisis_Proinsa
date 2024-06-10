using Azure;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using PROINSA_GP_API.DbConnection;
using PROINSA_GP_API.Entidad;

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
        public async Task<IActionResult> ActualizarDatosUsuario ( Empleado entidad )
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
        public async Task<IActionResult> ConsultarEmpleado (long ID_EMPLEADO)
        {
            EmpleadoRespuesta respuesta = new EmpleadoRespuesta();

            try
            {
                using (var contexto = _dbConnection.CreateConnection())
                {
                    var request = (await contexto.QueryAsync("ConsultarEmpleado",
                       new { ID_EMPLEADO },
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
    }
}
