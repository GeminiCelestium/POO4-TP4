using ModernRecrut.Documents.API.Models;

namespace ModernRecrut.Documents.API.Interfaces
{
    public interface IGestionFichiers
    {
        public Task<string> EnregistrerFichier(Fichier fichier);

        public List<string> ObtenirNomFichiersSelonId(string id);
    }
}
