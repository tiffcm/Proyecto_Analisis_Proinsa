﻿using PROINSA_GP_WEB.Entidad;

namespace PROINSA_GP_WEB.Servicios
{
    /// <summary>
    /// Es la interfaz del modelo de Usuario que se expone a la red.
    /// </summary>
    public interface IUsuarioModel
    {
        Respuesta? ConsultarDatosEmpleado(string correo);
    }
}