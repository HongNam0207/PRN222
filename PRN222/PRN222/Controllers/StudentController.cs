using Microsoft.AspNetCore.Mvc;

namespace PRN222.Controllers
{
    public class StudentController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
