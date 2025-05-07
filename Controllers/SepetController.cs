using E_Ticaret.DataContext;
using E_Ticaret.Models.Entities;
using E_Ticaret.ExtensionMethods;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Iyzipay.Model;
using Iyzipay.Request;
using Iyzipay;

namespace E_Ticaret.Controllers
{
    public class SepetController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly IConfiguration _configuration;

        public SepetController(DatabaseContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }

        [HttpPost]
        public IActionResult SetKargo(string kargoTipi)
        {
            HttpContext.Session.SetString("KargoTipi", kargoTipi);
            return RedirectToAction("Index");
        }

        private List<CartLine> GetCart()
        {
            return HttpContext.Session.GetJson<List<CartLine>>("Sepet") ?? new List<CartLine>();
        }

        private void SaveCart(List<CartLine> cart)
        {
            HttpContext.Session.SetJson("Sepet", cart);
        }

        private decimal GetTotal()
        {
            var cart = GetCart();
            return cart.Sum(x => (x.Urun.IndirimliFiyat ?? x.Urun.Fiyat) * x.Quantity);
        }

        public IActionResult Index() // Sepet sayfası
        {
            var cart = GetCart();
            var toplam = GetTotal();

            // Varsayılan kargo tipi
            var kargoTipi = HttpContext.Session.GetString("KargoTipi");
            if (string.IsNullOrEmpty(kargoTipi))
            {
                kargoTipi = "Standart"; // Varsayılan değeri kaydediyoruz
                HttpContext.Session.SetString("KargoTipi", kargoTipi);
            }

            decimal kargoUcreti = 99.99m;
            if (toplam >= 50000)
                kargoUcreti = 0;
            else if (kargoTipi == "Hizli")
                kargoUcreti = 199.99m;

            var model = new CheckoutViewModel
            {
                CartProducts = cart,
                TotalPrice = toplam,
                KargoUcreti = kargoUcreti,
                KargoTipi = kargoTipi
            };

            return View(model);
        }

        public IActionResult Add(int urunId, int quantity = 1, string? returnUrl = null) // Sepete ürün ekleme
        {
            var cart = GetCart();
            var urun = _context.Urunler.FirstOrDefault(p => p.Id == urunId);
            if (urun != null)
            {
                var existing = cart.FirstOrDefault(x => x.Urun.Id == urunId);
                if (existing != null)
                {
                    existing.Quantity += quantity;
                }
                else
                {
                    cart.Add(new CartLine { Urun = urun, Quantity = quantity });
                }

                SaveCart(cart);
                TempData["SepetMesaji"] = "Ürün sepete eklendi!";
            }

            return Redirect(returnUrl ?? "/Sepet/Index");
        }

        public IActionResult Remove(int urunId) // Sepetten ürün silme
        {
            var cart = GetCart();
            cart.RemoveAll(x => x.Urun.Id == urunId);
            SaveCart(cart);
            return RedirectToAction("Index");
        }

        public IActionResult Clear() // Sepeti temizleme
        {
            HttpContext.Session.Remove("Sepet");
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Update(int urunId, int quantity) // Sepetteki ürün miktarını güncelleme
        {
            var cart = GetCart();
            var item = cart.FirstOrDefault(x => x.Urun.Id == urunId);
            if (item != null)
            {
                if (quantity <= 0)
                    cart.Remove(item);
                else
                    item.Quantity = quantity;

                SaveCart(cart);
            }

            return RedirectToAction("Index");
        }


        [Authorize]
        public async Task<IActionResult> Checkout()
        {
            var cart = GetCart();
            var total = GetTotal();
            var userGuidStr = HttpContext.User.FindFirst("KullaniciGuid")?.Value;

            if (string.IsNullOrEmpty(userGuidStr))
                return Unauthorized();

            var userGuid = Guid.Parse(userGuidStr);
            var kullanici = await _context.Kullancilar
                .Include(k => k.Adresler)
                .FirstOrDefaultAsync(k => k.KullaniciGuid == userGuid);

            if (kullanici == null)
                return NotFound();

            var adresListesi = kullanici.Adresler?.Where(a => a.AktifMi).ToList() ?? new();

            var kargoTipi = HttpContext.Session.GetString("KargoTipi") ?? "Standart";
            decimal kargoUcreti = total >= 50000 ? 0 : (kargoTipi == "Hizli" ? 199.99m : 99.99m);

            var model = new CheckoutViewModel
            {
                CartProducts = cart,
                TotalPrice = total,
                KargoTipi = kargoTipi,
                KargoUcreti = kargoUcreti,
                Addresses = adresListesi
            };

            return View(model);
        }



        [Authorize, HttpPost]
        public async Task<IActionResult> Checkout(
        int SelectedAddressId,
        string CardNumber,
        string ExpireMonth,
        string ExpireYear,
        string CVV)
        {
            var cart = GetCart();
            var total = GetTotal();

            if (!cart.Any())
            {
                TempData["Hata"] = "Sepetiniz boş!";
                return RedirectToAction("Index");
            }

            var userGuidStr = HttpContext.User.FindFirst("KullaniciGuid")?.Value;
            if (string.IsNullOrEmpty(userGuidStr))
                return Unauthorized();

            var userGuid = Guid.Parse(userGuidStr);
            var kullanici = await _context.Kullancilar.FirstOrDefaultAsync(k => k.KullaniciGuid == userGuid);
            if (kullanici == null)
                return NotFound();

            var selectedAddress = await _context.Adresler.FindAsync(SelectedAddressId);
            if (selectedAddress == null)
                return NotFound("Adres bulunamadı.");
           

            // Sipariş oluşturuluyor
            var siparis = new Siparis
            {
                SiparisNo = Guid.NewGuid().ToString().Substring(0, 8).ToUpper(),
                ToplamTutar = total,
                KullaniciId = kullanici.Id,
                MusteriNumarasi = kullanici.KullaniciGuid.ToString(),
                TeslimatAdresi = $"{selectedAddress.AcikAdres} - {selectedAddress.Ilce}/{selectedAddress.Sehir}",
                FaturaAdresi = $"{selectedAddress.AcikAdres} - {selectedAddress.Ilce}/{selectedAddress.Sehir}",
                SiparisTarihi = DateTime.Now,
                SiparisDurumu = 0, // Sipariş durumu (Onay Bekliyor)
                SiparisDetaylari = new List<SiparisDetay>()
            };

            foreach (var item in cart)
            {
                siparis.SiparisDetaylari.Add(new SiparisDetay
                {
                    UrunId = item.Urun.Id,
                    Miktar = item.Quantity,
                    BirimFiyati = item.Urun.IndirimliFiyat ?? item.Urun.Fiyat
                });
            }

            _context.Siparisler.Add(siparis);
            await _context.SaveChangesAsync();

            // Sepet temizleniyor
            HttpContext.Session.Remove("Sepet");

            return RedirectToAction("Tesekkurler", "Sepet");

        }


        public IActionResult Tesekkurler()
        {
            return View();
        }







    }
}
