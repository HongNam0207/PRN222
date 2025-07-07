using Microsoft.AspNetCore.Mvc;
using PRN222.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace PRN222.Controllers
{
    public class CertificateController : Controller
    {
        private readonly ProjectPrn222Context _context;

        public CertificateController(ProjectPrn222Context context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var passedStudents = _context.Results
                .Include(r => r.User)
                .Where(r => r.PassStatus == true)
                .Select(r => r.User)
                .Distinct()
                .ToList();

            return View(passedStudents);
        }

        [HttpPost]
        public IActionResult Issue(int userId)
        {
            var certificate = new Certificate
            {
                UserId = userId,
                IssuedDate = DateOnly.FromDateTime(DateTime.Now),
                ExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddYears(2)),
                CertificateCode = $"CERT-{DateTime.Now:yyyyMMdd}-{userId}"
            };


            _context.Certificates.Add(certificate);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
