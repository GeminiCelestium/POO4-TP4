using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using ModernRecrut.MVC.Areas.Identity.Data;
using ModernRecrut.MVC.Helpers;
using ModernRecrut.MVC.Interfaces;
using ModernRecrut.MVC.Models;


namespace ModernRecrut.MVC.Controllers
{
    public class PostulationController : Controller
    {
        public readonly IGestionEmploisService _gestionEmploisService;
        private readonly IGestionEmploisService _gestionEmploisServiceProxy;
        private readonly IGestionDocumentsService _gestionDocumentsServiceProxy;
        private readonly IGestionPostulationsService _gestionPostulationsServiceProxy;
        private readonly ILogger<OffreEmploisController> _logger;
        private readonly UserManager<ModernRecrutMVCUser> _userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IWebHostEnvironment _env; 


        public PostulationController(IGestionEmploisService gestionEmploisService, IGestionEmploisService gestionEmploisServiceProxy, IGestionDocumentsService gestionDocumentsServiceProxy, IGestionPostulationsService gestionPostulationsServiceProxy, ILogger<OffreEmploisController> logger, IWebHostEnvironment env)
        {
            _gestionEmploisService = gestionEmploisService;
            _gestionEmploisServiceProxy = gestionEmploisServiceProxy;
            _gestionDocumentsServiceProxy = gestionDocumentsServiceProxy;
            _gestionPostulationsServiceProxy = gestionPostulationsServiceProxy;
            _logger = logger;
            _env = env;
            
        }

        // GET: PostulationController
        [Authorize(Roles = "Employe, Admin")]
        public async Task<ActionResult> ListePostulations()
        {
            var postulations = await _gestionPostulationsServiceProxy.ObtenirTout();

            return View(postulations);
        }

        // GET: PostulationController/Details/5
        [Authorize(Roles = "RH, Admin")]
        public ActionResult Notes(int id)
        {
            return View();
        }

        // GET: PostulationController/Create
        [Authorize(Roles = "Candidat, Admin")]
        public async Task<ActionResult> Postuler()
        {
            try
            {
                 return View();     
            }
            catch
            {
                return BadRequest();
            }
        }

        // POST: PostulationController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Postuler(Postulation postulation)
        {
            // TODO : Valider si tout est correct.

            if ()
            {

            }

            try
            {
                var reponse = await _gestionPostulationsServiceProxy.Creer(postulation);

                if (reponse.IsSuccessStatusCode)
                {
                    _logger.LogInformation(CustomLogEvents.Creation, $"Création de la postulation {postulation.Id}");
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError("Erreur", "Une erreur est survenue lors de la création de la postulation");
                }

                return View(postulation);
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: PostulationController/Edit/5
        public async Task<ActionResult> Edit(int id)
        {
            var postulation = await _gestionPostulationsServiceProxy.Obtenir(id);

            if (postulation == null)
            {
                return NotFound();
            }

            return View(postulation);
        }

        // POST: PostulationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, Postulation postulation)
        {
            try
            {
                await _gestionPostulationsServiceProxy.Modifier(postulation);

                _logger.LogInformation(CustomLogEvents.Modication, $"Modification de la postulation {postulation.Id}");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(postulation);
            }
        }

        // GET: PostulationController/Delete/5
        public async Task<ActionResult> Delete(int id)
        {
            var postulation = await _gestionPostulationsServiceProxy.Obtenir(id);

            if (postulation != null && ModelState.IsValid)
            {
                return View(postulation);
            }

            return NotFound();
        }

        // POST: PostulationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(Postulation postulation)
        {
            try
            {
                await _gestionPostulationsServiceProxy.Supprimer(postulation.Id);

                _logger.LogInformation(CustomLogEvents.Suppression, $"Suppression de la postulation {postulation.Id}");

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
