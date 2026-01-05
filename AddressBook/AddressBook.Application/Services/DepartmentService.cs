using AddressBook.Application.Interfaces;
using AddressBook.Domain.Entities;
using AddressBook.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Application.Services
{
    public class DepartmentService : GenericService<Department>, IDepartmentService
    {
        public DepartmentService(IGenericRepository<Department> deptRepo):base(deptRepo)
        {
        }
        public async Task AddNewDeptWithValidation(Department dept)
        {
            var existingDepartments = await genericRepo.GetAllAsync();
            if(existingDepartments.Any(d=>d.Name.Trim().Equals(dept.Name.Trim(),StringComparison.OrdinalIgnoreCase)))
                throw new Exception("Department with the same name already exists.");
            await genericRepo.AddAsync(dept);
        }
    }
}
