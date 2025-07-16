using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRN222.Models;

namespace PRN222.Controllers
{
    public class StudentController : Controller
    {
        private readonly ProjectPrn222Context _context;

        public StudentController(ProjectPrn222Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var courses = _context.Courses
                             .Include(c => c.Teacher)
                             .ToList();
            return View(courses);
        }
    }
}
