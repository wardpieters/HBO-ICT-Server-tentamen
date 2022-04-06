using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SamenGamen.Data;
using SamenGamen.Models;

namespace SamenGamen.Controllers
{
    public class AanmeldingenController : Controller
    {
        private readonly SamenGamenContext _context;

        public AanmeldingenController(SamenGamenContext context)
        {
            _context = context;
        }

        // GET: Aanmeldingen
        public async Task<IActionResult> Index()
        {
            var samenGamenContext = _context.Aanmelding.Include(a => a.Deelnemer).Include(a => a.Gamevoorstel);
            return View(await samenGamenContext.ToListAsync());
        }

        // GET: Aanmeldingen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aanmelding = await _context.Aanmelding
                .Include(a => a.Deelnemer)
                .Include(a => a.Gamevoorstel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aanmelding == null)
            {
                return NotFound();
            }

            return View(aanmelding);
        }

        // GET: Aanmeldingen/Create
        public IActionResult Create()
        {
            ViewData["DeelnemerId"] = new SelectList(_context.Deelnemer, "Id", "Naam");
            ViewData["GamevoorstelId"] = new SelectList(_context.Gamevoorstel, "Id", "Beschrijving");
            return View();
        }

        // POST: Aanmeldingen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Aanmelddatum,DeelnemerId,GamevoorstelId")] Aanmelding aanmelding)
        {
            if (ModelState.IsValid)
            {
                _context.Add(aanmelding);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DeelnemerId"] = new SelectList(_context.Deelnemer, "Id", "Naam", aanmelding.DeelnemerId);
            ViewData["GamevoorstelId"] = new SelectList(_context.Gamevoorstel, "Id", "Id", aanmelding.GamevoorstelId);
            return View(aanmelding);
        }

        // GET: Aanmeldingen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aanmelding = await _context.Aanmelding.FindAsync(id);
            if (aanmelding == null)
            {
                return NotFound();
            }
            ViewData["DeelnemerId"] = new SelectList(_context.Deelnemer, "Id", "Naam", aanmelding.DeelnemerId);
            ViewData["GamevoorstelId"] = new SelectList(_context.Gamevoorstel, "Id", "Id", aanmelding.GamevoorstelId);
            return View(aanmelding);
        }

        // POST: Aanmeldingen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Aanmelddatum,DeelnemerId,GamevoorstelId")] Aanmelding aanmelding)
        {
            if (id != aanmelding.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(aanmelding);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AanmeldingExists(aanmelding.Id))
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
            ViewData["DeelnemerId"] = new SelectList(_context.Deelnemer, "Id", "Naam", aanmelding.DeelnemerId);
            ViewData["GamevoorstelId"] = new SelectList(_context.Gamevoorstel, "Id", "Id", aanmelding.GamevoorstelId);
            return View(aanmelding);
        }

        // GET: Aanmeldingen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var aanmelding = await _context.Aanmelding
                .Include(a => a.Deelnemer)
                .Include(a => a.Gamevoorstel)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (aanmelding == null)
            {
                return NotFound();
            }

            return View(aanmelding);
        }

        // POST: Aanmeldingen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var aanmelding = await _context.Aanmelding.FindAsync(id);
            _context.Aanmelding.Remove(aanmelding);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AanmeldingExists(int id)
        {
            return _context.Aanmelding.Any(e => e.Id == id);
        }
    }
}
