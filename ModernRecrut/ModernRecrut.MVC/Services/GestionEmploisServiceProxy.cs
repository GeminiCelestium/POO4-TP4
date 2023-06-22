using ModernRecrut.MVC.Helpers;
using ModernRecrut.MVC.Interfaces;
using ModernRecrut.MVC.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace ModernRecrut.MVC.Services
{
    public class GestionEmploisServiceProxy : IGestionEmploisService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<GestionEmploisServiceProxy> _logger;
        private const string _ApiUrl = "api/OffreEmplois";

        public GestionEmploisServiceProxy(HttpClient httpClient, ILogger<GestionEmploisServiceProxy> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }

        public async Task<HttpResponseMessage> Ajouter(OffreEmploi offreEmploi)
        {
            StringContent content = new(JsonConvert.SerializeObject(offreEmploi), Encoding.UTF8, "application/json");

            var result = await _httpClient.PostAsync(_ApiUrl, content);

            LogHttpCode(result.StatusCode);

            return result;


        }

        public async Task<List<OffreEmploi>> ObtenirTout()
        {
            var result = await _httpClient.GetAsync(_ApiUrl);

            LogHttpCode(result.StatusCode);

            return await result.Content.ReadFromJsonAsync<List<OffreEmploi>>();

        }

        public async Task<OffreEmploi> Obtenir(int id)
        {
            var result = await _httpClient.GetAsync(_ApiUrl + "/" + id);

            LogHttpCode(result.StatusCode);

            return await result.Content.ReadFromJsonAsync<OffreEmploi>();
        }

        public async Task Supprimer(int id)
        {

            var result = await _httpClient.DeleteAsync(_ApiUrl + "/" + id);

            LogHttpCode(result.StatusCode);

        }

        public async Task Modifier(OffreEmploi offreEmploi)
        {
            StringContent content = new(JsonConvert.SerializeObject(offreEmploi), Encoding.UTF8, "application/json");


            var result = await _httpClient.PutAsync(_ApiUrl + "/" + offreEmploi.Id, content);

            LogHttpCode(result.StatusCode);
        }


        private void LogHttpCode(HttpStatusCode httpStatusCode)
        {
            int codeStatus = (int)httpStatusCode;


            if (codeStatus.ToString().StartsWith("2"))
            {
                _logger.LogInformation($"Le service a retourné un code HTTP de type succès avec pour valeur {httpStatusCode}");
            }
            else if (codeStatus.ToString().StartsWith("4"))
            {
                _logger.LogError($"Le service a retourné un code HTTP de type erreur avec pour valeur {httpStatusCode}");
            }
            else if (codeStatus.ToString().StartsWith("5"))
            {
                _logger.LogCritical($"Le service a retourné un code HTTP de type critique avec pour valeur {httpStatusCode}");

                throw new HttpRequestException("Erreur grave dans l’API");
            }
        }
    }
}
