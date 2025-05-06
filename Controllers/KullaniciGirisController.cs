using E_Ticaret.DataContext;
using E_Ticaret.Models.Entities;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization; //login için
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Net.Mail;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks; //login için

namespace E_Ticaret.Controllers
{
    public class KullaniciGirisController : Controller
    {


        private readonly DatabaseContext _context;

        public KullaniciGirisController(DatabaseContext context)
        {
            _context = context;


        }
        [Authorize]
        //GET: KullaniciGiris
        public IActionResult Index()
        {
            var model = _context.Kullancilar.FirstOrDefault(i => i.KullaniciGuid.ToString() == HttpContext.User.FindFirst("KullaniciGuid").Value);
            if (model is null)
            {
                return NotFound();
            }
            return View(model);

        }
        [HttpPost, Authorize]
        public IActionResult Index(Kullanici kullanici)
        {
            var guid = HttpContext.User.FindFirst("KullaniciGuid")?.Value;
            if (guid == null)
                return Unauthorized();

            var model = _context.Kullancilar.FirstOrDefault(i => i.KullaniciGuid.ToString() == guid);
            if (model is null)
                return NotFound();

            if (ModelState.IsValid)
            {
                model.Adi = kullanici.Adi;
                model.Soyadi = kullanici.Soyadi;
                model.Telefon = kullanici.Telefon;
                model.Adres = kullanici.Adres;
                if (!string.IsNullOrWhiteSpace(kullanici.Sifre))
                {
                    model.Sifre = kullanici.Sifre;
                }



                _context.Update(model);
                var sonuc = _context.SaveChanges();

                if (sonuc > 0)
                {
                    TempData["Mesaj"] = "<div class=\"alert alert-success alert-dismissible fade show\" role=\"alert\">\r\n  <b>Bilgileriniz Güncellenmiştir.</b> <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"Close\"></button>\r\n</div>";
                    return RedirectToAction("Index");
                }

            }

            return View(model);
        }






