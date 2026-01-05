using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Domain.Entities
{
    public class AddressBookEntry
    {
        public int Id { get; set; }
        public string? FullName { get; set; }
        [ForeignKey("Job")]
        public int JobId { get; set; }
        public virtual Job? Job { get; set; }
        [ForeignKey("Department")]
        public int DepartmentId { get; set; }
        public virtual Department? Department { get; set; }
        public string? PhoneNumber { get; set; }
        public DateOnly BirthOfDate { get; set; }
        public string? Address { get; set; }
        public string? Email { get; set; }
        public string? PhotoURL { get; set; }
        //Driven attribute calculated in the run time
        public int Age
        {
            get
            {
                var today = DateOnly.FromDateTime(DateTime.Today);  
                var age = today.Year - BirthOfDate.Year;  
                if (BirthOfDate > today.AddYears(-age))
                    age--;
                return age;

            }
        }


    }
    }

