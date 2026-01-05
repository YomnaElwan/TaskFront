using System.ComponentModel.DataAnnotations;

namespace AddressBook.Presentation.DTOs.JobDTOs
{
    public class UpdateJobDTO
    {
        public int Id { get; set; }

        [Length(maximumLength: 25, minimumLength: 10, ErrorMessage = "Job title length must be between 10 & 25 !")]
        [Required(ErrorMessage = "You must enter the job title!")]
        public string Title { get; set; }
        public string? Description { get; set; }
    }
}
