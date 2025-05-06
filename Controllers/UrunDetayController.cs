using E_Ticaret.Areas.Admin.Controllers;
using E_Ticaret.DataContext;
using E_Ticaret.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret.Controllers
{
    // bu kısım  urunlerimizin olduğu ve arama motorunundan arattığımızda cıkan ürünler  
    public class UrunDetayController : Controller
    {
        private readonly DatabaseContext _context;

        public UrunDetayController(DatabaseContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index(string arama="")
        {
            // arama kısmı  arama yaparken  arama kısmına yazdığımız kelimeyi alır ve o kelimeyi içeren ürünleri getirir daha fazla ayar icin i.UrunAdi.Contains(arama) buradan devam et mesela || i.Description.Contains(arama9
            var databaseContext = _context.Urunler.Where(i => i.AktifMi &&  i.UrunAdi.Contains(arama)).Include(u => u.Kategori).Include(u => u.Marka);
            return View(await databaseContext.ToListAsync());
        }



        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var urun = await _context.Urunler
                .Include(u => u.Kategori)
                .Include(u => u.Marka)
                .Include(u => u.Yorumlar)
                    .ThenInclude(y => y.Kullanici)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (urun == null)
                return NotFound();

            var yorumlar = urun.Yorumlar?
                .OrderByDescending(y => y.Tarih)
                .ToList() ?? new();

            var ortalamaPuan = yorumlar.Any() ? yorumlar.Average(y => y.Puan ?? 0) : 0;
            var toplamYorum = yorumlar.Count;

            var puanDagilimi = yorumlar
                .GroupBy(y => y.Puan ?? 0)
                .ToDictionary(g => g.Key, g => g.Count());

            var model = new UrunDetayModeli()
            {
                urun = urun,
                ilgiliurunler = await _context.Urunler
                    .Where(i => i.KategoriId == urun.KategoriId && i.Id != urun.Id && i.AktifMi)
                    .ToListAsync(),
                yorumlar = yorumlar,
                ortalamaPuan = ortalamaPuan,
                toplamYorum = toplamYorum,
                puanDagilimi = puanDagilimi
            };

            return View(model);
        }

    }
}