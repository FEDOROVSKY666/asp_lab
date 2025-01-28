using lr_fifteen.DB;
using lr_fifteen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lr_fifteen.Controllers
{
    public class UserController: Controller
    {
        private readonly ApplicationContext _context;
        private readonly DBEmailService _serivce;

        public UserController(ApplicationContext context, DBEmailService serivce)
        {
            _context = context;
            _serivce = serivce;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return View(await _context.Users.ToListAsync());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(UserModel model)
        {
            if (ModelState.IsValid)
            {
                await _serivce.AddNewUser(model);
                return RedirectToAction("Index");
            }
            else
            {   
                return View(model);
            }
        }
    }
}
