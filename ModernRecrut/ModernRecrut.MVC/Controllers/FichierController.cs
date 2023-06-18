﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ModernRecrut.MVC.Interfaces;
using ModernRecrut.MVC.Models;
using ModernRecrut.MVC.Helpers;
using Microsoft.AspNetCore.Identity;
using ModernRecrut.MVC.Areas.Identity.Data;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ModernRecrut.MVC.Controllers
{
    public class FichierController : Controller
    {

        private readonly IGestionDocumentsService _gestionDocumentsServiceProxy;
        private readonly IWebHostEnvironment _env;
        private readonly ILogger<FichierController> _logger;
        private readonly UserManager<ModernRecrutMVCUser> _userManager;
        private readonly IConfiguration _config;

        public FichierController(IGestionDocumentsService gestionDocumentsServiceProxy, IWebHostEnvironment env, ILogger<FichierController> logger, UserManager<ModernRecrutMVCUser> userManager, IConfiguration config)
        {
            _gestionDocumentsServiceProxy = gestionDocumentsServiceProxy;
            _env = env;
            _logger = logger;
            _userManager = userManager;
            _config = config;
        }

        public async Task<ActionResult> Index()
        {
            await ObtenirListeCandidat();
            return View();
        }
        // GET: FichierController
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Details(ModernRecrutMVCUser modernRecrutMVCUser)
        {
           
                var user = await _userManager.FindByIdAsync(modernRecrutMVCUser.Id);
                if (user.Type == TypeOccupation.Candidat)
                {
                    var Id = modernRecrutMVCUser.Id;
                    var listeFichiersSansChemin = await _gestionDocumentsServiceProxy.ObtenirTout(Id);
                    var listeFichiersAvecChemin = new List<Fichier>();

                    foreach (var fichier in listeFichiersSansChemin)
                    {
                        var typeDocumentString = fichier.Split('_')[1];
                        var typeDocument = (TypeDocument)Enum.Parse(typeof(TypeDocument), typeDocumentString);
                        var fichierAvecChemin = new Fichier { FileName = _config.GetValue<string>("UrlDocumentsAPI") + "documents/" + fichier, Name = fichier, TypeDocument = typeDocument };
                        listeFichiersAvecChemin.Add(fichierAvecChemin);
                    }

                    return View(listeFichiersAvecChemin);
                }
                else
                {
                    _logger.LogError(CustomLogEvents.Erreur, $"Erreur lors de l'ajout d'un document : L'utilisateur n'est pas authentifié");
                    return View(new List<Fichier>());
                }
        }
            
        

        // GET: FichierController/Create
        public async Task<ActionResult> Create()
        {
            return View();
        }

        // POST: FichierController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind("TypeDocument,DataFormFile")] Fichier fichier)
        {
           
                if (ModelState.IsValid)
                {
                    if (User.Identity.IsAuthenticated)
                    {
                        var user = await _userManager.FindByNameAsync(User.Identity.Name);
                        var Id = user.Id;
                        fichier.Id = Id;

                        Fichier document = new Fichier()
                        {
                            DataFile =  await ConversionDocument.ConvertirDocumentEnString(fichier.DataFormFile),
                            Name = fichier.DataFormFile.Name,
                            FileName = fichier.DataFormFile.FileName,
                            Id = fichier.Id,
                            TypeDocument = fichier.TypeDocument
                        };

                        await _gestionDocumentsServiceProxy.Ajouter(document);

                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        _logger.LogError(CustomLogEvents.Erreur, $"Erreur lors de l'ajout d'un document : L'utilisateur n'est pas authentifié");
                        return BadRequest();
                    }
                }
               
          return View();
            
        }
        private async Task ObtenirListeCandidat()
        {
            ViewBag.users = new SelectList(await _userManager.GetUsersInRoleAsync("Candidat"), "Id", "UserName");
        }
    
}
}
