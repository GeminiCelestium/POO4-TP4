using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ModernRecrut.MVC.Areas.Identity.Data;
using ModernRecrut.MVC.Models;
using System.Data;

namespace ModernRecrut.MVC.Controllers
{
    public class RoleController : Controller
    {
        private readonly UserManager<ModernRecrutMVCUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly ILogger<RoleController> _logger;


        public RoleController(UserManager<ModernRecrutMVCUser> userManager, RoleManager<IdentityRole> roleManager, ILogger<RoleController> logger)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _logger = logger;
        }


        // GET: RoleController
        public async Task<ActionResult> Index()
        {
            //string nom = User.Identity.Name;

            //var roles = new IdentityRole[]
            //{
            //    new IdentityRole("Admin"),
            //    new IdentityRole("Candidat"),
            //    new IdentityRole("RH"),
            //    new IdentityRole("Employe")
            //};

            //var users = new ModernRecrutMVCUser[]
            //{
            //    new ModernRecrutMVCUser
            //    {
            //        Id = "1",
            //        UserName = "Test1"
            //    },
            //    new ModernRecrutMVCUser
            //    {
            //        Id = "2",
            //        UserName = "Test2"
            //    },
            //    new ModernRecrutMVCUser
            //    {
            //        Id = "3",
            //        UserName = "Test3"
            //    }
            //};

            //foreach (var userItem in users)
            //{
            //    var user = await _userManager.FindByIdAsync(userItem.Id);
            //    if (user == null)
            //    {
            //        await _userManager.CreateAsync(userItem);
            //    }
            //}

            //foreach (var roleItem in roles)
            //{
            //    var role = await _roleManager.FindByNameAsync(roleItem.Name);
            //    if (role == null)
            //    {
            //        await _roleManager.CreateAsync(roleItem);
            //    }
            //}

            List<RoleViewModel> rolesViewModels = new List<RoleViewModel>();

            var vueRole = await _roleManager.Roles.ToListAsync();

            vueRole.ForEach(v => rolesViewModels.Add(
                new RoleViewModel
                {
                    RoleId = v.Id,
                    RoleName = v.Name
                }
                ));

            return View(rolesViewModels);
        }


        // GET: RoleController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RoleController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RoleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(RoleViewModel roleViewModel)
        {
           
                if (await _roleManager.Roles.AnyAsync(r => r.Name.ToLower() == roleViewModel.RoleName))
                {
                    ModelState.AddModelError("RoleName", "Il y déja un role portant ce nom ");
                   
                }


                if (ModelState.IsValid)
                {
                   
                    var roleAjoute = new IdentityRole(roleViewModel.RoleName);
                    await _roleManager.CreateAsync(roleAjoute);
          
                  _logger.LogInformation($"Ajout du role {roleViewModel.RoleName}");
                return RedirectToAction(nameof(Index));
            }


            return View(roleViewModel);
        }



        [HttpGet]
        public ActionResult Attribution()
        {
            SelectList();
            return View();
        }

        // POST: RoleController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Attribution(UserRoleViewModel userRoleViewModel)
        {
            
                if (ModelState.IsValid)
                {
                    var user = await _userManager.FindByIdAsync(userRoleViewModel.UserId);
                    var userList = await _userManager.GetUsersInRoleAsync(userRoleViewModel.RoleName);
                    if (userList.Any(u => u.UserName.ToLower() == user.UserName.ToLower()))
                    {
                        _logger.LogError($"L'utilisateur {user.UserName} est déjà membre du role {userRoleViewModel.RoleName}");
                    }
                    else
                    {
                        _logger.LogInformation($"Ajout de l'utilisateur {user.UserName} au role {userRoleViewModel.RoleName}");
                        await _userManager.AddToRoleAsync(user, userRoleViewModel.RoleName);
                    }
                }

                SelectList();
                return RedirectToAction(nameof(Index));
            
        }


        // GET: RoleController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RoleController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
           
                return RedirectToAction(nameof(Index));
           
        }

        // GET: RoleController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RoleController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Delete(RoleViewModel roleViewModel)
        {
           
                var role = await _roleManager.FindByIdAsync(roleViewModel.RoleId);

                await _roleManager.DeleteAsync(role);

                return RedirectToAction(nameof(Index));
            
        }

        private async void SelectList()
        {
            ViewBag.roles = new SelectList(_roleManager.Roles, "Name", "Name");

            ViewBag.users = new SelectList(_userManager.Users, "Id", "UserName");
        }
    }
}
