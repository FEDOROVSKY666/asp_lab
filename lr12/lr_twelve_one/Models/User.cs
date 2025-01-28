using System.ComponentModel.DataAnnotations;

namespace lr_twelve_one.Models
{
    public class User
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Please enter first name...")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Error first name...")]
        public string? FirstName { get; set; }
        [Required(ErrorMessage = "Please enter last name...")]
        [StringLength(100, MinimumLength = 4, ErrorMessage = "Error last name...")]
        public string? LastName { get; set; }
        [Required(ErrorMessage = "Please enter age...")]
        [Range(18, 148, ErrorMessage = "Age must be between 18 and 148.")]
        public int Age { get; set; }
    }
}
