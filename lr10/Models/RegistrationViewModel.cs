using System.ComponentModel.DataAnnotations;

namespace lr_ten.Models
{
    public class RegistrationViewModel
    {
        [Required(ErrorMessage = "Please enter your name...")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Error name...")]
        public String Name { get; set; }
        [Required(ErrorMessage = "Please enter your email...")]
        [EmailAddress(ErrorMessage = "Error email format...")]
        public String Email { get; set; }
        [Required(ErrorMessage = "Please enter consultation date...")]
        [DataType(DataType.Date, ErrorMessage = "Error date type...")]
        public DateTime ConsultationDate { get; set; }
        [Required(ErrorMessage = "Please select consultation subject...")]
        public String Subject { get; set; }
    }
}
