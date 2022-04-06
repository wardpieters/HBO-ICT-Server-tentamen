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
    public class GamevoorstellenController : Controller
    {
        private readonly SamenGamenContext _context;

        public GamevoorstellenController(SamenGamenContext context)
        {
            _context = context;
        }

        // GET: Gamevoorstellen
        public async Task<IActionResult> Index()
        {
            return View(await _context.Gamevoorstel.ToListAsync());
        }
        
        // GET: Gamevoorstellen/Overzicht
        public async Task<IActionResult> Overzicht()
        {
            ViewBag.SessieData = DeelnemerSessie.GetSessionInfo(HttpContext.Session);
            
            return View(await _context.Gamevoorstel.Include(x => x.Aanmeldingen).ToListAsync());
        }
        
        // GET: Gamevoorstellen/AddCurrentPlayerToGame
        public async Task<IActionResult> AddCurrentPlayerToGame(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Overzicht");
            }
            
            var sessionInfo = DeelnemerSessie.GetSessionInfo(HttpContext.Session);

            if (sessionInfo != null)
            {
                if (_context.Aanmelding.FirstOrDefault(x => x.DeelnemerId.Equals(sessionInfo.Id) && x.GamevoorstelId.Equals(id)) != null)
                {
                    // Er bestaat al een aanmelding met deze deelnemer en gamevoorstel.
                    return RedirectToAction("Overzicht");
                }
                
                _context.Aanmelding.Add(new Aanmelding
                {
                    Aanmelddatum = DateTime.Now,
                    DeelnemerId = sessionInfo.Id,
                    GamevoorstelId = (int) id
                });

                await _context.SaveChangesAsync();
            }

            return RedirectToAction("Overzicht");
        }

        // GET: Gamevoorstellen/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamevoorstel = await _context.Gamevoorstel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gamevoorstel == null)
            {
                return NotFound();
            }

            return View(gamevoorstel);
        }

        // GET: Gamevoorstellen/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Gamevoorstellen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Beschrijving")] Gamevoorstel gamevoorstel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gamevoorstel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(gamevoorstel);
        }

        // GET: Gamevoorstellen/Create
        public IActionResult CreateSpecial()
        {
            return View();
        }

        // POST: Gamevoorstellen/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateSpecial([Bind("Id,Beschrijving")] Gamevoorstel gamevoorstel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(gamevoorstel);
                await _context.SaveChangesAsync();
                return RedirectToAction("Overzicht");
            }
            return RedirectToAction("Overzicht");
        }

        // GET: Gamevoorstellen/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamevoorstel = await _context.Gamevoorstel.FindAsync(id);
            if (gamevoorstel == null)
            {
                return NotFound();
            }
            return View(gamevoorstel);
        }

        // POST: Gamevoorstellen/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Beschrijving")] Gamevoorstel gamevoorstel)
        {
            if (id != gamevoorstel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(gamevoorstel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GamevoorstelExists(gamevoorstel.Id))
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
            return View(gamevoorstel);
        }

        // GET: Gamevoorstellen/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var gamevoorstel = await _context.Gamevoorstel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (gamevoorstel == null)
            {
                return NotFound();
            }

            return View(gamevoorstel);
        }

        // POST: Gamevoorstellen/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var gamevoorstel = await _context.Gamevoorstel.FindAsync(id);
            _context.Gamevoorstel.Remove(gamevoorstel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GamevoorstelExists(int id)
        {
            return _context.Gamevoorstel.Any(e => e.Id == id);
        }
    }
}
