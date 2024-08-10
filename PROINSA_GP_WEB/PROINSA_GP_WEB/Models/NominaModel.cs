using PROINSA_GP_WEB.Entidad;
using PROINSA_GP_WEB.Servicios;

namespace PROINSA_GP_WEB.Models
{
    public class NominaModel (HttpClient _httpClient, IConfiguration iConfiguration) : INominaModel
    {
        public Respuesta? RegistrarNomina(Nomina entidad)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Nomina/RegistrarNomina";
            JsonContent body = JsonContent.Create(entidad);
            var solicitud = _httpClient.PostAsync(url, body).Result;
            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }

        //Este hay que revisarlo
        public Respuesta? CalculoNominaInicial(Nomina entidad)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Nomina/CalculoNominaInicial";
            JsonContent body = JsonContent.Create(entidad);
            var solicitud = _httpClient.PostAsync(url, body).Result;
            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }

        //Este hay que revisarlo
        public Respuesta? CalculoNominaFinal(Nomina entidad)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Nomina/CalculoNominaFinal";
            JsonContent body = JsonContent.Create(entidad);
            var solicitud = _httpClient.PostAsync(url, body).Result;
            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }

        public Respuesta? ConsultarTiposNomina()
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Nomina/ConsultarTiposNomina";
            var solicitud = _httpClient.GetAsync(url).Result;

            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }

        //Este hay que revisarlo
        public Respuesta? RegistrarIngresosNominaDetalle(Nomina entidad)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Nomina/RegistrarIngresosNominaDetalle";
            JsonContent body = JsonContent.Create(entidad);
            var solicitud = _httpClient.PostAsync(url, body).Result;
            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }

        //Este hay que revisarlo
        public Respuesta? RegistrarDeduccionNominaDetalle(Nomina entidad)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Nomina/RegistrarDeduccionNominaDetalle";
            JsonContent body = JsonContent.Create(entidad);
            var solicitud = _httpClient.PostAsync(url, body).Result;
            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }

        public Respuesta? ObtenerNominaEmpleado(int EMPLEADO_ID)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Nomina/ObtenerNominaEmpleado";
            var solicitud = _httpClient.GetAsync(url).Result;

            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }

        public Respuesta? ObtenerNominaMensualEmpleados(DateTime fechapago)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Nomina/ObtenerNominaMensualEmpleados";
            var solicitud = _httpClient.GetAsync(url).Result;

            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }

        //En los PUT se tiene que mandar objetos
        public Respuesta? RevisionNomina(Nomina entidad)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Nomina/RevisionNomina";
            JsonContent body = JsonContent.Create(entidad);
            var solicitud = _httpClient.PutAsync(url, body).Result;
            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }
    }
}
