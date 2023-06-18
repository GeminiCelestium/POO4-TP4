using Microsoft.AspNetCore.Mvc;
using ModernRecrut.Favoris.API.Models;

namespace ModernRecrut.Favoris.API.Interfaces
{
    public interface ICacheFavorisService
    {
        List<OffreEmploi> ObtenirFavoris();
        void AjouterFavoris(OffreEmploi offreEmploi);
        void SupprimerFavoris(int id);
        
    }
}
