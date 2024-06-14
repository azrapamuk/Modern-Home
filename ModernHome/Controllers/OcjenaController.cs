using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ModernHome.Data;
using ModernHome.Models;
using Microsoft.AspNetCore.Identity;
using System.Transactions;
using Microsoft.VisualBasic;
using System.Data.SqlTypes;
using Microsoft.CodeAnalysis.Elfie.Serialization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;



namespace ModernHome.Controllers
{
    public class OcjenaController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;


        public OcjenaController(ApplicationDbContext context, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _context = context;
            _userManager = userManager;
            _signInManager = signInManager;

        }

        // GET: Ocjenas
        public async Task<IActionResult> Index(int? id)

        {
            return View(await _context.Ocjena.ToListAsync());
        }

        // GET: Ocjenas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            ViewBag.ocjenaid = id;
            if (id == null)
            {
                return NotFound();
            }

            var ocjena = await _context.Ocjena
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ocjena == null)
            {
                return NotFound();
            }

            return View(ocjena);
        }

        //[Authorize(Roles="Administrator, Korisnik")]
        // GET: Ocjenas/OcjeneFilma/5
        public async Task<IActionResult> OcjeneArtikla(int? id)
        {
            ViewBag.Idocjene = id;
            if (id == null)
            {
                return NotFound();
            }

            var ocjena = await _context.Ocjena
                .Where(o => o.Idartikal == id)
                .ToListAsync();
            ViewBag.ocjenaid = id;
            if (ocjena == null)
            {
                return NotFound();
            }

            return View(ocjena);
        }

        // GET: Ocjenas/OcijeniFilm/5
        [HttpGet]
        [Authorize(Roles = "Korisnik")]
        public async Task<IActionResult> OcijeniArtikal(int? id)
        {
            var film = await _context.Artikal.FindAsync(id);
           // if (film.StatusPrikazivanja != StatusPrikazivanja.Aktuelan) return RedirectToAction("NajavljeniFilmovi", "Films");

            var user = await _userManager.GetUserAsync(User);
            var korisnik1 = await _userManager.GetUserNameAsync(user);
            var korisnik = await _userManager.GetUserIdAsync(user);
            var postojecaOcjena = await _context.Ocjena
            .FirstOrDefaultAsync(o => o.Idartikal == id && o.Idkorisnik == korisnik);

            if (postojecaOcjena != null)
            {
                // Ako korisnik već ima ocjenu, preusmjeri ga na neku drugu stranicu
                return RedirectToAction("Details", "Artikal", new { id });
            }

            var artikal = await _context.Artikal.FindAsync(id);
            ViewBag.Artikal = artikal;

            ViewBag.Idkorisnik = korisnik;
            ViewBag.Idartikal = id;
            return View();
        }

        // POST: Ocjenas/OcijeniFilm/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> OcijeniArtikal([Bind("ocjena,komentar,Idkorisnik,Idartikal,datum")] Ocjena ocjenaA)
        {
            var user = await _userManager.GetUserAsync(User);
            var korisnik = await _userManager.GetUserIdAsync(user);
            ocjenaA.datum = DateTime.Today;
            ocjenaA.Idkorisnik = korisnik;

            if (ModelState.IsValid)
            {
                var artikal = await _context.Artikal.FindAsync(ocjenaA.Idartikal);
              
                    if ((User.IsInRole("Administrator")) || ocjenaA.Idkorisnik == korisnik)
                    {
                        _context.Add(ocjenaA);
                        await _context.SaveChangesAsync();
                        return RedirectToAction("Details", "Artikal", new { id = ocjenaA.Idartikal });
                    }
                    else if (ocjenaA.Idkorisnik != korisnik)
                    {

                        return RedirectToAction("OcjeneArtikla", "Ocjena", new { id = ocjenaA.Idartikal });

                    }
               


            }
            return View(ocjenaA);
        }


        // GET: Ocjenas/Create
        /*[HttpGet]

        public async Task<IActionResult> Create(int?id)
        {
            var user = await _userManager.GetUserAsync(User);
            var korisnik1 = await _userManager.GetUserNameAsync(user);
            var korisnik = await _userManager.GetUserIdAsync(user);
            ViewBag.KorisnikId = korisnik1;
            ViewBag.FilmId = id;
            return View();
        }

        // POST: Ocjenas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("id,ocjenaFilma,komentar,datum,korisnikId,FilmId")] Ocjena ocjena)
        {
            var user = await _userManager.GetUserAsync(User);
            var korisnik = await _userManager.GetUserIdAsync(user);          
            ocjena.datum = DateTime.Today;
            ocjena.korisnikId = korisnik;

            
            if (ModelState.IsValid)
            {

                if (ocjena.korisnikId != korisnik)
                {
                    return NotFound();
                }
                _context.Add(ocjena);
                await _context.SaveChangesAsync();
                /*var ocjeneFilma = new OcjeneFilma
                {
                    FilmId = 3, 
                    OcjenaId = ocjena.id 
                };

                //_context.OcjeneFilma.Add(ocjeneFilma);
                //await _context.SaveChangesAsync();

                //var film = await _context.Film.FirstOrDefaultAsync();
                //          ocjeneFilma.FilmId = film.id;
                //          ocjeneFilma.OcjenaId = ocjena.id;
                //          _context.OcjeneFilma.Add(ocjeneFilma);
                //          await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            if (ModelState.IsValid)
            {
                _context.Add(ocjena);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ocjena);
            return View(ocjena);
        }*/

        // GET: Ocjenas/Edit/5
        [Authorize(Roles = "Administrator, Korisnik")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ocjena = await _context.Ocjena.FindAsync(id);
            var user = await _userManager.GetUserAsync(User);
            var korisnik = await _userManager.GetUserIdAsync(user);

            if ((User.IsInRole("Administrator")) || ocjena.Idkorisnik == korisnik)
            {
                ViewBag.UserId = korisnik;
                ViewBag.UserArtikal = ocjena.Idartikal;
                if (ocjena == null)
                {
                    return NotFound();
                }
            }
            else if (ocjena.Idkorisnik != korisnik)
            {

                return RedirectToAction("OcjeneArtikla", "Ocjena", new { id = ocjena.Idartikal });

            }
            return View(ocjena);
        }

        // POST: Ocjenas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,ocjena,komentar,datum,Idkorisnik,Idartikal")] Ocjena ocjena)
        {
            if (id != ocjena.Id)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            var korisnik = await _userManager.GetUserIdAsync(user);
            //ocjena.korisnikId = korisnik;
            ViewBag.UserFilm = ocjena.Idartikal;
            ViewBag.korisnikId = ocjena.Idkorisnik;


            if (ModelState.IsValid)
            {
                if ((User.IsInRole("Administrator")) || ocjena.Idkorisnik == korisnik)
                {
                    try
                    {
                        _context.Update(ocjena);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!OcjenaExists(ocjena.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                }
                else if (ocjena.Idkorisnik != korisnik)
                {
                    return RedirectToAction("OcjeneArtikla", "Ocjena", new { id = ocjena.Idartikal });
                }
            }
            return View(ocjena);
        }

        // GET: Ocjenas/Delete/5

        public async Task<IActionResult> Delete(int? id)
        {
            ViewBag.Id = id;
            if (id == null)
            {
                return NotFound();
            }
            var user = await _userManager.GetUserAsync(User);
            var korisnik = await _userManager.GetUserIdAsync(user);
            var ocjena = await _context.Ocjena
                .FirstOrDefaultAsync(m => m.Id == id);

            if ((User.IsInRole("Administrator")) || ocjena.Idkorisnik == korisnik)
            {
                if (ocjena == null)
                {
                    return NotFound();
                }
            }
            else if (ocjena.Idkorisnik != korisnik)
            {
                return RedirectToAction("OcjeneArtikla", "Ocjena", new { id = ocjena.Idartikal });
            }

            return View(ocjena);
        }

        // POST: Ocjenas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var korisnik = await _userManager.GetUserIdAsync(user);
            var ocjena = await _context.Ocjena.FindAsync(id);
            if ((User.IsInRole("Administrator")) || ocjena.Idkorisnik == korisnik)
            {
                if (ocjena != null)
                {
                    _context.Ocjena.Remove(ocjena);
                }
            }
            else if (ocjena.Idkorisnik != korisnik)
            {
                return RedirectToAction("OcjeneArtikla", "Ocjena", new { id = ocjena.Idartikal });
            }


            await _context.SaveChangesAsync();
            return RedirectToAction("OcjeneArtikla", "Ocjena", new { id = ocjena.Idartikal });
        }
      
        private bool OcjenaExists(int id)
        {
            return _context.Ocjena.Any(e => e.Id == id);
        }
    }
}
