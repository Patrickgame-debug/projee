using E_Ticaret.DataContext;
using E_Ticaret.Models.Entities;
using E_Ticaret.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret.Controllers
{
    public class UrunDetayController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly YorumModerasyonService _moderasyonService;

        public UrunDetayController(DatabaseContext context, YorumModerasyonService moderasyonService)
        {
            _context = context;
            _moderasyonService = moderasyonService;
        }

        public async Task<IActionResult> Index(string arama = "")
        {
            var databaseContext = _context.Urunler
                .Where(i => i.AktifMi && i.UrunAdi.Contains(arama))
                .Include(u => u.Kategori)
                .Include(u => u.Marka);

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

            // 💬 Yorumlar üzerinde moderasyon kontrolü yap
            foreach (var yorum in urun.Yorumlar.Where(y => y.OnaylandiMi))
            {
                var olumsuzMu = await _moderasyonService.YorumOlumsuzMu(yorum.YorumMetni);
                if (olumsuzMu)
                {
                    yorum.OnaylandiMi = false; // yayından kaldır
                    _context.Update(yorum);    // değişikliği veritabanına kaydet
                }
            }

            await _context.SaveChangesAsync();

            // 🔎 Sadece onaylanan yorumları listele
            var yorumlar = urun.Yorumlar
                .Where(y => y.OnaylandiMi)
                .OrderByDescending(y => y.Tarih)
                .ToList();


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
