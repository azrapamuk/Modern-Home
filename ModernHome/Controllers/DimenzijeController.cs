using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ModernHome.Data;
using ModernHome.Models;

namespace ModernHome.Controllers
{
    public class DimenzijeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DimenzijeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Dimenzije
        [Authorize(Roles = "Administrator, Korisnik")]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Dimenzije.ToListAsync());
        }

        // GET: Dimenzije/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dimenzije = await _context.Dimenzije
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dimenzije == null)
            {
                return NotFound();
            }

            return View(dimenzije);
        }

        // GET: Dimenzije/Create
        [Authorize(Roles = "Administrator")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Dimenzije/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,visina,sirina,duzina")] Dimenzije dimenzije)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dimenzije);
                await _context.SaveChangesAsync();
                return RedirectToAction("Create","Artikal");
            }
            return View(dimenzije);
        }

        // GET: Dimenzije/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dimenzije = await _context.Dimenzije.FindAsync(id);
            if (dimenzije == null)
            {
                return NotFound();
            }
            return View(dimenzije);
        }

        // POST: Dimenzije/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,visina,sirina,duzina")] Dimenzije dimenzije)
        {
            if (id != dimenzije.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dimenzije);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DimenzijeExists(dimenzije.Id))
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
            return View(dimenzije);
        }

        // GET: Dimenzije/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dimenzije = await _context.Dimenzije
                .FirstOrDefaultAsync(m => m.Id == id);
            if (dimenzije == null)
            {
                return NotFound();
            }

            return View(dimenzije);
        }

        // POST: Dimenzije/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dimenzije = await _context.Dimenzije.FindAsync(id);
            if (dimenzije != null)
            {
                _context.Dimenzije.Remove(dimenzije);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DimenzijeExists(int id)
        {
            return _context.Dimenzije.Any(e => e.Id == id);
        }
    }
}
