using System.ComponentModel.DataAnnotations;

namespace AddressBook.Presentation.DTOs.AccountDTOs
{
    public class RegisterDTO
    {
        [Required(ErrorMessage ="You must enter user name !")]
        [StringLength(20,MinimumLength =3,ErrorMessage ="User name length must be between 3 & 20 !")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="You must enter password!")]
        [RegularExpression(
        @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$",
        ErrorMessage = "Password must be at least 8 characters and include uppercase, lowercase, number, and special character."
        )]
        public string Password { get; set; }
        [Required(ErrorMessage ="You must enter email!")]
        [EmailAddress(ErrorMessage ="Enter a valid email!")]
        public string Email { get; set; }
    }
}
