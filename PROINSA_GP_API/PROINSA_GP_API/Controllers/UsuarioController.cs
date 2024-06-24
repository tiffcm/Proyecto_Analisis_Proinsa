using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using PROINSA_GP_API.Entidad;

namespace PROINSA_GP_API.Controllers
{
    /// <summary>
    /// Este controlador se encargará de administrar las solicitudes de los usuarios.
    /// </summary>
    /// <param name="iConfiguration">Inyección de dependencia para manejo cadenas de conexión</param>
    /// <author>Brandon Ruiz Miranda</author>
    /// <version>1.6</version>
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController (IConfiguration iConfiguration) : ControllerBase
    {
        /// <summary>
        /// Esta acción se encargará de pasarle a la base de datos el correo para poder hacer la consulta
        /// del rol y de los datos que el usuario necesita para el apartado de "Mi cuenta".
        /// </summary>        
        /// <param name="correo">Correo del usuario recuperado con Identity</param>
        /// <returns>Devuele la respuesta de la acción</returns>
        [HttpGet][Route("ConsultarDatosEmpleado")]
        public async Task<IActionResult> ConsultarDatosEmpleado(string correo)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var request = (await contexto.QueryAsync("ConsultarDatosEmpleado",
                    new { correo },
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
                    respuesta.MENSAJE = "La información del empleado no se encuentra registrada";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }                
            }
        }

        /// <summary>
        /// Se encarga de enviar la información que se quiere actualizar en un paquete.
        /// </summary>
        /// <param name="entidad">Datos del usuario enviados como un objeto</param>
        /// <returns>Confirmación de éxito o no en la actualización de la información</returns>
        [HttpPut][Route("ActualizarDatosUsuario")]
        public async Task<IActionResult> ActualizarDatosUsuario(Usuario usuario)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@ID_EMPLEADO", usuario.ID_EMPLEADO);
                parametros.Add("@DIRRECCION", usuario.DIRRECION);
                if (usuario.TELEFONOS != null && usuario.TELEFONOS.Any())
                {
                    if (usuario.TELEFONOS.Count == 1)
                    {
                        parametros.Add($"@ID_TELEFONO{1}", usuario.TELEFONOS[0].ID_TELEFONO);
                        parametros.Add($"@TELEFONO{1}", usuario.TELEFONOS[0].TELEFONO);
                        parametros.Add($"@ID_TELEFONO{2}", usuario.TELEFONOS[0].ID_TELEFONO);
                        parametros.Add($"@TELEFONO{2}", usuario.TELEFONOS[0].TELEFONO);
                    }
                    else
                    {
                        for (int i = 0; i < usuario.TELEFONOS.Count; i++)
                        {
                            parametros.Add($"@ID_TELEFONO{i + 1}", usuario.TELEFONOS[i].ID_TELEFONO);
                            parametros.Add($"@TELEFONO{i + 1}", usuario.TELEFONOS[i].TELEFONO);
                        }
                    }

                }

                var request = await contexto.ExecuteAsync("ActualizarDatosUsuario", parametros,
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
                    respuesta.MENSAJE = "Se presentó un inconveniente actualizando su información";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

        /// <summary>
        /// Esta acción se encarga de traer todos los teléfonos que tiene registrados un empleado
        /// utilizando para ello el Id.
        /// </summary>        
        /// <param name="idEmpleado">Id  del usuario recuperado de otro SP</param>
        /// <returns>Devuele la respuesta de la acción</returns>
        [HttpGet]
        [Route("ObtenerTelefonosUsuario")]
        public async Task<IActionResult> ObtenerTelefonosUsuario(long idEmpleado)
        {
            Respuesta respuesta = new Respuesta();
            using (var contexto = new SqlConnection(iConfiguration.GetSection("ConnectionStrings:Db_Connection").Value))
            {
                var parametros = new DynamicParameters();
                parametros.Add("@ID_EMPLEADO", idEmpleado);
                var request = await contexto.QueryAsync<Telefono>("ObtenerTelefonosUsuario", parametros,
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
                    respuesta.MENSAJE = "La información del empleado no se encuentra registrada";
                    respuesta.CONTENIDO = false;
                    return Ok(respuesta);
                }
            }
        }

        //------------------------------------------------------------------



        //// Verificar si es get o post
        //[HttpGet]
        //[Route("ConsultarUsuarioAdministrador")]
        //public async Task<IActionResult> ConsultarUsuarioAdministrador(long ID_EMPLEADO)

        //{
        //    Respuesta respuesta = new Respuesta();

        //    using (var contexto = _dbConnection.CreateConnection())
        //    {
        //        var empleado = (await contexto.QueryAsync("ConsultarUsuarioAdministrador", new { ID_EMPLEADO }, commandType: System.Data.CommandType.StoredProcedure)).FirstOrDefault();

        //        if (empleado != null)
        //        {
        //            respuesta.CODIGO = "00";
        //            respuesta.MENSAJE = "OK";
        //            respuesta.DATO = empleado;
        //            return Ok(empleado); // nose puede hacer un respuesta.DATO
        //        }
        //        else
        //        {
        //            respuesta.CODIGO = "0";
        //            respuesta.MENSAJE = "La información del empleado no se encuentra registrada";
        //            return Ok(empleado);
        //        }

        //        // return Ok(empleado);

        //    }
        //}

        //[HttpPut]
        //[Route("ActualizarUsuarioAdministrador")]
        //public async Task<IActionResult> ActualizarUsuarioAdministrador(DatosUsuario AcU, IFormFile FOTO)

        //{
        //    Respuesta respuesta = new Respuesta();

        //    using (var contexto = _dbConnection.CreateConnection())
        //    {

        //        if (FOTO != null && FOTO.Length > 0)
        //        {
        //            // No permitir imagenes de producto con mas de 1MB
        //            if (FOTO.Length > 1048576)
        //                return BadRequest();

        //            using (var memoryStream = new MemoryStream())
        //            {
        //                await FOTO.CopyToAsync(memoryStream);

        //                string mimeType = FOTO.ContentType;
        //                // Crea el texto Base64 incluyendo el tipo de dato base 64 (MIME)
        //                string base64String = $"data:{mimeType};base64," + Convert.ToBase64String(memoryStream.ToArray());
        //                AcU.FOTO = base64String;
        //            }
        //        }

        //        var request = await contexto.ExecuteAsync("ActualizarUsuarioAdministrador",
        //           new { AcU.IDENTIFICACION, AcU.NOMBRECOMPLETO, AcU.CORREO, AcU.TEMPORAL, AcU.DIRECCION, AcU.TELEFONO, AcU.SALARIO, AcU.FOTO, AcU.NOMBRE_CARGO, AcU.NOMBRE_DEPARTAMENTO, AcU.ESTADO, AcU.NOMBRE_ROL, AcU.NOMBREHL },
        //           commandType: System.Data.CommandType.StoredProcedure);
        //        if (request > 0)
        //        {
        //            respuesta.CODIGO = 1;
        //            respuesta.MENSAJE = "OK";
        //            respuesta.CONTENIDO = true;
        //            return Ok(request);
        //        }
        //        else
        //        {
        //            respuesta.CODIGO = 0;
        //            respuesta.MENSAJE = "La información del usuario ya se encuentra registrada";
        //            respuesta.CONTENIDO = false;
        //            return Ok(request);
        //        }
        //    }

        //}
    }
}
