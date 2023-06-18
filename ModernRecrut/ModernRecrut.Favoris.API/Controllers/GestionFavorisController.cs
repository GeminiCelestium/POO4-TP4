using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using ModernRecrut.Favoris.API.Helpers;
using ModernRecrut.Favoris.API.Interfaces;
using ModernRecrut.Favoris.API.Models;

namespace ModernRecrut.Favoris.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GestionFavorisController : Controller
    {
        
        private readonly ICacheFavorisService _cacheFavorisService;


        public GestionFavorisController(ICacheFavorisService cacheFavorisService)
        {
           
            _cacheFavorisService = cacheFavorisService;
           
        }

        [HttpGet]
        public ActionResult<List<OffreEmploi>> ObtenirFavoris()
        {
            
            return _cacheFavorisService.ObtenirFavoris();
          
        }

        [HttpPost]
        public ActionResult Ajouter(OffreEmploi offreEmploi)
        {
           _cacheFavorisService.AjouterFavoris(offreEmploi);
          return Ok();
            
        }
        [HttpDelete("{id}")]
        public ActionResult Supprimer(int id)
        {
           _cacheFavorisService?.SupprimerFavoris(id);
           return Ok();
            
        }
    }
}
