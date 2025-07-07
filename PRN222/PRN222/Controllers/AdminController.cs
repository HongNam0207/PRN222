using Microsoft.AspNetCore.Mvc;
using PRN222.Models; // Đảm bảo đúng namespace của DbContext và Models
using System.Linq;

namespace PRN222.Controllers
{
    public class AdminController : Controller
    {
        private readonly ProjectPrn222Context _context;

        public AdminController(ProjectPrn222Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var users = _context.Users.ToList();
            return View(users);
        }
    }
}
