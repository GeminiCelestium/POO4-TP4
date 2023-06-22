using ModernRecrut.MVC.Models;

namespace ModernRecrut.MVC.Interfaces
{
    public interface IGestionPostulationsService
    {
        Task<List<Postulation>> ObtenirTout();

        Task<Postulation> Obtenir(int id);

        Task<HttpResponseMessage> Creer(Postulation postulation);

        Task Modifier(Postulation postulation);

        Task Supprimer(int id);
    }
}
