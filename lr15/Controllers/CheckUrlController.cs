using lr_fifteen.Models;
using lr_fifteen.Services;
using Microsoft.AspNetCore.Mvc;

namespace lr_fifteen.Controllers
{
    public class CheckUrlController: Controller
    {
        private readonly BackgroundCheckService _serivce;

        public CheckUrlController(BackgroundCheckService serivce)
        {
            _serivce = serivce;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(UrlModel model)
        {
            if (ModelState.IsValid)
            {
                var url = model.Url;
                _serivce.CheckUrlAvailable(url);
                return View();
            }
            else
            {
                return View(model);
            }
        }
    }
}
