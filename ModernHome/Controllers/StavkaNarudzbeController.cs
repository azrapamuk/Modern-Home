using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ModernHome.Data;
using ModernHome.Models;

namespace ModernHome.Controllers
{
    public class StavkaNarudzbeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StavkaNarudzbeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: StavkaNarudzbe
        [Authorize(Roles = "Korisnik, Administrator")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.StavkaNarudzbe.ToListAsync());
        }

        // GET: StavkaNarudzbe/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stavkaNarudzbe = await _context.StavkaNarudzbe
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stavkaNarudzbe == null)
            {
                return NotFound();
            }

            return View(stavkaNarudzbe);
        }

        // GET: StavkaNarudzbe/Create
        /* [Authorize(Roles = "Korisnik, Administrator")]
          public IActionResult Create()
          {
              return View();
          }*/

        [Authorize(Roles = "Korisnik, Administrator")]
        public IActionResult Create(int Idartikal, double cijena)
         {
             ViewData["Idartikal"] = Idartikal;
             ViewData["cijena"] = cijena;
             return View();
         }

        // POST: StavkaNarudzbe/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Korisnik, Administrator")]
        public async Task<IActionResult> Create([Bind("Id,Idartikal,kolicina,cijena,Idkorpa")] StavkaNarudzbe stavkaNarudzbe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(stavkaNarudzbe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(stavkaNarudzbe);
        }

        // GET: StavkaNarudzbe/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stavkaNarudzbe = await _context.StavkaNarudzbe.FindAsync(id);
            if (stavkaNarudzbe == null)
            {
                return NotFound();
            }
            return View(stavkaNarudzbe);
        }

        // POST: StavkaNarudzbe/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Idartikal,kolicina,cijena,Idkorpa")] StavkaNarudzbe stavkaNarudzbe)
        {
            if (id != stavkaNarudzbe.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(stavkaNarudzbe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StavkaNarudzbeExists(stavkaNarudzbe.Id))
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
            return View(stavkaNarudzbe);
        }

        // GET: StavkaNarudzbe/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var stavkaNarudzbe = await _context.StavkaNarudzbe
                .FirstOrDefaultAsync(m => m.Id == id);
            if (stavkaNarudzbe == null)
            {
                return NotFound();
            }

            return View(stavkaNarudzbe);
        }

        // POST: StavkaNarudzbe/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var stavkaNarudzbe = await _context.StavkaNarudzbe.FindAsync(id);
            if (stavkaNarudzbe != null)
            {
                _context.StavkaNarudzbe.Remove(stavkaNarudzbe);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StavkaNarudzbeExists(int id)
        {
            return _context.StavkaNarudzbe.Any(e => e.Id == id);
        }
        private double IzracunajUkupnuCijenuKorpe(int idKorpe)
        {
            var ukupnaCijena = _context.StavkaNarudzbe
                .Where(s => s.Idkorpa == idKorpe)
                .Sum(s => s.cijena);

            return ukupnaCijena;
        }
    }
}
