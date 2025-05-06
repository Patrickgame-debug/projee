using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using E_Ticaret.DataContext;
using E_Ticaret.Models.Entities;

namespace E_Ticaret.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class YorumlarController : Controller
    {
        private readonly DatabaseContext _context;

        public YorumlarController(DatabaseContext context)
        {
            _context = context;
        }

        // GET: Admin/Yorumlar
        public async Task<IActionResult> Index()
        {
            var databaseContext = _context.Yorumlar.Include(y => y.Kullanici).Include(y => y.SiparisDetay).Include(y => y.Urun);
            return View(await databaseContext.ToListAsync());
        }

        // GET: Admin/Yorumlar/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yorum = await _context.Yorumlar
                .Include(y => y.Kullanici)
                .Include(y => y.SiparisDetay)
                .Include(y => y.Urun)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (yorum == null)
            {
                return NotFound();
            }

            return View(yorum);
        }

        // GET: Admin/Yorumlar/Create
        public IActionResult Create()
        {
            ViewData["KullaniciId"] = new SelectList(_context.Kullancilar, "Id", "Adi");
            ViewData["SiparisDetayId"] = new SelectList(_context.SiparisDetaylar, "Id", "Id");
            ViewData["UrunId"] = new SelectList(_context.Urunler, "Id", "UrunAdi");
            return View();
        }

        // POST: Admin/Yorumlar/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,SiparisDetayId,KullaniciId,Puan,YorumMetni,Tarih,UrunId")] Yorum yorum)
        {
            if (ModelState.IsValid)
            {
                _context.Add(yorum);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["KullaniciId"] = new SelectList(_context.Kullancilar, "Id", "Adi", yorum.KullaniciId);
            ViewData["SiparisDetayId"] = new SelectList(_context.SiparisDetaylar, "Id", "Id", yorum.SiparisDetayId);
            ViewData["UrunId"] = new SelectList(_context.Urunler, "Id", "UrunAdi", yorum.UrunId);
            return View(yorum);
        }

        // GET: Admin/Yorumlar/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yorum = await _context.Yorumlar.FindAsync(id);
            if (yorum == null)
            {
                return NotFound();
            }
            ViewData["KullaniciId"] = new SelectList(_context.Kullancilar, "Id", "Adi", yorum.KullaniciId);
            ViewData["SiparisDetayId"] = new SelectList(_context.SiparisDetaylar, "Id", "Id", yorum.SiparisDetayId);
            ViewData["UrunId"] = new SelectList(_context.Urunler, "Id", "UrunAdi", yorum.UrunId);
            return View(yorum);
        }

        // POST: Admin/Yorumlar/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,SiparisDetayId,KullaniciId,Puan,YorumMetni,Tarih,UrunId")] Yorum yorum)
        {
            if (id != yorum.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(yorum);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!YorumExists(yorum.Id))
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
            ViewData["KullaniciId"] = new SelectList(_context.Kullancilar, "Id", "Adi", yorum.KullaniciId);
            ViewData["SiparisDetayId"] = new SelectList(_context.SiparisDetaylar, "Id", "Id", yorum.SiparisDetayId);
            ViewData["UrunId"] = new SelectList(_context.Urunler, "Id", "UrunAdi", yorum.UrunId);
            return View(yorum);
        }

        // GET: Admin/Yorumlar/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var yorum = await _context.Yorumlar
                .Include(y => y.Kullanici)
                .Include(y => y.SiparisDetay)
                .Include(y => y.Urun)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (yorum == null)
            {
                return NotFound();
            }

            return View(yorum);
        }

        // POST: Admin/Yorumlar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var yorum = await _context.Yorumlar.FindAsync(id);
            if (yorum != null)
            {
                _context.Yorumlar.Remove(yorum);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool YorumExists(int id)
        {
            return _context.Yorumlar.Any(e => e.Id == id);
        }
    }
}
