using lr_nine.Models;
using Microsoft.AspNetCore.Mvc;

namespace lr_nine.Controllers
{
    public class ProductController: Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            List<ProductViewModel> model = new List<ProductViewModel>();

            model.Add(new ProductViewModel { Id = 1, Name = "Milk", Price = 45 });
            model.Add(new ProductViewModel { Id = 2, Name = "Pizza", Price = 300 });
            model.Add(new ProductViewModel { Id = 3, Name = "Burger", Price = 275 });
            model.Add(new ProductViewModel { Id = 4, Name = "Tomato", Price = 50 });
            model.Add(new ProductViewModel { Id = 5, Name = "Ships", Price = 60 });

            return View(model);
        }
    }
}
