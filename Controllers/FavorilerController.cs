using E_Ticaret.DataContext;
using E_Ticaret.ExtensionMethods;
using E_Ticaret.Models.Entities;
using Microsoft.AspNetCore.Mvc;

namespace E_Ticaret.Controllers
{
    public class FavorilerController : Controller
    {
        private readonly DatabaseContext _context;
        public FavorilerController(DatabaseContext context)
        {
            _context = context;
        }



        public IActionResult Index()
        {
            var favoriler = Favorilerim(); // Favoriler listesini al
            return View(favoriler);
        }

        private List<Urun> Favorilerim() // Favoriler listesini döndürür MyFavorites yada GetFavorites // ÜRÜN İCİN
        {
            return HttpContext.Session.GetJson<List<Urun>>("Favorilerim") ?? []; // Favoriler listesini al, eğer yoksa boş liste döndür


        }
        public IActionResult Add(int UrunId, string? returnUrl = null)
        {
            var favoriler = Favorilerim();
            var urun = _context.Urunler.Find(UrunId);
            if (urun != null && !favoriler.Any(p => p.Id == UrunId))
            {
                favoriler.Add(urun);
                HttpContext.Session.SetJson("Favorilerim", favoriler);
                TempData["FavoriMesaji"] = "Ürün favorilere eklendi! 💖";
            }

            return Redirect(returnUrl ?? "/");
        }

        public IActionResult Remove(int UrunId)
        {
            var favoriler = Favorilerim();
            var urun = _context.Urunler.Find(UrunId);
            if (urun != null && favoriler.Any(p => p.Id == UrunId))
            {
                favoriler.RemoveAll(i => i.Id == urun.Id);
                HttpContext.Session.SetJson("Favorilerim", favoriler);
            }
            return RedirectToAction("Index");
        }
        
        // 2.05.2025   favorideki ürünü sepete ekleme işlemi saat 13.24 de yapıldı
        public IActionResult SepeteEkle(int urunId)
        {
            var cart = HttpContext.Session.GetJson<List<CartLine>>("Sepet") ?? new List<CartLine>();
            var urun = _context.Urunler.FirstOrDefault(p => p.Id == urunId);

            if (urun != null)
            {
                var existing = cart.FirstOrDefault(x => x.Urun.Id == urunId);
                if (existing != null)
                {
                    existing.Quantity += 1;
                }
                else
                {
                    cart.Add(new CartLine { Urun = urun, Quantity = 1 });
                }

                HttpContext.Session.SetJson("Sepet", cart);
                TempData["SepetMesaji"] = "Favorilerden sepete eklendi! 🎉";
            }

            return RedirectToAction("Index");
        }

    }
}
