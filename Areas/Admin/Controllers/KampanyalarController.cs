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
    public class KampanyalarController : Controller
    {
        private readonly DatabaseContext _context;

        public KampanyalarController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/Kampanyalar
        public async Task<IActionResult> Index()
        {
            return View(await _context.Kampanyalar.ToListAsync());
        }

        // GET: Admin/Kampanyalar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kampanya = await _context.Kampanyalar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kampanya == null)
            {
                return NotFound();
            }

            return View(kampanya);
        }

        // GET: Admin/Kampanyalar/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: Admin/Kampanyalar/Create
       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Kampanya kampanya ,IFormFile? Resim)
        {

            if (ModelState.IsValid)
            {
                kampanya.Resim = await FotoKayitVSilme.FileLoaderAsync(Resim);
                await _context.AddAsync(kampanya);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine(error.ErrorMessage);
                }
            }


            return View(kampanya);
        }

        // GET: Admin/Kampanyalar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kampanya = await _context.Kampanyalar.FindAsync(id);
            if (kampanya == null)
            {
                return NotFound();
            }
            return View(kampanya);
        }

        // POST: Admin/Kampanyalar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Kampanya kampanya)
        {
            if (id != kampanya.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(kampanya);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!KampanyaExists(kampanya.Id))
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
            return View(kampanya);
        }

        // GET: Admin/Kampanyalar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kampanya = await _context.Kampanyalar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kampanya == null)
            {
                return NotFound();
            }

            return View(kampanya);
        }

        // POST: Admin/Kampanyalar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var kampanya = await _context.Kampanyalar.FindAsync(id);
            if (kampanya != null)
            {
                if (!string.IsNullOrEmpty(kampanya.Resim))
                {
                    FotoKayitVSilme.FileDelete(kampanya.Resim);
                }
                _context.Kampanyalar.Remove(kampanya);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool KampanyaExists(int id)
        {
            return _context.Kampanyalar.Any(e => e.Id == id);
        }
    }
}
