using E_Ticaret.DataContext;
using E_Ticaret.Models.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace E_Ticaret.Areas.Admin.Controllers
{
    public class MainController : Controller
    {
        private readonly DatabaseContext _context;
        public MainController(DatabaseContext context)
        {
            _context = context;
        }
        [Area("admin")]
        [Authorize(Policy = "AdminPolicy")]
        public async Task<IActionResult> Index()
        {

            return View(Index);
        }

    }
}
