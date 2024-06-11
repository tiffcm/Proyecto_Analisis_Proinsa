﻿using Newtonsoft.Json;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Servicios;
using System.Net.Http;
using System.Text;

namespace PROINSA_GP_WEB.Models
{
    public class EmpleadoModel (HttpClient _httpClient) : IEmpleadoModel
    {
        /**
         * Consulta de tipo Get para enviar un ID por la ruta que se comunica con el API
         * Dependiendo de la respuesta que se obtenga se lee el archivo JSON que retorna el 
         * API
         **/
        public EmpleadoRespuesta? ConsultarEmpleado(long ID_EMPLEADO)
        {            
            string url = "https://localhost:7220/api/Usuario/ConsultarEmpleado?ID_EMPLEADO="+ ID_EMPLEADO;           
            var solicitud = _httpClient.GetAsync(url).Result;

            if (solicitud.IsSuccessStatusCode)
            {
                return solicitud.Content.ReadFromJsonAsync<EmpleadoRespuesta>().Result;
            }
            else
            {
                return null;
            }
        }

        public EmpleadoRespuesta? ObtenerDatosEmpleado(string CORREO)
        {
            string url = "https://localhost:7220/api/Usuario/ObtenerDatosEmpleado?CORREO=" + CORREO;
            var solicitud = _httpClient.GetAsync(url).Result;

            if (solicitud.IsSuccessStatusCode)
            {
                return solicitud.Content.ReadFromJsonAsync<EmpleadoRespuesta>().Result;
            }
            else
            {
                return null;
            }
        }
    }
}
