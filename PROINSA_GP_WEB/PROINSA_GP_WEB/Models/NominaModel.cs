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

        public Respuesta? CalculoNominaInicial(DateTime Fecha)
        {
            string fechaFormato = Fecha.ToString("yyyy-MM-ddTHH:mm:ss");
            string url = $"{iConfiguration.GetSection("Llaves:UrlApi").Value}Nomina/CalculoNominaInicial?Fecha={Uri.EscapeDataString(fechaFormato)}";
            var solicitud = _httpClient.GetAsync(url).Result;
            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }

        public Respuesta? CalculoNominaFinal(DateTime Fecha)
        {
            string fechaFormato = Fecha.ToString("yyyy-MM-ddTHH:mm:ss");
            string url = $"{iConfiguration.GetSection("Llaves:UrlApi").Value}Nomina/CalculoNominaFinal?Fecha={Uri.EscapeDataString(fechaFormato)}";
            var solicitud = _httpClient.GetAsync(url).Result;
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

        public Respuesta? ObtenerIngresos()
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Nomina/ObtenerIngresos";
            var solicitud = _httpClient.GetAsync(url).Result;

            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }

        public Respuesta? ObtenerDeducciones()
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Nomina/ObtenerDeducciones";
            var solicitud = _httpClient.GetAsync(url).Result;

            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }

        public Respuesta? RegistrarIngresosNominaDetalle(List<IngresoNominaDetalle> entidad)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Nomina/RegistrarIngresosNominaDetalle";
            JsonContent body = JsonContent.Create(entidad);
            var solicitud = _httpClient.PostAsync(url, body).Result;
            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }

        public Respuesta? RegistrarDeduccionNominaDetalle(List<DeduccionNominaDetalle> entidad)
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
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Nomina/ObtenerNominaEmpleado?EMPLEADO_ID=" + EMPLEADO_ID;
            var solicitud = _httpClient.GetAsync(url).Result;

            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }

        public Respuesta? ObtenerNominaMensualEmpleados(DateTime fechapago)
        {
            string fechaFormato = fechapago.ToString("yyyy-MM-ddTHH:mm:ss");
            string url = $"{iConfiguration.GetSection("Llaves:UrlApi").Value}Nomina/ObtenerNominaMensualEmpleados?fechapago={Uri.EscapeDataString(fechaFormato)}";
            var solicitud = _httpClient.GetAsync(url).Result;

            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }
       
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

        public Respuesta? AprobacionNomina(Nomina entidad)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Nomina/AprobacionNomina";
            JsonContent body = JsonContent.Create(entidad);
            var solicitud = _httpClient.PutAsync(url, body).Result;
            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }

        public Respuesta? ConsultarNombreEmpleado(long ID_EMPLEADO)
        {
            string url = iConfiguration.GetSection("Llaves:UrlApi").Value + "Usuario/ConsultarNombreEmpleado?ID_EMPLEADO=" + ID_EMPLEADO;
            var solicitud = _httpClient.GetAsync(url).Result;

            if (solicitud.IsSuccessStatusCode)
                return solicitud.Content.ReadFromJsonAsync<Respuesta>().Result;
            else
                return new Respuesta();
        }
    }
}
