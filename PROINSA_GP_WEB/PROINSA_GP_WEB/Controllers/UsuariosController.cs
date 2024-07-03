using Microsoft.AspNetCore.Mvc;
using P_WebMartes.Models;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Models;
using PROINSA_GP_WEB.Servicios;
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
    public class UsuariosController (IUsuarioModel _iUsuarioModel) : Controller 
    {
        /// <summary>
        /// Permite al usuario acceder a su información.
        /// </summary>
        /// <returns></returns>
        [Seguridad][HttpGet]
        public IActionResult MiCuenta()
        {
            var correoEmpleado = User.Identity?.Name;
            if (correoEmpleado != null)
            {
                var respuesta = _iUsuarioModel.ConsultarDatosEmpleado(correoEmpleado);
                if (respuesta!.CODIGO == 1)
                {
                    var usuario = JsonSerializer.Deserialize<Usuario>((JsonElement)respuesta.CONTENIDO!);
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
            }
            else
            {
                return RedirectToAction("IniciarSesion", "Inicio");
            }
            return View();
        }

        /// <summary>
        /// Permite al usuario actualizar la información que es permitida según restricciones
        /// </summary>
        /// <param name="entidad">Información del usuario como un objeto</param>
        /// <returns>Página principal en caso de éxito</returns>
        [Seguridad][HttpPost]
        public IActionResult MiCuenta(Usuario entidad)
        {
            var respuesta = _iUsuarioModel.ActualizarDatosUsuario(entidad);
            return RedirectToAction("Principal","Home");
        }

        /// <summary>
        /// Permite al administrador acceder a la lista de empleados.
        /// </summary>
        /// <returns></returns>
        //[Administrador]
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

        [Administrador]
        [Seguridad]
        [HttpPost]
        public IActionResult CambiarEstadoUsuario(long IdEmpleado)
        {
            var Respuesta = _iUsuarioModel.CambiarEstadoUsuarioAdmin(IdEmpleado);
            return RedirectToAction("AdministrarUsuarios", "Usuarios");

        }

        [Administrador][Seguridad][HttpGet]
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

        [Administrador]
        [Seguridad]
        [HttpPost]
        public IActionResult MantenimientoUsuario(Usuario usuario)
        {
			var respuesta = _iUsuarioModel.EditarDatosVistaAdmin(usuario);
			return RedirectToAction("AdministrarUsuarios", "Usuarios");

        }

			/// <summary>
			/// 
			/// </summary>
			/// <returns></returns>

			[Administrador][Seguridad][HttpPost]
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
        [HttpPost]
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
        [HttpPost]
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
        [HttpPost]
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
        

    }
}
