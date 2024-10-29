using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using P_WebMartes.Models;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Models;
using PROINSA_GP_WEB.Servicios;
using System.Buffers.Text;
using System.Text.Json;

namespace PROINSA_GP_WEB.Controllers
{
    /// <summary>
    /// Maneja la información del empleado y el mantenimiento de usuarios
    /// </summary>
    /// <param name="_iUsuarioModel">s para manejar interfaces en lugar del modelo directo</param>
    /// <version>1.3</version>
    /// <author>Tiffany Camacho Monge, Brandon Ruiz Miranda</author>
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    [Authorize]
    public class UsuariosController(IUsuarioModel _iUsuarioModel) : Controller
    {
        /// <summary>
        /// Permite al usuario acceder a su información.
        /// </summary>
        /// <returns></returns>
        [Seguridad]
        [HttpGet]
        public IActionResult MiCuenta()
        {
            var correoEmpleado = User.Identity?.Name;

            var respuesta = _iUsuarioModel.ConsultarDatosEmpleado(correoEmpleado);
            if (respuesta!.CODIGO == 1)
            {
                var usuario = JsonSerializer.Deserialize<Usuario>((JsonElement)respuesta.CONTENIDO!);
                string base64 = Convert.ToBase64String(usuario!.FOTO!);
                string extension = usuario.TIPO_FOTO ?? "image/png";
                usuario.FOTO_VISTA = $"data:{extension};base64,{base64}";
                if (usuario != null)
                {
                    long? IdEmpleado = HttpContext.Session.GetInt32("ID_EMPLEADO");
                    var telefonosRespuesta = _iUsuarioModel.ObtenerTelefonosUsuario(IdEmpleado);
                    if (telefonosRespuesta != null)
                    {
                        var telefonos = JsonSerializer.Deserialize<List<Telefono>>((JsonElement)telefonosRespuesta.CONTENIDO!);
                        usuario.TELEFONOS = telefonos;
                        return View(usuario);
                    }
                }
            }

            return View();
        }

        /// <summary>
        /// Permite al usuario actualizar la información que es permitida según restricciones
        /// </summary>
        /// <param name="entidad">Información del usuario como un objeto</param>
        /// <returns>Página principal en caso de éxito</returns>
        [Seguridad]
        [HttpPost]
        public IActionResult MiCuenta(Usuario entidad)
        {
            IFormFile archivo = Request.Form.Files["upload"]!;
            if (archivo != null && archivo.Length > 0)
            {
                var extension = archivo.ContentType;
                entidad.FOTO = ConvertirIMGBytes(archivo);
                entidad.TIPO_FOTO = extension;
                string base64 = Convert.ToBase64String(entidad!.FOTO!);
                string tipoarchivo = entidad.TIPO_FOTO ?? "image/png";
                entidad.FOTO_VISTA = $"data:{tipoarchivo};base64,{base64}";
                HttpContext.Session.SetString("FOTO", entidad!.FOTO_VISTA!);
                HttpContext.Session.SetString("EXTENSION", tipoarchivo);
                HttpContext.Session.Set("BLOB", entidad!.FOTO);
            }
            else
            {
                entidad.FOTO = HttpContext.Session.Get("BLOB");
                entidad.FOTO_VISTA = HttpContext.Session.GetString("FOTO");
                entidad.TIPO_FOTO = HttpContext.Session.GetString("EXTENSION");
            }
            var respuesta = _iUsuarioModel.ActualizarDatosUsuario(entidad);
            return RedirectToAction("Principal", "Home");
        }

        /// <summary>
        /// Permite al administrador acceder a la lista de empleados.
        /// </summary>
        /// <returns></returns>
        [Administrador]
        [Seguridad]
        [HttpGet]
        public IActionResult AdministrarUsuarios()
        {
            var respuesta = _iUsuarioModel.MostrarInfoVistaAdmin();
            if (respuesta!.CODIGO == 1)
            {
                var datos = JsonSerializer.Deserialize<List<Usuario>>((JsonElement)respuesta.CONTENIDO!);

                return View(datos);


            }
            return View(new List<Usuario>());
        }
        /// <summary>
        /// Permite al administrador activar/desactivar a los usuarios por medio del parametro "IdEmpleado"
        /// </summary>
        /// <returns></returns>
        [Administrador]
        [Seguridad]
        [HttpPost]
        public IActionResult CambiarEstadoUsuario(long IdEmpleado)
        {
            var Respuesta = _iUsuarioModel.CambiarEstadoUsuarioAdmin(IdEmpleado);
            return RedirectToAction("AdministrarUsuarios", "Usuarios");

        }

