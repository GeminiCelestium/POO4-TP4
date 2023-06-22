using ModernRecrut.MVC.Helpers;
using ModernRecrut.MVC.Interfaces;
using ModernRecrut.MVC.Models;
using Newtonsoft.Json;
using System.Net;
using System.Text;

namespace ModernRecrut.MVC.Services
{
    public class GestionPostulationsServiceProxy : IGestionPostulationsService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<GestionPostulationsServiceProxy> _logger;
        private const string _apiUrl = "api/Postulations";

        public GestionPostulationsServiceProxy(HttpClient httpClient, ILogger<GestionPostulationsServiceProxy> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
        }
        public async Task<List<Postulation>> ObtenirTout()
        {
            var resultat = await _httpClient.GetAsync(_apiUrl);

            LogHttpCode(resultat.StatusCode);

            return await resultat.Content.ReadFromJsonAsync<List<Postulation>>();
        }

        public async Task<Postulation> Obtenir(int id)
        {
            var result = await _httpClient.GetAsync(_apiUrl + "/" + id);

            LogHttpCode(result.StatusCode);

            return await result.Content.ReadFromJsonAsync<Postulation>();
        }

        public async Task<HttpResponseMessage> Creer(Postulation postulation)
        {
            StringContent content = new(JsonConvert.SerializeObject(postulation), Encoding.UTF8, "application/json");
            _logger.LogInformation(CustomLogEvents.Creation, $"Ajout d'une postulation {content}");
            
            var response = await _httpClient.PostAsync(_apiUrl, content);
            LogHttpCode(response.StatusCode);

            try
            {
                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(CustomLogEvents.Erreur, $"Erreur lors de l'ajout d'une postulation : {ex.Message}");
                return new HttpResponseMessage(HttpStatusCode.BadRequest);
            }

        }

        public async Task Modifier(Postulation postulation)
        {
            StringContent content = new(JsonConvert.SerializeObject(postulation), Encoding.UTF8, "application/json");

            var result = await _httpClient.PutAsync(_apiUrl + "/" + postulation.Id, content);

            LogHttpCode(result.StatusCode);
        }

        public async Task Supprimer(int id)
        {
            _logger.LogInformation(CustomLogEvents.Suppression, $"Suppression d'une postulation");

            try
            {
                await _httpClient.DeleteAsync(_apiUrl + "/" + id);
            }
            catch (Exception ex)
            {
                _logger.LogError(CustomLogEvents.Erreur, $"Erreur lors de la suppression d'une postulation : {ex.Message}");
            }

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