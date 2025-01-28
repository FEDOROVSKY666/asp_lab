using lr_twelve_two.DataBase;
using lr_twelve_two.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lr_twelve_two.Controllers
{
    public class CompanyController: Controller
    {
        ApplicationContext _context;
        public CompanyController(ApplicationContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            return View(await _context.Companies.ToListAsync());
        }
        [HttpPost]
        public async Task<IActionResult> AddFiveCompanies()
        {
            Company microsoft = new Company { Name = "Microsoft", Field = "Computers", Value = 3.6 };
            Company google = new Company { Name = "Google", Field = "Computers", Value = 2 };
            Company apple = new Company { Name = "Apple", Field = "Computers", Value = 3.4 };
            Company amazon = new Company { Name = "Amazon", Field = "Сommerce", Value = 2.25 };
            Company valve = new Company { Name = "Valve", Field = "Games", Value = 0.0077 };
            _context.Companies.AddRange(microsoft, google, apple, amazon, valve);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Remove(Company company)
        {
            _context.Remove(company);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }
    }
}
