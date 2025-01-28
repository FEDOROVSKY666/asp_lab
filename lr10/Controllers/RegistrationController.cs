using lr_ten.Models;
using Microsoft.AspNetCore.Mvc;

namespace lr_ten.Controllers
{
    public class RegistrationController: Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.ConsultationDate < DateTime.Now)
                {
                    ModelState.AddModelError(nameof(model.ConsultationDate), $"Counseling cannot be in the past tense");
                    return View(model);
                }
                else if(model.ConsultationDate.DayOfWeek == DayOfWeek.Saturday || model.ConsultationDate.DayOfWeek == DayOfWeek.Sunday)
                {
                    ModelState.AddModelError(nameof(model.ConsultationDate), $"Consultation cannot take place on a weekend");
                    return View(model);
                }
                else if(model.ConsultationDate.DayOfWeek == DayOfWeek.Monday && model.Subject == "Основи")
                {
                    ModelState.AddModelError(nameof(model.ConsultationDate), $"Subject \"Основи\" counseling can't be on Monday.");
                    return View(model);
                }
                return View("Result", model);
            }
            else
            {
                return View(model);
            }
        }
    }
}
