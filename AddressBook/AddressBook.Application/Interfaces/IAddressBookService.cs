using AddressBook.Domain.Entities;
using AddressBook.Presentation.DTOs.AddressBookDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Application.Interfaces
{
    public interface IAddressBookService:IGenericService<AddressBookEntry>
    {
        Task<List<AddressBookEntry>> Search(SearchAddressBookDTO searchDTO);
    }
}
