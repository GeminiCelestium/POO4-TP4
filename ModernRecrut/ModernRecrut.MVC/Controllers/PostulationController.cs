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

        private readonly IGestionEmploisService _gestionEmploisServiceProxy;
        private readonly IGestionDocumentsService _gestionDocumentsServiceProxy;
        private readonly UserManager<ModernRecrutMVCUser> _userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private readonly IWebHostEnvironment _env; 


        public PostulationController(IGestionEmploisService gestionEmploisServiceProxy, IGestionDocumentsService gestionDocumentsServiceProxy, IWebHostEnvironment env)
        {
            _gestionEmploisServiceProxy = gestionEmploisServiceProxy;
            _gestionDocumentsServiceProxy = gestionDocumentsServiceProxy;
            _env = env;
        }



        // GET: PostulationController
        [Authorize(Roles = "Employe, Admin")]
        public ActionResult ListePostulations()
        {
            return View();
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
            try
            {
                
                return View();
            }
            catch
            {
                return BadRequest();
            }
        }

        // GET: PostulationController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PostulationController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PostulationController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PostulationController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
