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
    public class AnaSayfaSlidersController : Controller
    {
        private readonly DatabaseContext _context;

        public AnaSayfaSlidersController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/AnaSayfaSliders
        public async Task<IActionResult> Index()
        {
            return View(await _context.AnaSayfaSliderlar.ToListAsync());
        }

        // GET: Admin/AnaSayfaSliders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anaSayfaSlider = await _context.AnaSayfaSliderlar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (anaSayfaSlider == null)
            {
                return NotFound();
            }

            return View(anaSayfaSlider);
        }

        // GET: Admin/AnaSayfaSliders/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/AnaSayfaSliders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AnaSayfaSlider anaSayfaSlider , IFormFile? Resim)
        {
            if (ModelState.IsValid)
            {
                anaSayfaSlider.Resim = await FotoKayitVSilme.FileLoaderAsync(Resim);
                await _context.AddAsync(anaSayfaSlider);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(anaSayfaSlider);
        }

        // GET: Admin/AnaSayfaSliders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anaSayfaSlider = await _context.AnaSayfaSliderlar.FindAsync(id);
            if (anaSayfaSlider == null)
            {
                return NotFound();
            }
            return View(anaSayfaSlider);
        }

        // POST: Admin/AnaSayfaSliders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Resim,Baslik,Aciklama,Link,AktifMi")] AnaSayfaSlider anaSayfaSlider)
        {
            if (id != anaSayfaSlider.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(anaSayfaSlider);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnaSayfaSliderExists(anaSayfaSlider.Id))
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
            return View(anaSayfaSlider);
        }

        // GET: Admin/AnaSayfaSliders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var anaSayfaSlider = await _context.AnaSayfaSliderlar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (anaSayfaSlider == null)
            {
                return NotFound();
            }

            return View(anaSayfaSlider);
        }

        // POST: Admin/AnaSayfaSliders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var anaSayfaSlider = await _context.AnaSayfaSliderlar.FindAsync(id);
            if (anaSayfaSlider != null)
            {
                _context.AnaSayfaSliderlar.Remove(anaSayfaSlider);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnaSayfaSliderExists(int id)
        {
            return _context.AnaSayfaSliderlar.Any(e => e.Id == id);
        }
    }
}
