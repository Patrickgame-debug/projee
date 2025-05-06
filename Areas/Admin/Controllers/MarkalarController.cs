using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Ticaret.DataContext;
using E_Ticaret.Models.Entities;
using E_Ticaret.FotoKayit;
using Microsoft.AspNetCore.Authorization;

namespace E_Ticaret.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class MarkalarController : Controller
    {
        private readonly DatabaseContext _context;

        public MarkalarController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/Markalar
        public async Task<IActionResult> Index()
        {
            return View(await _context.Markalar.ToListAsync());
        }

        // GET: Admin/Markalar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marka = await _context.Markalar.FirstOrDefaultAsync(m => m.Id == id);
            if (marka == null)
            {
                return NotFound();
            }

            return View(marka);
        }

        // GET: Admin/Markalar/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/Markalar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Marka marka, IFormFile? Resim)
        {
            if (ModelState.IsValid)
            {
                marka.Resim = await FotoKayitVSilme.FileLoaderAsync(Resim);
                await _context.AddAsync(marka);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(marka);
        }

        // GET: Admin/Markalar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marka = await _context.Markalar.FindAsync(id);
            marka.GuncellemeTarihi = DateTime.Now;
            if (marka == null)
            {
                return NotFound();
            }
            return View(marka);
        }

        // POST: Admin/Markalar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Marka marka, IFormFile? Resim)
        {
            if (id != marka.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (Resim is not null)
                        marka.Resim = await FotoKayitVSilme.FileLoaderAsync(Resim);
                    _context.Update(marka);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MarkaExists(marka.Id))
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
            return View(marka);
        }

        // GET: Admin/Markalar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var marka = await _context.Markalar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (marka == null)
            {
                return NotFound();
            }

            return View(marka);
        }

        // POST: Admin/Markalar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var marka = await _context.Markalar.FindAsync(id);
            if (!string.IsNullOrEmpty(marka.Resim))
            {
                // Resim dosyasını silme işlemi
                FotoKayitVSilme.FileDelete(marka.Resim);
            }

            if (marka != null)

            {
              
                if(!string.IsNullOrEmpty(marka.Resim))
                {
                    FotoKayitVSilme.FileDelete(marka.Resim);
                }
                _context.Markalar.Remove(marka);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MarkaExists(int id)
        {
            return _context.Markalar.Any(e => e.Id == id);
        }
    }
}