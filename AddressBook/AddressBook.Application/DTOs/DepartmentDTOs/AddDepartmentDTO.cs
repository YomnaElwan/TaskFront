using System.ComponentModel.DataAnnotations;

namespace AddressBook.Presentation.DTOs.DepartmentDTOs
{
    public class AddDepartmentDTO
    {
        [Required(ErrorMessage ="You must enter the department name!")]
        [StringLength(10,MinimumLength =2,ErrorMessage ="Department name length must be between 2 and 10")]
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
