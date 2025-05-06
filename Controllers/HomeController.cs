using System.Diagnostics;
using E_Ticaret.Areas.Admin.Controllers;
using E_Ticaret.DataContext;
using E_Ticaret.Models;
using E_Ticaret.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret.Controllers
{
    public class HomeController : Controller
    {
        private readonly DatabaseContext _context;


        public HomeController(DatabaseContext context)
        {
            _context = context;
        }
       
        public async Task<IActionResult> Index()

        {
            var model = new AnaSayfaModeli()
            {
                Sliders = await _context.AnaSayfaSliderlar.Where(p => p.AktifMi).ToListAsync(),
                Urunler = await _context.Urunler.Where(a => a.AktifMi).ToListAsync(),
                Kampanyalar = await _context.Kampanyalar.Where(i=>i.AktifMi).ToListAsync()

            };
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();


        }
        [Route("AccessDenied")]
        public IActionResult AccessDenied()
        {
            return View();


        }
        [HttpGet]
        public IActionResult Iletisim()
        {
            return View();

        }

        [HttpPost]
        public IActionResult Iletisim(Iletisim iletisim)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Iletisimler.Add(iletisim);
                    var sonuc = _context.SaveChanges();
                    if(sonuc > 0)
                    {
                        TempData["Mesaj"] = "<div class=\"alert alert-success alert-dismissible fade show\" role=\"alert\">\r\n  <b>Mesaj�n�z G�nderilmi�tir.</b> <button type=\"button\" class=\"btn-close\" data-bs-dismiss=\"alert\" aria-label=\"Close\"></button>\r\n</div>";
                        return RedirectToAction("Iletisim");
                    }
                    else
                    {
                        ViewBag.Mesaj = "Mesaj�n�z G�nderilemedi";
                    }
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Mesaj�n�z G�nderilemedi");
                }
                
            }
            return View(iletisim);
        }
        // dinamik veri ekleme kategori k�sm�ndan admin panelinden yani  (dinamik olarak kategoriler gelsin)

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
