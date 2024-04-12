using System.ComponentModel.DataAnnotations;

namespace TastyDelivery.Core.Models.AdminModels
{
    public class AppointDriverModel
    {
        [Required(ErrorMessage = "Please enter the first name.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Please enter the last name.")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Please enter the email.")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter a password.")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter the phone number.")]
        [Phone(ErrorMessage = "Please enter a valid phone number.")]
        public string PhoneNumber { get; set; }

        
    }
}
