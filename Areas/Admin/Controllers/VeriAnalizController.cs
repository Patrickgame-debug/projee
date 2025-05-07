using E_Ticaret.Models.Entities;
using E_Ticaret.Services;
using Microsoft.AspNetCore.Mvc;
using E_Ticaret.DataContext;
using Microsoft.EntityFrameworkCore;


namespace E_Ticaret.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class VeriAnalizController : Controller
    {
        private readonly AdminVeriAnalizService _veriAnalizService;
        private readonly DatabaseContext _context;
        public async Task<IActionResult> Index()
        {
            var liste = await _context.Kullancilar
         .Where(k => !k.AdminMi) // Sadece müşteri olanlar
         .Select(k => new KullaniciHarcamaModel
         {
             AdSoyad = k.Adi + " " + k.Soyadi,
             KayitTarihi = k.KayitTarihi,
             ToplamHarcama = k.Siparisler != null ? k.Siparisler.Sum(s => s.ToplamTutar) : 0
         })
         .ToListAsync();

            return View(liste);
        }


        public VeriAnalizController(AdminVeriAnalizService veriAnalizService, DatabaseContext context)
        {
            _veriAnalizService = veriAnalizService;
            _context = context;
        }


        public async Task<IActionResult> MusteriDetay()
        {
            var toplamMusteri = await _context.Kullancilar.CountAsync();

            var yediGunOncesi = DateTime.Today.AddDays(-7);
            var yeniKayitlar = await _context.Kullancilar
                .Where(k => k.KayitTarihi >= yediGunOncesi)
                .ToListAsync();

            var topMusteriler = await _context.Siparisler
                .Include(s => s.Kullanici)
                .GroupBy(s => s.KullaniciId)
                .Select(g => new
                {
                    Kullanici = g.First().Kullanici,
                    SiparisSayisi = g.Count(),
                    ToplamTutar = g.Sum(x => x.ToplamTutar)
                })
                .OrderByDescending(x => x.SiparisSayisi)
                .Take(10)
                .ToListAsync();

            ViewBag.ToplamMusteri = toplamMusteri;
            ViewBag.YeniMusteriler = yeniKayitlar;
            ViewBag.TopMusteriler = topMusteriler;

            return View();
        }




        public async Task<IActionResult> SatisDetay()
        {
            var siparisler = await _context.Siparisler.ToListAsync();

            var toplamSatis = siparisler.Count;
            var toplamTutar = siparisler.Sum(s => s.ToplamTutar);
            var ortalama = toplamSatis > 0 ? toplamTutar / toplamSatis : 0;

            var model = new
            {
                ToplamSatis = toplamSatis,
                ToplamTutar = toplamTutar,
                OrtalamaTutar = ortalama
            };

            return View(model);
        }

        public async Task<IActionResult> UrunDetay()
        {
            var grup = await _context.SiparisDetaylar
                .Include(sd => sd.Urun)
                .ThenInclude(u => u.Kategori)
                .Where(sd => sd.Urun != null && sd.Urun.Kategori != null)
                .GroupBy(sd => sd.Urun.Kategori.KategoriAdi)
                .ToListAsync();

            var liste = new List<KategoriUrunSatisModel>();

            foreach (var kategoriGroup in grup)
            {
                var urunGrup = kategoriGroup
                    .GroupBy(sd => sd.Urun.UrunAdi)
                    .Select(g => new
                    {
                        UrunAdi = g.Key,
                        SatisAdedi = g.Count()
                    })
                    .ToList();

                if (urunGrup.Count > 0)
                {
                    var enCok = urunGrup.OrderByDescending(x => x.SatisAdedi).First();
                    var enAz = urunGrup.OrderBy(x => x.SatisAdedi).First();

                    liste.Add(new KategoriUrunSatisModel
                    {
                        KategoriAdi = kategoriGroup.Key,
                        EnCokSatanUrun = enCok.UrunAdi,
                        EnCokSatisAdedi = enCok.SatisAdedi,
                        EnAzSatanUrun = enAz.UrunAdi,
                        EnAzSatisAdedi = enAz.SatisAdedi
                    });
                }
            }

            return View(liste);
        }


        // ... (diğer methodlar en üste gelecek şekilde yerli yerinde kalsın)

        public async Task<IActionResult> GenelUrunOnerisi()
        {
            var gptMetni = await _veriAnalizService.GetGenelSatisOnerisiAsync();

            ViewBag.GenelOneri = gptMetni;
            return View();
        }







        public async Task<IActionResult> KampanyaOneri()
        {
            var gptOneri = await _veriAnalizService.GetKampanyaOnerisiAsync();
            ViewBag.Oneri = gptOneri;
            return View("KampanyaOnerisi");
        }






        public async Task<IActionResult> SatisTahminRaporu()
        {
            var rapor = await _veriAnalizService.GetSatisTahminRaporuAsync();
            ViewBag.Tahmin = rapor;
            return View();
        }





        public async Task<IActionResult> GenelDurumAnalizi()
        {
            var gptRapor = await _veriAnalizService.GetGenelDurumAnaliziAsync();
            ViewBag.Rapor = gptRapor;
            return View();
        }













        public async Task<IActionResult> UrunOnerisi(int kullaniciId)
        {
            var kullanici = await _context.Kullancilar.FindAsync(kullaniciId);

            if (kullanici == null)
                return NotFound();

            var gptMetni = await _veriAnalizService.GetUrunOnerisiMetniAsync(kullaniciId);

            var model = new UrunOneriSonucuModel
            {
                KullaniciAdi = kullanici.Adi + " " + kullanici.Soyadi,
                PromptMetni = "Kullanıcının satın aldığı ürünlere göre öneriler:",
                ChatGptCevabi = gptMetni
            };

            return View(model);
        }

        public async Task<IActionResult> SatisOzeti()
        {
            var gptRapor = await _veriAnalizService.GetSatisOzetiMetniAsync();

            ViewBag.Rapor = gptRapor;
            return View();
        }


        public async Task<JsonResult> GenelSatisOzetiVerisi()
        {
            var ozet = await _veriAnalizService.GetGenelSatisOzetiAsync();
            return Json(ozet);
        }


        public IActionResult GenelSatisOzeti()
        {
            return View(); // GenelSatisOzeti.cshtml
        }







    }
}
