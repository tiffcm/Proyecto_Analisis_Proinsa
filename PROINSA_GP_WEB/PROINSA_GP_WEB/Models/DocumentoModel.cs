using Microsoft.Extensions.Configuration;
using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Servicios;
using System.Data;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace PROINSA_GP_WEB.Models
{
    public class DocumentoModel(HttpClient _httpClient, IConfiguration iConfiguration) : IDocumentoModel
    {

        public Respuesta? RegistrarDocumento(Documento entidad)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Documentos/RegistrarDocumento";
            JsonContent body = JsonContent.Create(entidad);
            var solicitud = _httpClient.PostAsync(url, body).Result;
            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }

        public Respuesta ConsultarTiposDocumento()
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Documentos/ConsultarTiposDocumento";                        
            var result = _httpClient.GetAsync(url).Result;
            if (result.IsSuccessStatusCode)
                return result.Content.ReadFromJsonAsync<Respuesta>().Result!;
            else
                return new Respuesta();
        }

        public DataTable ConsultarDocumentosEmpleado(int EMPLEADO_ID)
        {
            DataTable dataTable = new DataTable();

            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Documentos/ConsultarDocumentosEmpleado?EMPLEADO_ID=" + EMPLEADO_ID;
            var response = _httpClient.GetAsync(url).Result;

            if (response.IsSuccessStatusCode)
            {
                var respuesta = response.Content.ReadFromJsonAsync<Respuesta>().Result;
                if (respuesta!.CODIGO == 1)
                {
                    var jsonElement = (JsonElement)respuesta.CONTENIDO!;
                    var documentos = JsonSerializer.Deserialize<List<Documento>>(jsonElement.GetRawText());

                    if (documentos != null)
                    {
                        dataTable.Columns.Add("DESCRIPCION", typeof(string));
                        dataTable.Columns.Add("NOMBRE_DOCUMENTO", typeof(string));
                        dataTable.Columns.Add("COMENTARIO", typeof(string));
                        dataTable.Columns.Add("DOCUMENTO", typeof(string));

                        foreach (var documento in documentos)
                        {
                            string base64 = Convert.ToBase64String(documento.DOCUMENTO!);                            
                            documento.VER_DOCUMENTO = $"data:application/pdf;base64,{base64}";
                            DataRow row = dataTable.NewRow();
                            row["DESCRIPCION"] = documento.DESCRIPCION;
                            row["NOMBRE_DOCUMENTO"] = documento.NOMBRE_DOCUMENTO;
                            row["COMENTARIO"] = documento.COMENTARIO;
                            row["DOCUMENTO"] = documento.VER_DOCUMENTO;
                            dataTable.Rows.Add(row);
                        }
                    }
                }
            }
            return dataTable;
        }
    }
}
