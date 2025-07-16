using Microsoft.AspNetCore.Mvc;
using PRN222.Models; 
using System.Linq;

public class AccountController : Controller
{
    private readonly ProjectPrn222Context _context;

    public AccountController(ProjectPrn222Context context)
    {
        _context = context;
    }


    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }


    [HttpPost]
    public IActionResult Login(string email, string password)
    {
        var user = _context.Users
            .FirstOrDefault(u => u.Email == email && u.Password == password); 

        if (user != null)
        {

            HttpContext.Session.SetString("UserID", user.UserId.ToString());
            HttpContext.Session.SetString("FullName", user.FullName);
            HttpContext.Session.SetString("Role", user.Role);


            switch (user.Role)
            {
                case "Student":
                    return RedirectToAction("Index", "Home");
                case "Teacher":
                    return RedirectToAction("Index", "Admin");
                case "TrafficPolice":
                    return RedirectToAction("Index", "Police");
                case "Admin":
                    return RedirectToAction("Index", "Admin");
                default:
                    return RedirectToAction("Login");
            }
        }

        ViewBag.Error = "Email hoặc mật khẩu không đúng!";
        return View();
    }

    public IActionResult Logout()
    {
        HttpContext.Session.Clear();
        return RedirectToAction("Login");
    }
}
