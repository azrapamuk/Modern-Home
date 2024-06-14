using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ModernHome.Data;
using ModernHome.Models;

namespace ModernHome.Controllers
{
    public class ArtikalController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public ArtikalController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Artikal
        // [Authorize(Roles = "Administrator, Korisnik")]
        public async Task<IActionResult> Index(string query)
        {
            IQueryable<Artikal> artikliQuery = _context.Artikal;

            if (!string.IsNullOrEmpty(query))
            {
                artikliQuery = artikliQuery.Where(a => a.naziv.Contains(query));
            }

            var artikli = await artikliQuery.ToListAsync();

            ViewBag.Query = query; // Pass query to the view

            return View(artikli);
        }

        public async Task<IActionResult> SobeFiltriranje(string query)
        {
            IQueryable<Artikal> artikliQuery = _context.Artikal;

            switch (query)
            {
                case "0":
                    artikliQuery = artikliQuery.Where(a => a.tip == TipNamjestaja.dnevnasoba);
                    break;
                case "1":
                    artikliQuery = artikliQuery.Where(a => a.tip == TipNamjestaja.kuhinja);
                    break;
                case "2":
                    artikliQuery = artikliQuery.Where(a => a.tip == TipNamjestaja.spavacasoba);
                    break;
                case "3":
                    artikliQuery = artikliQuery.Where(a => a.tip == TipNamjestaja.ured);
                    break;
                case "4":
                    artikliQuery = artikliQuery.Where(a => a.tip == TipNamjestaja.dekoracije);
                    break;
                case "5":
                    artikliQuery = artikliQuery.Where(a => a.tip == TipNamjestaja.razno);
                    break;
            }

            var artikli = await artikliQuery.ToListAsync();

            ViewBag.Query = query;


            return View("Index", artikli);
        }

        public async Task<IActionResult> Filtriranje(string query)
        {
            IQueryable<Artikal> artikliQuery = _context.Artikal;

            if (query == "00")
            {
                artikliQuery = artikliQuery.OrderBy(a => a.cijena);
            }
            else if (query == "11")
            {
                artikliQuery = artikliQuery.OrderByDescending(a => a.cijena);
            }
            else
            {
                switch (query)
                {
                    case "0":
                        artikliQuery = artikliQuery.Where(a => a.boja == Boje.crvena);
                        break;
                    case "1":
                        artikliQuery = artikliQuery.Where(a => a.boja == Boje.narandzasta);
                        break;
                    case "2":
                        artikliQuery = artikliQuery.Where(a => a.boja == Boje.zuta);
                        break;
                    case "3":
                        artikliQuery = artikliQuery.Where(a => a.boja == Boje.zelena);
                        break;
                    case "4":
                        artikliQuery = artikliQuery.Where(a => a.boja == Boje.plava);
                        break;
                    case "5":
                        artikliQuery = artikliQuery.Where(a => a.boja == Boje.ljubicasta);
                        break;
                    case "6":
                        artikliQuery = artikliQuery.Where(a => a.boja == Boje.roza);
                        break;
                    case "7":
                        artikliQuery = artikliQuery.Where(a => a.boja == Boje.bijela);
                        break;
                    case "8":
                        artikliQuery = artikliQuery.Where(a => a.boja == Boje.siva);
                        break;
                    case "9":
                        artikliQuery = artikliQuery.Where(a => a.boja == Boje.crna);
                        break;
                    case "10":
                        artikliQuery = artikliQuery.Where(a => a.boja == Boje.smedja);
                        break;
                    case "11":
                        artikliQuery = artikliQuery.Where(a => a.boja == Boje.bez);
                        break;
                }
            }

            var artikli = await artikliQuery.ToListAsync();

            return View("Index", artikli);
        }

        // GET: Artikal/Details/5
        public async Task<IActionResult> Details(int? id)
        {
             if (id == null)
             {
                 return NotFound();
             }

             var artikal = await _context.Artikal
                 .FirstOrDefaultAsync(m => m.Id == id);
             if (artikal == null)
             {
                 return NotFound();
             }

            var dimenzije = await _context.Dimenzije.FindAsync(artikal.Iddimenzije);
            ViewBag.Dimenzije = dimenzije; 

            return View(artikal);
        }

        public IActionResult FilterNaziv(string query)
        {
            // Query items based on search query
            var artikli = _context.Artikal.Where(a => a.naziv.Contains(query)).ToList();

            ViewBag.Query = query; // Pass query to the view

            return View(artikli);
       
        }

        // GET: Artikal/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Artikal/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,naziv,tip,boja,kolicina,cijena,Iddimenzije,slika")] Artikal artikal)
        {
            if (ModelState.IsValid)
            {
                _context.Add(artikal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(artikal);
        }

        // GET: Artikal/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artikal = await _context.Artikal.FindAsync(id);
            if (artikal == null)
            {
                return NotFound();
            }
            return View(artikal);
        }



        // POST: Artikal/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,naziv,tip,boja,kolicina,cijena,Iddimenzije,slika")] Artikal artikal)
        {
            if (id != artikal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(artikal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ArtikalExists(artikal.Id))
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
            return View(artikal);
        }

        // GET: Artikal/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var artikal = await _context.Artikal
                .FirstOrDefaultAsync(m => m.Id == id);
            if (artikal == null)
            {
                return NotFound();
            }

            return View(artikal);
        }

        // POST: Artikal/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var artikal = await _context.Artikal.FindAsync(id);
            if (artikal != null)
            {
                _context.Artikal.Remove(artikal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ArtikalExists(int id)
        {
            return _context.Artikal.Any(e => e.Id == id);
        }
    }
}
