using System.ComponentModel.DataAnnotations;

namespace AddressBook.Presentation.DTOs.JobDTOs
{
    public class AddJobDTO
    {
        [StringLength(25,MinimumLength =10,ErrorMessage ="Job title length must be between 10 & 25 !")]
        [Required(ErrorMessage ="You must enter the job title!")]
        public string Title { get; set; }
        public string? Description { get; set; }
    }
}
