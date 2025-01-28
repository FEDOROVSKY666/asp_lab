using Microsoft.AspNetCore.Mvc;
using lr_evelen.Filters;

namespace lr_evelen.Controllers
{
    public class RedirectionController: Controller
    {
        [HttpGet]
        [ServiceFilter(typeof(UserAcitionFilter))]
        [ServiceFilter(typeof(UniqueUserFilter))]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet("Picture")]
        [ServiceFilter(typeof(UserAcitionFilter))]
        [ServiceFilter(typeof(UniqueUserFilter))]
        public IActionResult Picture()
        {
            return View();
        }
        [HttpGet("SomeInfo")]
        [ServiceFilter(typeof(UserAcitionFilter))]
        [ServiceFilter(typeof(UniqueUserFilter))]
        public IActionResult SomeInfo()
        {
            return View();
        }
    }
}
