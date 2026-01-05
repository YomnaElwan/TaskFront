using AddressBook.Domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Infrastructure.Data
{
    public class AddressBookDbContext:IdentityDbContext<ApplicationUser>
    {

        public AddressBookDbContext(DbContextOptions<AddressBookDbContext> options):base(options)
        {
            
        }
        public virtual DbSet<AddressBookEntry> AddressBookEntries { get; set; }
        public virtual DbSet<Department> Departments { get; set; }
        public virtual DbSet<Job> Jobs { get; set; }
    }
}
