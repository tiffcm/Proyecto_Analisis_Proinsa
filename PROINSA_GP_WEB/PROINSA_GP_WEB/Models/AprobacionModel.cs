
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Servicios;
using System.Data;
using System.Text.Json;

namespace PROINSA_GP_WEB.Models
{
    public class AprobacionModel(HttpClient _httpClient, IConfiguration iConfiguration) : IAprobacionModel
    {
        public DataTable ObtenerSolicitudesEmpleado(int idEmpleado)
        {
            DataTable dataTable = new DataTable();

            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Aprobacion/ObtenerAprobacionPendiente?id_empleado=" + idEmpleado;
            var response = _httpClient.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                var respuesta = response.Content.ReadFromJsonAsync<Respuesta>().Result;
                if (respuesta!.CODIGO == 1)
                {
                    var jsonElement = (JsonElement)respuesta.CONTENIDO!;
                    var aprobaciones = JsonSerializer.Deserialize<List<Aprobacion>>(jsonElement.GetRawText());

                    if (aprobaciones != null)
                    {
                        dataTable.Columns.Add("RESPUESTASOLICITUD", typeof(string));
                        dataTable.Columns.Add("FECHA_SOLICITUD", typeof(string));
                        dataTable.Columns.Add("IDENTIFICACION", typeof(string));
                        dataTable.Columns.Add("ID_SOLICITUD", typeof(string));                       
                        dataTable.Columns.Add("SOLICITANTE", typeof(string));                        
                        dataTable.Columns.Add("TIPOPERMISO", typeof(string));
                        
                        foreach (var aprobacion in aprobaciones)
                        {
                            DataRow row = dataTable.NewRow();
                            row["RESPUESTASOLICITUD"] = aprobacion.RESPUESTASOLICITUD;
                            row["FECHA_SOLICITUD"] = aprobacion.FECHA_SOLICITUD;
                            row["IDENTIFICACION"] = aprobacion.IDENTIFICACION;
                            row["ID_SOLICITUD"] = aprobacion.ID_SOLICITUD;
                            row["SOLICITANTE"] = aprobacion.SOLICITANTE;
                            row["TIPOPERMISO"] = aprobacion.TIPOPERMISO;
                            dataTable.Rows.Add(row);
                        }
                    }
                }
            }
            return dataTable;
        }
    }
}
