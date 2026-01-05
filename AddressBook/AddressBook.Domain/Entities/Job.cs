using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Domain.Entities
{
    public class Job
    {
       public int Id { get; set; }
       public string Title { get; set; }
       public string? Description { get; set; }
       public virtual List<AddressBookEntry> AddressBooks { get; set; } = new List<AddressBookEntry>();
    }
}