        public IActionResult GirisYap()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> GirisYapAsync(KayitSayfaModeli kayitSayfaModeli)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var kullanici = await _context.Kullancilar
                        .FirstOrDefaultAsync(x => x.Email == kayitSayfaModeli.Email & x.Sifre == kayitSayfaModeli.Sifre);
                    if (kullanici == null)
                    {
                        ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
                    }
                    else
                    {
                        var claims = new List<Claim>
                        {
                            new Claim(ClaimTypes.Name, kullanici.Adi),
                            new Claim(ClaimTypes.Role, kullanici.AdminMi ? "Admin" : "User"),
                            new Claim(ClaimTypes.Email, kullanici.Email),
                            new Claim("KullaniciId", kullanici.Id.ToString()),
                            new Claim("KullaniciGuid", kullanici.KullaniciGuid.ToString())
                        };
                        var UserIdentity = new ClaimsIdentity(claims, "Login");
                        ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(UserIdentity);
                        await HttpContext.SignInAsync(claimsPrincipal);

                        /*if (kullanici.AdminMi)
                        {
                            return RedirectToAction("Index", "Main", new { area = "Admin" });
                        }
                        */

                        return Redirect(string.IsNullOrEmpty(kayitSayfaModeli.GeriDonusUrl) ? "/" : kayitSayfaModeli.GeriDonusUrl); 

                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Kullanıcı adı veya şifre hatalı");
                    return View(kayitSayfaModeli);
                }
            }
            return View(kayitSayfaModeli);

        }




        public IActionResult KayitOl()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> KayitOlAsync(Kullanici kullanici)
        {
            kullanici.AdminMi = false;
            kullanici.AktifMi = true;

            if (ModelState.IsValid)
            {
                await _context.AddAsync(kullanici);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(kullanici);

        }
        public async Task<IActionResult> CikisYap()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("GirisYap");
        }


        [Authorize]
        public async Task<IActionResult> Siparislerim()
        {
            var userGuidStr = HttpContext.User.FindFirst("KullaniciGuid")?.Value;
            if (string.IsNullOrEmpty(userGuidStr))
                return Unauthorized();

            var userGuid = Guid.Parse(userGuidStr);
            var kullanici = await _context.Kullancilar
                .FirstOrDefaultAsync(k => k.KullaniciGuid == userGuid);

            if (kullanici == null)
                return NotFound("Kullanıcı bulunamadı.");

            var siparisler = await _context.Siparisler
                .Include(s => s.SiparisDetaylari)
                .ThenInclude(d => d.Urun)
                .Where(s => s.MusteriNumarasi == kullanici.KullaniciGuid.ToString())
                .OrderByDescending(s => s.SiparisTarihi)
                .ToListAsync();

            return View(siparisler);
        }

        [Authorize]
        public async Task<IActionResult> SiparisDetay(int id)
        {
            var userGuidStr = HttpContext.User.FindFirst("KullaniciGuid")?.Value;
            if (string.IsNullOrEmpty(userGuidStr)) return Unauthorized();

            var siparis = await _context.Siparisler
                .Include(s => s.SiparisDetaylari)
                    .ThenInclude(sd => sd.Urun)
                .FirstOrDefaultAsync(s => s.Id == id && s.MusteriNumarasi == userGuidStr);

            if (siparis == null)
                return NotFound("Sipariş bulunamadı veya erişiminiz yok.");

            return View(siparis);
        }

        // DEĞERLENDİRME KISIMLARI

        [Authorize]
        public async Task<IActionResult> DegerlendirmeyeAcilanlar()
        {
            var guid = HttpContext.User.FindFirst("KullaniciGuid")?.Value;
            if (guid == null) return Unauthorized();

            var kullanici = await _context.Kullancilar.FirstOrDefaultAsync(k => k.KullaniciGuid.ToString() == guid);
            if (kullanici == null) return NotFound();

            // Sadece tamamlanmış siparişlerin detaylarını getir
            var detaylar = await _context.SiparisDetaylar
    .Include(x => x.Urun)
        .ThenInclude(u => u.Yorumlar) // Yorumlar ilişkilensin
    .Include(x => x.Siparis)
    .Where(x => x.Siparis.KullaniciId == kullanici.Id && x.Siparis.SiparisDurumu == EnumSiparisDurumu.Waiting)
    .ToListAsync();



            return View(detaylar); // View henüz yazılmadı, sıradaki adım
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> YorumYap(int siparisDetayId)
        {
            var detay = await _context.SiparisDetaylar
                .Include(x => x.Urun)
                .FirstOrDefaultAsync(x => x.Id == siparisDetayId);

            if (detay == null)
                return NotFound();

            var yorum = new Yorum
            {
                SiparisDetayId = detay.Id,
                SiparisDetay = detay,
                UrunId = detay.Urun?.Id // 👈 BU satırı ekle!
            };


            return View(yorum);
        }


        [Authorize]
        [HttpPost]
        public async Task<IActionResult> YorumYap(Yorum yorum)
        {
            if (!ModelState.IsValid)
                return View(yorum);

            var guidStr = HttpContext.User.FindFirst("KullaniciGuid")?.Value;
            var kullanici = await _context.Kullancilar.FirstOrDefaultAsync(k => k.KullaniciGuid.ToString() == guidStr);

            if (kullanici == null)
                return Unauthorized();

            yorum.KullaniciId = kullanici.Id;
            yorum.Tarih = DateTime.Now;

            _context.Yorumlar.Add(yorum);
            await _context.SaveChangesAsync();

            TempData["Mesaj"] = "Yorumunuz başarıyla kaydedildi!";
            return RedirectToAction("DegerlendirmeyeAcilanlar");
        }


        [Authorize]
        [HttpGet]
        public async Task<IActionResult> YorumDuzenle(int id)
        {
            var yorum = await _context.Yorumlar
                .Include(y => y.SiparisDetay)
                    .ThenInclude(sd => sd.Urun)
                .FirstOrDefaultAsync(y => y.Id == id);

            if (yorum == null)
                return NotFound();

            return View(yorum);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> YorumDuzenle(Yorum yorum)
        {
            if (!ModelState.IsValid)
                return View(yorum);

            var mevcutYorum = await _context.Yorumlar.FindAsync(yorum.Id);
            if (mevcutYorum == null)
                return NotFound();

            mevcutYorum.Puan = yorum.Puan;
            mevcutYorum.YorumMetni = yorum.YorumMetni;
            mevcutYorum.Tarih = DateTime.Now;

            await _context.SaveChangesAsync();
            TempData["Mesaj"] = "Yorum başarıyla güncellendi!";
            return RedirectToAction("DegerlendirmeyeAcilanlar");
        }



    }
}
