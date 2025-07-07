using Microsoft.AspNetCore.Mvc;
using PRN222.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace PRN222.Controllers
{
    public class PoliceController : Controller
    {
        private readonly ProjectPrn222Context _context;

        public PoliceController(ProjectPrn222Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var exams = _context.Exams.Include(e => e.Course).ToList();
            return View(exams);
        }
    }
}
