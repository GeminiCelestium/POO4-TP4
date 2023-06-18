using ModernRecrut.MVC.Models;

namespace ModernRecrut.MVC.Interfaces
{
    public interface IGestionDocumentsService
    {
        Task<HttpResponseMessage> Ajouter(Fichier document);

        Task<List<string>> ObtenirTout(string id);

        //public string ObtenirAddresseAPI();

    }
}
