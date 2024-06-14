using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Scripting;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using ModernHome.Data;
using ModernHome.Models;

namespace ModernHome.Controllers
{
    [Authorize]
    public class NarudzbaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        public NarudzbaController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // GET: Narudzba
        [Authorize(Roles = "Administrator, Korisnik, Uposlenik")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Narudzba.ToListAsync());
        }

        // GET: Narudzba/Details/5
        [Authorize(Roles = "Administrator, Uposlenik")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudzba = await _context.Narudzba
                .FirstOrDefaultAsync(m => m.Id == id);
            if (narudzba == null)
            {
                return NotFound();
            }

            return View(narudzba);
        }

        // GET: Narudzba/Create
        [Authorize(Roles = "Administrator, Korisnik")]
        public IActionResult Create()
        {

            var userid = _userManager.GetUserId(HttpContext.User);
            var korpaIds = _context.Korpa
                               .Where(k => k.Idkorisnik == userid)
                               .Select(k => k.Id)
                               .ToList();
            var KorpaID = "";
            if (korpaIds != null && korpaIds.Any())
            {
                // Convert the first element to string
                KorpaID = korpaIds.First().ToString();
            }
            else
            {
                // Initialize KorpaID with a default value in case korpaIds is null or empty
                KorpaID = "";
            }
            ViewData["Idkorpa"] = KorpaID;
            //ViewData["cijena"] = cijena;

            ViewData["korisnik"] = userid;
            //return View();
            bool nemaNaStanju = false;
            var stavkeNarudzbe = _context.StavkaNarudzbe
                                    .Where(s => s.Idkorpa == Convert.ToInt32(KorpaID))
                                    .ToList();

            // Ažuriranje količina stavki
            foreach (var stavka in stavkeNarudzbe)
            {
                // Pronalazak odgovarajućeg artikla
                var artikal = _context.Artikal.Find(stavka.Idartikal);

                if (artikal != null)
                {
                    if (artikal.kolicina < stavka.kolicina)
                    {
                        nemaNaStanju = true;
                        TempData["Poruka"] = "Nema dovoljno zaliha za artikal: " + artikal.naziv;
                        break;
                    }
                    else
                    {
                        artikal.kolicina -= stavka.kolicina;
                    }
                    // Možete dodati dodatne logike ovdje, ako je potrebno

                    // Ažuriranje stanja u bazi podataka
                    _context.Update(artikal);
                }
            }
            if (nemaNaStanju)
            {
                //ModelState.AddModelError(nameof(Artikal.kolicina), "Nedovoljno artikala na stanju za ovu narudzbu");
                return RedirectToAction("NemaNaStanju", "Narudzba");
            }


            var stavkeNarudzbe1 = _context.StavkaNarudzbe
                .Where(s => s.Idkorpa == Convert.ToInt32(KorpaID))
                .ToList();


            double ukupnaCijena = stavkeNarudzbe.Sum(s => s.cijena * s.kolicina);

            ViewData["Ucijena"] = ukupnaCijena;



            // Čuvanje promjena u bazi podataka
            _context.SaveChanges();


            return View();
        }

        // POST: Narudzba/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Idkorisnik,vrijemeNarudzbe,stanjeIsporuke,Idkorpa, cijena")] Narudzba narudzba)
        {

            narudzba.vrijemeNarudzbe = DateTime.Now;


            var userId = _userManager.GetUserId(HttpContext.User);


            var korpa = await _context.Korpa.FirstOrDefaultAsync(k => k.Idkorisnik == userId);

            if (korpa != null)
            {

                var stavkeNarudzbe = await _context.StavkaNarudzbe
                    .Where(s => s.Idkorpa == korpa.Id)
                    .ToListAsync();


                double ukupnaCijena = stavkeNarudzbe.Sum(s => s.cijena * s.kolicina);

                ViewData["cijena"] = ukupnaCijena;
                narudzba.cijena = ukupnaCijena;
            }
            if (ModelState.IsValid)
            {
                _context.Add(narudzba);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(narudzba);

        }

        // GET: Narudzba/Edit/5

        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudzba = await _context.Narudzba.FindAsync(id);
            if (narudzba == null)
            {
                return NotFound();
            }
            return View(narudzba);
        }

        // POST: Narudzba/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> Edit(int id, [Bind("Id,Idkorisnik,vrijemeNarudzbe,stanjeIsporuke,Idkorpa")] Narudzba narudzba)
        {
            if (id != narudzba.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(narudzba);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NarudzbaExists(narudzba.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(narudzba);
        }

        // GET: Narudzba/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var narudzba = await _context.Narudzba
                .FirstOrDefaultAsync(m => m.Id == id);
            if (narudzba == null)
            {
                return NotFound();
            }

            return View(narudzba);
        }

        // POST: Narudzba/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var narudzba = await _context.Narudzba.FindAsync(id);
            if (narudzba != null)
            {
                _context.Narudzba.Remove(narudzba);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NarudzbaExists(int id)
        {
            return _context.Narudzba.Any(e => e.Id == id);
        }
        public IActionResult NemaNaStanju()
        {
            ViewData["Poruka"] = TempData["Poruka"];
            return View();
        }
       

    }
}
