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
        public async Task<IActionResult> Index()
        {
            return View(await _context.Artikal.ToListAsync());
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

             return View(artikal);
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
