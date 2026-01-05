using AddressBook.Application.Interfaces;
using AddressBook.Domain.Entities;
using AddressBook.Domain.Interfaces;
using AddressBook.Presentation.DTOs.AddressBookDTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Application.Services
{
    public class AddressBookService:GenericService<AddressBookEntry>,IAddressBookService
    {
        public AddressBookService(IGenericRepository<AddressBookEntry> addressBookRepo) :base(addressBookRepo)
        {
        }

        public async Task<List<AddressBookEntry>> Search(SearchAddressBookDTO searchDTO)
        {
            var query = genericRepo.GetQueryable();
            if (!string.IsNullOrWhiteSpace(searchDTO.FullName))
                query = query.Where(n => n.FullName!=null &&n.FullName.Trim().ToLower().Contains(searchDTO.FullName.Trim().ToLower()));
            if (searchDTO.DepartmentId.HasValue)
                query = query.Where(d => d.DepartmentId == searchDTO.DepartmentId);
            if (searchDTO.JobId.HasValue)
                query = query.Where(j => j.JobId == searchDTO.JobId);
            if (!string.IsNullOrEmpty(searchDTO.Email))
                query = query.Where(e => e.Email!=null &&e.Email.Trim().ToLower().Contains(searchDTO.Email.Trim().ToLower()));
            if (!string.IsNullOrEmpty(searchDTO.Address))
                query = query.Where(a => a.Address!=null&&a.Address.Trim().ToLower().Contains(searchDTO.Address.Trim().ToLower()));
            if (!string.IsNullOrEmpty(searchDTO.PhoneNumber))
                query = query.Where(p => p.PhoneNumber!=null && p.PhoneNumber.Trim().Contains(searchDTO.PhoneNumber.Trim()));
            if (searchDTO.BirthDateFrom.HasValue)
                query = query.Where(f => f.BirthOfDate >= searchDTO.BirthDateFrom);
            if (searchDTO.BirthDateTo.HasValue)
                query = query.Where(t => t.BirthOfDate <= searchDTO.BirthDateTo);
            return await query.ToListAsync();
        }
    }
}
