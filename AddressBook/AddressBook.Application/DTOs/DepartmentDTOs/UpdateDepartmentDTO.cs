using System.ComponentModel.DataAnnotations;

namespace AddressBook.Presentation.DTOs.DepartmentDTOs
{
    public class UpdateDepartmentDTO
    {
       public int Id { get; set; }
       [Required(ErrorMessage ="Department name is required!")]
       [Length(maximumLength: 10, minimumLength: 2, ErrorMessage = "Department name length must be between 2 and 10")]
       public string Name { get; set; }
       public string? Description { get; set; }
    }
}
