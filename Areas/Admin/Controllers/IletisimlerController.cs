using E_Ticaret.DataContext;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Policy = "AdminPolicy")]
    public class IletisimlerController : Controller
    {


        private readonly DatabaseContext _context;

        public IletisimlerController(DatabaseContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View(_context.Iletisimler);
        }

        // GET: Admin//Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ıletisim = await _context.Iletisimler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ıletisim == null)
            {
                return NotFound();
            }

            return View(ıletisim);
        }

        // POST: Admin/Kullanicilar/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var ıletisim = await _context.Iletisimler.FindAsync(id);
            if (ıletisim != null)
            {
                _context.Iletisimler.Remove(ıletisim);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: Admin/ILetısım/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var ıletisim = await _context.Iletisimler
                .FirstOrDefaultAsync(m => m.Id == id);
            if (ıletisim == null)
            {
                return NotFound();
            }

            return View(ıletisim);
        }



    }
}
