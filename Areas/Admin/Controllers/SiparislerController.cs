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
using System.ComponentModel.DataAnnotations;

namespace E_Ticaret.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class SiparislerController : Controller
    {
        private readonly DatabaseContext _context;

        public SiparislerController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/Siparisler
        public async Task<IActionResult> Index()
        {
            return View(await _context.Siparisler
                .Include(m => m.Kullanici)
                .Include(s => s.SiparisDetaylari)
                    .ThenInclude(d => d.Urun)
                .ToListAsync());
        }

        // GET: Admin/Siparisler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siparis = await _context.Siparisler
                .Include(m => m.Kullanici)
                .Include(s => s.SiparisDetaylari)
                    .ThenInclude(d => d.Urun)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (siparis == null)
            {
                return NotFound();
            }

            return View(siparis);
        }

        // GET: Admin/Siparisler/Create
        public IActionResult Create()
        {
            ViewData["KullaniciId"] = new SelectList(_context.Kullancilar, "Id", "Email");
            return View();
        }

        // POST: Admin/Siparisler/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Siparis siparis)
        {
            if (ModelState.IsValid)
            {
                _context.Add(siparis);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KullaniciId"] = new SelectList(_context.Kullancilar, "Id", "Email", siparis.KullaniciId);
            return View(siparis);
        }

        // GET: Admin/Siparisler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var siparis = await _context.Siparisler
                .Include(s => s.Kullanici)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (siparis == null)
                return NotFound();

            ViewBag.SiparisDurumuları = Enum.GetValues(typeof(EnumSiparisDurumu))
                .Cast<EnumSiparisDurumu>()
                .Select(e => new SelectListItem
                {
                    Text = e.GetType().GetMember(e.ToString()).First()
                        .GetCustomAttributes(typeof(DisplayAttribute), false)
                        .Cast<DisplayAttribute>()
                        .FirstOrDefault()?.Name ?? e.ToString(),
                    Value = e.ToString(),
                    Selected = e == siparis.SiparisDurumu
                }).ToList();

            return View(siparis);
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Siparis siparis)
        {
            if (id != siparis.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(siparis);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.Siparisler.Any(e => e.Id == siparis.Id))
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

            ViewData["KullaniciId"] = new SelectList(_context.Kullancilar, "Id", "Email", siparis.KullaniciId);
            return View(siparis);
        }


        // GET: Admin/Siparisler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var siparis = await _context.Siparisler
                .Include(m => m.Kullanici)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (siparis == null)
            {
                return NotFound();
            }

            return View(siparis);
        }

        // POST: Admin/Siparisler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var siparis = await _context.Siparisler.FindAsync(id);
            if (siparis != null)
            {
                _context.Siparisler.Remove(siparis);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SiparisExists(int id)
        {
            return _context.Siparisler.Any(e => e.Id == id);
        }
    }
}
