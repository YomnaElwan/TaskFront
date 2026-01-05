using AddressBook.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Application.Interfaces
{
    public interface IDepartmentService:IGenericService<Department>
    {
        Task AddNewDeptWithValidation(Department dept);
    }
}
