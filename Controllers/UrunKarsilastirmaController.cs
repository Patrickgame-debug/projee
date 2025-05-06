using E_Ticaret.DataContext;
using E_Ticaret.Models.Entities;
using E_Ticaret.OrtakKullanim;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret.Controllers
{
    public class UrunKarsilastirmaController : Controller
    {
        private readonly DatabaseContext _context;
        private readonly CompareService _compareService;

        public UrunKarsilastirmaController(DatabaseContext context, CompareService compareService)
        {
            _context = context;
            _compareService = compareService;
        }

        // Ürün karşılaştırma listesini görüntüle
        public IActionResult Index()
        {
            var ids = _compareService.GetCompareList();
            var urunler = _context.Urunler
                .Include(u => u.Marka)
                .Include(u => u.Kategori)
                .Where(u => ids.Contains(u.Id))
                .ToList();

            return View(urunler);
        }

        // Listeye ürün ekle
        [HttpPost]
        public IActionResult AddToCompare(int urunId)
        {
            var urun = _context.Urunler.FirstOrDefault(u => u.Id == urunId);
            if (urun == null)
            {
                TempData["Hata"] = "Ürün bulunamadı.";
                return Redirect(Request.Headers["Referer"].ToString());
            }

            try
            {
                _compareService.AddToCompare(urun);
            }
            catch (InvalidOperationException ex)
            {
                TempData["Hata"] = ex.Message;
            }

            return Redirect(Request.Headers["Referer"].ToString());
        }


        // Listeden ürün çıkar
        [HttpPost]
        public IActionResult RemoveFromCompare(int urunId)
        {
            _compareService.RemoveFromCompare(urunId);
            return RedirectToAction("Index");
        }
    }
}
