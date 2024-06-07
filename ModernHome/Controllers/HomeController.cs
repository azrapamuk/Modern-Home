using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModernHome.Data;
using ModernHome.Models;

namespace ModernHome.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly RoleManager<IdentityRole> _roleManager;
        
        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> userManager, ApplicationDbContext context, RoleManager<IdentityRole> roleManager)
        {
            _logger = logger;
            _context = context;
            _roleManager=roleManager;
            _userManager=userManager;
                
        }
        [Authorize(Roles = "Administrator")]
        // POST: Users/EditRoles/id
        [HttpPost]
        public async Task<IActionResult> SviKorisniciEdit(string username, List<string> selectedRoles)
        {
            if (string.IsNullOrEmpty(username))
            {
                return BadRequest("Username varijabla prilikom slanja je prazna");
            }

            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound();
            }

            var currentRoles = await _userManager.GetRolesAsync(user);

            var result = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Ne mogu maæi rolu");
                return View();
            }

            result = await _userManager.AddToRolesAsync(user, selectedRoles ?? new List<string>());
            if (!result.Succeeded)
            {
                ModelState.AddModelError("", "Ne mogu dati rolu");
                return View();
            }

            return RedirectToAction("SviKorisnici");
        }
        public IActionResult Index()
        {
            return View();
        }
    
        public IActionResult Privacy()
        {
            return View();
        }

        [Authorize(Roles = "Administrator")]
        public IActionResult SviKorisnici()
        {
            var users = _userManager.Users.ToList();
            users = users.OrderByDescending(u => _userManager.GetRolesAsync(u).Result.Contains("Administrator"))
                 .ThenBy(u => _userManager.GetRolesAsync(u).Result.Contains("Korisnik"))
                 .ToList();
            return View(users);
        }
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(string username)
        {
            var user = await _userManager.FindByNameAsync(username);
            ViewBag.userid = user;
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                await _context.SaveChangesAsync();
                return RedirectToAction("SviKorisnici");
            }
            return RedirectToAction("SviKorisnici");
        }
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> SviKorisniciEdit(string username)
        {


            var user = await _userManager.FindByNameAsync(username);
            if (user == null)
            {
                return NotFound();
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            var roles = _roleManager.Roles.ToList();

            ViewBag.UserId = user.Id;
            ViewBag.UserName = user.UserName;
            ViewBag.Roles = roles;
            ViewBag.UserRoles = userRoles;

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
