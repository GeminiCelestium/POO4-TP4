using Microsoft.Extensions.Caching.Memory;
using ModernRecrut.Favoris.API.Interfaces;
using ModernRecrut.Favoris.API.Models;
using ModernRecrut.Favoris.API.Helpers;

namespace ModernRecrut.Favoris.API.Services
{
    public class CacheFavorisService : ICacheFavorisService
    {

        private readonly IMemoryCache? _memoryCache;

        private string _cacheKey;

        public CacheFavorisService(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
            _cacheKey = IpAdresse.GetIpAdress();
        }

        public void AjouterFavoris(OffreEmploi offreEmploi)
        {
            OffreFavoris offreFavoris = (OffreFavoris)_memoryCache.Get(_cacheKey);

            if (offreFavoris != null && !offreFavoris.Favoris.Any(o => o.Id == offreEmploi.Id))
            {
                offreFavoris.Favoris.Add(offreEmploi);
                
            }
            else
            {
                offreFavoris = new OffreFavoris();
                offreFavoris.Favoris = new List<OffreEmploi>
                {
                    offreEmploi
                };
            }

            int tailleTotal = ObtenirTailleListOffreEmploi(offreFavoris.Favoris);
            var cacheEntryOptionsTotal = new MemoryCacheEntryOptions
            {
                SlidingExpiration = TimeSpan.FromHours(6),
                AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24),
                Size = tailleTotal
            };
            _memoryCache.Set(_cacheKey, offreFavoris, cacheEntryOptionsTotal);
        }

        public void SupprimerFavoris(int id)
        {
            OffreFavoris offreFavoris = (OffreFavoris)_memoryCache.Get(_cacheKey);
            if (offreFavoris != null && offreFavoris.Favoris.Any(o => o.Id == id))
            {
                var offreAEnlever = offreFavoris.Favoris.FirstOrDefault(o => o.Id == id);
                offreFavoris.Favoris.Remove(offreAEnlever);
                int tailleTotal = ObtenirTailleListOffreEmploi(offreFavoris.Favoris);
                var cacheEntryOptionsTotal = new MemoryCacheEntryOptions
                {
                    SlidingExpiration = TimeSpan.FromHours(6),
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24),
                    Size = tailleTotal
                };
                _memoryCache.Set(_cacheKey, offreFavoris, cacheEntryOptionsTotal);
            }
        }

        public List<OffreEmploi> ObtenirFavoris()
        {
            var cache = (OffreFavoris)_memoryCache.Get(_cacheKey);
            List<OffreEmploi> offreEmplois = new List<OffreEmploi>();
            if(cache != null)
                offreEmplois = cache.Favoris.ToList();

            return offreEmplois;
        }

        private int ObtenirTailleListOffreEmploi(IEnumerable<OffreEmploi> offreEmplois)
        {
            int taille = 0;
            foreach (var offreEmploi in offreEmplois)
            {
                taille += ObtenirTailleOffreEmploi(offreEmploi);
            }
            return taille;
        }

        private int ObtenirTailleOffreEmploi(OffreEmploi offreEmploi)
        {

            int taille = offreEmploi.DateAffichage.ToString().Length;
            taille += offreEmploi.DateDeFin.ToString().Length;
            taille += offreEmploi.Poste.Length;
            taille += offreEmploi.Nom.Length;

            if (offreEmploi.Description != null)
            {
                taille += offreEmploi.Description.Length;
            }

            return taille;
        }

        
    }
}
