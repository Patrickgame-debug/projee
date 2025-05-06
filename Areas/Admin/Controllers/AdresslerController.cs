using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Ticaret.DataContext;
using E_Ticaret.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace E_Ticaret.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class AdresslerController : Controller
    {
        private readonly DatabaseContext _context;

        public AdresslerController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/Adressler
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.Adresler.Include(a => a.Kullanici);
            return View(await databaseContext.ToListAsync());
        }

        // GET: Admin/Adressler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adress = await _context.Adresler
                .Include(a => a.Kullanici)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adress == null)
            {
                return NotFound();
            }

            return View(adress);
        }

        // GET: Admin/Adressler/Create
        public IActionResult Create()
        {
            ViewData["KullaniciId"] = new SelectList(_context.Kullancilar, "Id", "Email");
            return View();
        }

        // POST: Admin/Adressler/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Adress adress)
        {
            if (ModelState.IsValid)
            {
                _context.Add(adress);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KullaniciId"] = new SelectList(_context.Kullancilar, "Id", "Email", adress.KullaniciId);
            return View(adress);
        }

        // GET: Admin/Adressler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adress = await _context.Adresler.FindAsync(id);
            if (adress == null)
            {
                return NotFound();
            }
            ViewData["KullaniciId"] = new SelectList(_context.Kullancilar, "Id", "Email", adress.KullaniciId);
            return View(adress);
        }

        // POST: Admin/Adressler/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Adress adress)
        {
            if (id != adress.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(adress);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdressExists(adress.Id))
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
            ViewData["KullaniciId"] = new SelectList(_context.Kullancilar, "Id", "Email", adress.KullaniciId);
            return View(adress);
        }

        // GET: Admin/Adressler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var adress = await _context.Adresler
                .Include(a => a.Kullanici)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (adress == null)
            {
                return NotFound();
            }

            return View(adress);
        }

        // POST: Admin/Adressler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adress = await _context.Adresler.FindAsync(id);
            if (adress != null)
            {
                _context.Adresler.Remove(adress);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdressExists(int id)
        {
            return _context.Adresler.Any(e => e.Id == id);
        }
    }
}
