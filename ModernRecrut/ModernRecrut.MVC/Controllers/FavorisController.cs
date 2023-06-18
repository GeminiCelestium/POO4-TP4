using Microsoft.AspNetCore.Mvc;
using ModernRecrut.MVC.Interfaces;
using ModernRecrut.MVC.Models;

namespace ModernRecrut.MVC.Controllers
{
    public class FavorisController : Controller
    {

        private readonly IGestionFavorisService _gestionFavorisServiceProxy;
        private readonly IGestionEmploisService _gestionEmploisServiceProxy;

        public FavorisController(IGestionFavorisService GestionFavorisServiceProxy, IGestionEmploisService GestionEmploisServiceProxy)
        {
            _gestionFavorisServiceProxy = GestionFavorisServiceProxy;
            _gestionEmploisServiceProxy = GestionEmploisServiceProxy;

        }
        // GET: FavorisController
        public async Task<ActionResult> Index()
        {

           
                var offreEmplois = new List<OffreEmploi>();
                var listeFavoris = await _gestionFavorisServiceProxy.ObtenirTout();
                foreach (OffreEmploi favoris in listeFavoris)
                {
                    offreEmplois.Add(favoris);
                }
                return View(offreEmplois);
           
        }

        // GET: FavorisController/Details/5
        public async Task<ActionResult> Details(int id)
        {

            
                var offreEmplois = new List<OffreEmploi>();
                var listeFavoris = await _gestionFavorisServiceProxy.ObtenirTout();
                foreach (OffreEmploi favoris in listeFavoris)
                {
                    offreEmplois.Add(favoris);
                }
                if (offreEmplois.Any(e => e.Id == id))
                {
                    return View(offreEmplois.FirstOrDefault(e => e.Id == id));
                }
                else
                {
                    return NotFound();
                }
            
        }

        // GET: FavorisController/Create
        public async Task<ActionResult> Create(int id)
        {
            
                var offreEmploi = await _gestionEmploisServiceProxy.Obtenir(id);
                if (offreEmploi != null && ModelState.IsValid)
                {
                    return View(offreEmploi);
                }
                else
                {
                    return NotFound();
                }
          

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OffreEmploi offre)
        {
            
                await _gestionFavorisServiceProxy.Ajouter(offre);
                return RedirectToAction(nameof(Index));
            
        }
        public async Task<ActionResult> Delete(int id)
        {
           
                var offreEmploi = await _gestionEmploisServiceProxy.Obtenir(id);
                if (offreEmploi != null && ModelState.IsValid)
                {
                    return View(offreEmploi);
                }
                else
                {
                    return NotFound();
                }
          
        }
        // GET: FavorisController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(OffreEmploi offreEmploi)
        {
            
                await _gestionFavorisServiceProxy.Supprimer(offreEmploi.Id);
                return RedirectToAction(nameof(Index));
            

        }

    }
}
