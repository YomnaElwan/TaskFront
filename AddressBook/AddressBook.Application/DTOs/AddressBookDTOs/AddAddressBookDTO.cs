using System.ComponentModel.DataAnnotations;

namespace AddressBook.Presentation.DTOs.AddressBookDTOs
{
    public class AddAddressBookDTO
    {
        [Required(ErrorMessage ="Full Name is required !")]
        [StringLength(100,MinimumLength =6,ErrorMessage ="Full Name length must be between 6 & 100")]
        public string? FullName { get; set; }
        [Required(ErrorMessage ="Job is required !")]
        public int JobId { get; set; }
        [Required(ErrorMessage ="Department is required !")]
        public int DepartmentId { get; set; }

        [RegularExpression(@"^\+20[0-9]{10}$", ErrorMessage = "Phone Number isn't correct")]
        public string? PhoneNumber { get; set; }
        [Required(ErrorMessage ="Enter Birth Date!")]
        public DateOnly BirthOfDate { get; set; }
        public string? Address { get; set; }
        [EmailAddress(ErrorMessage ="Invalid email address!")]
        public string? Email { get; set; }
        public string? PhotoURL { get; set; }
    }
}
