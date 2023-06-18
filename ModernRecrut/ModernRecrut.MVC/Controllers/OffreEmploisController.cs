using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModernRecrut.MVC.Helpers;
using ModernRecrut.MVC.Interfaces;
using ModernRecrut.MVC.Models;

namespace ModernRecrut.MVC.Controllers
{
    public class OffreEmploisController : Controller
    {
        public readonly IGestionEmploisService _gestionEmploisService;

        private readonly ILogger<OffreEmploisController> _logger;

        public OffreEmploisController(IGestionEmploisService gestionEmploisService, ILogger<OffreEmploisController> logger)
        {
            _gestionEmploisService = gestionEmploisService;
            _logger = logger;
        }
       

        public async Task<ActionResult> Index()
        {
            
          var offreEmplois = await _gestionEmploisService.ObtenirTout();
          
          return View(offreEmplois);
            
        }

        // GET: OffreEmploisController/Details/5

        public async Task<ActionResult> Details(int id)
        {
           
                var offreEmploi = await _gestionEmploisService.Obtenir(id);

                if (offreEmploi == null)
                {
                    return NotFound();
                }
            _logger.LogInformation(CustomLogEvents.Consultation, $"Consultation de l'offre d'emploi {id}");
        
            return View(offreEmploi);
            
        }

        // GET: OffreEmploisController/Create

        public ActionResult Create()
        {
            return View();
        }

        // POST: OffreEmploisController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(OffreEmploi offreEmploi)
        {
           
                if (offreEmploi.DateAffichage > DateTime.Now)
                    ModelState.AddModelError("DateAffichage", "La date d'affichage doit etre inférieure ou égale a la date du jour ");

                if (offreEmploi.DateDeFin < DateTime.Now)
                    ModelState.AddModelError("DateDeFin", "La date de fin doit etre supérieure ou égale a la date du jour ");

                if (ModelState.IsValid)
                {
                    var reponse = await _gestionEmploisService.Ajouter(offreEmploi);

                   if (reponse.IsSuccessStatusCode)
                   {
                     _logger.LogInformation(CustomLogEvents.Creation, $"Création de l'offre d'emploi {offreEmploi.Nom}");
                     return RedirectToAction(nameof(Index));
                   }
                   else
                   {
                       ModelState.AddModelError("Erreur", "Une erreur est survenue lors de la création de l'offre d'emploi");
                   }
               }

                return View(offreEmploi);
            

        }

        // GET: OffreEmploisController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var offreEmploi = await _gestionEmploisService.Obtenir(id);
            if (offreEmploi == null)
            {
                return NotFound();
            }
            return View(offreEmploi);
        }

        // POST: OffreEmploisController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, OffreEmploi offreEmploi)
        {
            if (offreEmploi.DateAffichage > DateTime.Now)
                ModelState.AddModelError("DateAffichage", "La date d'affichage doit etre inférieure ou égale a la date du jour ");

            if (offreEmploi.DateDeFin < DateTime.Now)
                ModelState.AddModelError("DateDeFin", "La date de fin doit etre supérieure ou égale a la date du jour ");

            if (ModelState.IsValid)
            {
                
                 await _gestionEmploisService.Modifier(offreEmploi);

                _logger.LogInformation(CustomLogEvents.Modication, $"Modification de l'offre d'emploi {offreEmploi.Id}");


                return RedirectToAction(nameof(Index));
            }
            return View(offreEmploi);
        }

        // GET: OffreEmploisController/Delete/5

        public async Task<ActionResult> Delete(int id)
        {
            var offreEmploi = await _gestionEmploisService.Obtenir(id);
            if (offreEmploi != null && ModelState.IsValid)
            {
                return View(offreEmploi);
            }

            return NotFound();
        }

        // POST: OffreEmploisController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(OffreEmploi offreEmploi)
        {
            await _gestionEmploisService.Supprimer(offreEmploi.Id);

            _logger.LogInformation(CustomLogEvents.Suppression, $"Suppression de l'offre d'emploi {offreEmploi.Id}");


            return RedirectToAction(nameof(Index));
        }

        
    }
}
