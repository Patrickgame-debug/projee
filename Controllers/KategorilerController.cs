using E_Ticaret.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret.Controllers
{
    public class KategorilerController : Controller
    {

        private readonly DatabaseContext _context;

        public KategorilerController(DatabaseContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> IndexAsync(int? id)
        {

            if (id == null)
            {
                return NotFound();
            }

            var kategori = await _context.Kategoriler.Include(x => x.Urunler)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kategori == null)
            {
                return NotFound();
            }

            return View(kategori);
        }


    }
}
