using E_Ticaret.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace E_Ticaret.ViewComponents
{
    public class Kategoriler : ViewComponent
    {
        // Kategoriler sınıfı, ASP.NET Core MVC'de bir ViewComponent'dır. 
        // Bu sınıf, veritabanından kategori verilerini alarak bir görünüm bileşeni olarak sunmak için kullanılır.
        private readonly DatabaseContext _context;

        public Kategoriler(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(await _context.Kategoriler.ToListAsync());

        }
    }
    
}
