using System.ComponentModel.DataAnnotations;

namespace AddressBook.Presentation.DTOs.AddressBookDTOs
{
    public class SearchAddressBookDTO
    {
        public string? FullName { get; set; }
        public int? JobId { get; set; }
        public int? DepartmentId { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public DateOnly? BirthDateFrom { get; set; }
        public DateOnly? BirthDateTo { get; set; }

    }
}
