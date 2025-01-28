using System.ComponentModel.DataAnnotations;

namespace lr_nine.Models
{
    public class CityViewModel
    {
        [Required(ErrorMessage = "Enter the city name...")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Enter correct city name...")]
        public string CityName { get; set; }
    }
}
