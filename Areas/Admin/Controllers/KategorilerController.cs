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
    public class KategorilerController : Controller
    {
        private readonly DatabaseContext _context;

        public KategorilerController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/Kategoriler
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kategoriler.ToListAsync());
        }

        // GET: Admin/Kategoriler/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategori = await _context.Kategoriler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kategori == null)
            {
                return NotFound();
            }

            return View(kategori);
        }

        // GET: Admin/Kategoriler/Create
        public IActionResult Create()
        {
            ViewBag.Kategoriler= new SelectList(_context.Kategoriler, "Id", "KategoriAdi");
            return View();
        }

        // POST: Admin/Kategoriler/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Kategori kategori, IFormFile? Resim)
        {
            if (ModelState.IsValid)
            {
                kategori.Resim = await FotoKayitVSilme.FileLoaderAsync(Resim);
                await _context.AddAsync(kategori);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Kategoriler = new SelectList(_context.Kategoriler, "Id", "KategoriAdi");
            return View(kategori);
        }

        // GET: Admin/Kategoriler/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategori = await _context.Kategoriler.FindAsync(id);
            if (kategori == null)
            {
                return NotFound();
            }
            ViewBag.Kategoriler = new SelectList(_context.Kategoriler, "Id", "KategoriAdi");
            return View(kategori);
        }

        // POST: Admin/Kategoriler/Edit/5
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,Kategori kategori, IFormFile? Resim)
        {
            if (id != kategori.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    
                    if (Resim is not null)
                        kategori.Resim = await FotoKayitVSilme.FileLoaderAsync(Resim);
                    kategori.GuncellemeTarihi = DateTime.Now;
                    _context.Update(kategori);

                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KategoriExists(kategori.Id))
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
            ViewBag.Kategoriler = new SelectList(_context.Kategoriler, "Id", "KategoriAdi");
            return View(kategori);
        }

        // GET: Admin/Kategoriler/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kategori = await _context.Kategoriler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kategori == null)
            {
                return NotFound();
            }

            return View(kategori);
        }

        // POST: Admin/Kategoriler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kategori = await _context.Kategoriler.FindAsync(id);
            if (!string.IsNullOrEmpty(kategori.Resim))
            {
                // Resim dosyasını silme işlemi
                FotoKayitVSilme.FileDelete(kategori.Resim);
            }
           
           
            if (kategori != null)
            {
                _context.Kategoriler.Remove(kategori);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KategoriExists(int id)
        {
            return _context.Kategoriler.Any(e => e.Id == id);
        }
    }
}
