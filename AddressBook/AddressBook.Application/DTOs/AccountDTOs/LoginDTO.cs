using System.ComponentModel.DataAnnotations;

namespace AddressBook.Presentation.DTOs.AccountDTOs
{
    public class LoginDTO
    {
        [Required(ErrorMessage ="Enter user name!")]
        public string UserName { get; set; }
        [Required(ErrorMessage ="Enter password!")]
        public string Password { get; set; }

    }
}
