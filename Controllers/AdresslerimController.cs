using E_Ticaret.DataContext;
using E_Ticaret.Models.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace E_Ticaret.Controllers
{
    [Authorize]
    public class AdresslerimController : Controller
    {
        private readonly DatabaseContext _context;

        public AdresslerimController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var userGuidStr = HttpContext.User.FindFirst("KullaniciGuid")?.Value;
            if (string.IsNullOrEmpty(userGuidStr))
                return NotFound("Giriş yapmış kullanıcı bulunamadı!");

            var userGuid = Guid.Parse(userGuidStr);

            var kullanici = await _context.Kullancilar
                .Include(k => k.Adresler)
                .FirstOrDefaultAsync(k => k.KullaniciGuid == userGuid);

            if (kullanici == null)
                return NotFound("Kullanıcı bulunamadı!");

            var adresler = kullanici.Adresler?.Where(x => x.AktifMi).ToList() ?? new List<Adress>();
            return View(adresler);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Adress adres)
        {
            try
            {
                var userGuidStr = HttpContext.User.FindFirst("KullaniciGuid")?.Value;
                if (string.IsNullOrEmpty(userGuidStr))
                    return Unauthorized("Kullanıcı bilgisi alınamadı");

                var userGuid = Guid.Parse(userGuidStr);
                var kullanici = await _context.Kullancilar.FirstOrDefaultAsync(x => x.KullaniciGuid == userGuid);
                if (kullanici == null)
                    return NotFound("Kullanıcı bulunamadı");

                if (ModelState.IsValid)
                {
                    adres.KullaniciId = kullanici.Id;
                    adres.KayitTarihi = DateTime.Now;
                    adres.AktifMi = true;

                    _context.Adresler.Add(adres);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index");
                }

                return View(adres);
            }
            catch (Exception ex)
            {
                // Hataları yakala ve göster
                ViewBag.Hata = "Bir hata oluştu: " + ex.Message;
                return View(adres);
            }
        }


        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var adres = await _context.Adresler.FindAsync(id);
            if (adres == null) return NotFound();
            return View(adres);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var adres = await _context.Adresler.FindAsync(id);
            if (adres == null) return NotFound();

            _context.Adresler.Remove(adres);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var adres = await _context.Adresler.FindAsync(id);
            if (adres == null) return NotFound();
            return View(adres);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Adress guncelAdres)
        {
            if (id != guncelAdres.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var adres = await _context.Adresler.FindAsync(id);
                if (adres == null) return NotFound();

                adres.AdresBasligi = guncelAdres.AdresBasligi;
                adres.Sehir = guncelAdres.Sehir;
                adres.Ilce = guncelAdres.Ilce;
                adres.AcikAdres = guncelAdres.AcikAdres;
                adres.FaturaAdres = guncelAdres.FaturaAdres;
                adres.TeslimatAdres = guncelAdres.TeslimatAdres;

                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(guncelAdres);
        }

        public async Task<IActionResult> CikisYap()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("GirisYap");
        }

    }
}
