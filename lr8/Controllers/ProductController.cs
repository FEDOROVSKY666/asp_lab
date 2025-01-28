using lr_eight.Models;
using Microsoft.AspNetCore.Mvc;

namespace lr_eight.Controllers
{
    public class ProductController: Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            List<ProductViewModel> products = new List<ProductViewModel>();

            ProductViewModel product1 = new ProductViewModel { Id = 0, Name = "Milk", Price = 50, CreatedDate =DateTime.Now };
            ProductViewModel product2 = new ProductViewModel { Id = 1, Name = "Juice", Price = 100, CreatedDate = new DateTime(2024, 2, 12, 10, 39, 25) };
            ProductViewModel product3 = new ProductViewModel { Id = 2, Name = "Burger", Price = 200.75, CreatedDate = new DateTime(2025, 12, 01, 01, 33, 00) };
            ProductViewModel product4 = new ProductViewModel { Id = 3, Name = "Pizza", Price = 230.75, CreatedDate = new DateTime(2026, 03, 19, 8, 8, 48) };
            ProductViewModel product5 = new ProductViewModel { Id = 4, Name = "Chocolate", Price = 150, CreatedDate = new DateTime(2028, 12, 31, 23, 59, 59) };

            products.Add(product1);
            products.Add(product2);
            products.Add(product3);
            products.Add(product4);
            products.Add(product5);

            return View(products);
        }
    }
}