        /// <summary>
        /// Permite al administrador acceder a la informacion del empleado.
        /// </summary>
        /// <returns></returns>
        [Administrador]
        [Seguridad]
        [HttpGet]
        public IActionResult MantenimientoUsuario(long IDEmpleado)
        {
            MantenimientoUsuarioListaCargos();
            MantenimientoUsuarioListaHorarios();
            MantenimientoUsuarioListaRoles();
            MantenimientoUsuarioListaDepartamentos();
            if (IDEmpleado != null)
            {
                var respuesta = _iUsuarioModel.MostrarEmpleadoVistaAdmin(IDEmpleado);
                if (respuesta!.CODIGO == 1)
                {
                    var usuario = JsonSerializer.Deserialize<Usuario>((JsonElement)respuesta.CONTENIDO!);

                    if (usuario != null)
                    {
                        var telefonosRespuesta = _iUsuarioModel.ObtenerTelefonosUsuario(IDEmpleado);
                        if (telefonosRespuesta != null)
                        {
                            var telefonos = JsonSerializer.Deserialize<List<Telefono>>((JsonElement)telefonosRespuesta.CONTENIDO!);
                            usuario.TELEFONOS = telefonos;
                            return View(usuario);
                        }
                    }
                }
            }
            return NotFound();
        }

        /// <summary>
        /// Permite al administrador actualizar la informacion del empleado.
        /// </summary>
        /// <returns></returns>
        [Administrador]
        [Seguridad]
        [HttpPost]
        public IActionResult MantenimientoUsuario(Usuario usuario)
        {
            var respuesta = _iUsuarioModel.EditarDatosVistaAdmin(usuario);
            return RedirectToAction("AdministrarUsuarios", "Usuarios");
        }

        [Administrador]
        [Seguridad]
        [HttpPost]
        public IActionResult CambiarEstadoUsuarioVista(long IdEmpleado)
        {
            var respuesta = _iUsuarioModel.CambiarEstadoUsuarioAdmin(IdEmpleado);

            if (respuesta.CODIGO == 1)
            {
                return RedirectToAction("MantenimientoUsuario", new { IdEmpleado });
            }

            ViewBag.msj = respuesta.MENSAJE;
            return RedirectToAction("MantenimientoUsuario", new { IdEmpleado });

        }


        /// <summary>
        /// Las siguientes 4 funciones son usadas para enlistar las opciones a seleccionar durante la actualizacion del usuario llamados en MantenimientoUsuario()
        /// </summary>
        /// <returns></returns>
        [Administrador]
        [Seguridad]
        public IActionResult MantenimientoUsuarioListaCargos()
        {

            var cargoList = _iUsuarioModel.MostrarTodosCargos();
            ViewBag.CargoList = cargoList!;

            var viewModel = new Cargo
            {
                ID_CARGO = 1,

            };

            return View(viewModel);
        }

        [Administrador]
        [Seguridad]
        public IActionResult MantenimientoUsuarioListaHorarios()
        {

            var horarioList = _iUsuarioModel.MostrarTodosHorarios();
            ViewBag.HorarioList = horarioList!;

            var viewModel = new Horario
            {
                ID_HORARIOLABORAL = 1,

            };

            return View(viewModel);
        }


        [Administrador]
        [Seguridad]
        public IActionResult MantenimientoUsuarioListaRoles()
        {

            var rolesList = _iUsuarioModel.MostrarTodosRoles();
            ViewBag.RolesList = rolesList!;

            var viewModel = new Rol
            {

                IDROL = "1",

            };

            return View(viewModel);
        }

        [Administrador]
        [Seguridad]
        public IActionResult MantenimientoUsuarioListaDepartamentos()
        {

            var departamentoList = _iUsuarioModel.MostrarTodosDepartamentos();
            ViewBag.DepartamentoList = departamentoList!;

            var viewModel = new Departamento
            {
                ID_DEPARTAMENTO = 1,

            };

            return View(viewModel);
        }

        private byte[] ConvertirIMGBytes(IFormFile foto)
        {
            using (var ms = new MemoryStream())
            {
                foto.CopyTo(ms);
                return ms.ToArray();
            }
        }
    }
}
