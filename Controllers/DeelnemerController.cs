using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SamenGamen.Data;
using SamenGamen.Models;

namespace SamenGamen.Controllers
{
    // Opdracht 6
    public static class DeelnemerSessie
    {
        private const string SessionKey = "DeelnemerSessieKey";

        public static Deelnemer GetSessionInfo(ISession session)
        {
            Deelnemer deelnemer;
            
            var value = session.GetString(SessionKey);
            deelnemer = (value != null) ? JsonConvert.DeserializeObject<Deelnemer>(value) : null;
            
            return deelnemer;
        }
        
        public static void SetSessionInfo(ISession session, Deelnemer deelnemer)
        {
            session.SetString(SessionKey, JsonConvert.SerializeObject(deelnemer));
        }
    }
    
    public class DeelnemerController : Controller
    {
        private readonly SamenGamenContext _context;

        public DeelnemerController(SamenGamenContext context)
        {
            _context = context;
        }

        // GET: Deelnemer
        public async Task<IActionResult> Index()
        {
            // Opgave 3, sorteren op naam.
            return View(await _context.Deelnemer.OrderBy(d => d.Naam.ToLower()).ToListAsync());
        }
        
        // GET: Deelnemer/StartSessie/5
        public async Task<IActionResult> StartSessie(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deelnemer = await _context.Deelnemer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deelnemer == null)
            {
                return NotFound();
            }
            
            DeelnemerSessie.SetSessionInfo(HttpContext.Session, deelnemer);

            return RedirectToAction("Overzicht", "Gamevoorstellen");
        }

        // GET: Deelnemer/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deelnemer = await _context.Deelnemer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deelnemer == null)
            {
                return NotFound();
            }

            return View(deelnemer);
        }

        // GET: Deelnemer/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Deelnemer/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Naam")] Deelnemer deelnemer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(deelnemer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(deelnemer);
        }

        // GET: Deelnemer/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deelnemer = await _context.Deelnemer.FindAsync(id);
            if (deelnemer == null)
            {
                return NotFound();
            }
            return View(deelnemer);
        }

        // POST: Deelnemer/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Naam")] Deelnemer deelnemer)
        {
            if (id != deelnemer.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(deelnemer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DeelnemerExists(deelnemer.Id))
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
            return View(deelnemer);
        }

        // GET: Deelnemer/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var deelnemer = await _context.Deelnemer
                .FirstOrDefaultAsync(m => m.Id == id);
            if (deelnemer == null)
            {
                return NotFound();
            }

            return View(deelnemer);
        }

        // POST: Deelnemer/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var deelnemer = await _context.Deelnemer.FindAsync(id);
            _context.Deelnemer.Remove(deelnemer);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DeelnemerExists(int id)
        {
            return _context.Deelnemer.Any(e => e.Id == id);
        }
    }
}
