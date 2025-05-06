using E_Ticaret.DataContext;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret.Controllers
{
    public class KampanyalarController : Controller
    {

        private readonly DatabaseContext _context;


        public KampanyalarController(DatabaseContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _context.Kampanyalar.ToListAsync());
        }
        // GET 
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var kampanya = await _context.Kampanyalar
                .FirstOrDefaultAsync(m => m.Id == id);
            if (kampanya == null)
            {
                return NotFound();
            }

            return View(kampanya);
        }
       


    }
}
