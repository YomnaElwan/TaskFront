using AddressBook.Application.Interfaces;
using AddressBook.Domain.Entities;
using AddressBook.Presentation.DTOs.AddressBookDTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class AddressBookEntryController : ControllerBase
    {
        IAddressBookService addressBookService;
        IMapper map;
        public AddressBookEntryController(IAddressBookService addressBookService, IMapper map)
        {
            this.addressBookService = addressBookService;
            this.map = map;
        }
        [HttpGet]
        public async Task<List<ReadAddressBookDTO>> GetAddressBookList()
        {
            List<AddressBookEntry> addressBookList = await addressBookService.GetAllAsync();
            List<ReadAddressBookDTO> readAddressBookDTO = map.Map<List<ReadAddressBookDTO>>(addressBookList);
            return readAddressBookDTO;
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<ReadAddressBookDTO>> GetAddressBookById(int id)
        {
            AddressBookEntry addressBookFromDB = await addressBookService.GetByIdAsync(id);
            if (addressBookFromDB == null)
                return NotFound();
            ReadAddressBookDTO readAddressBookDTO = map.Map<ReadAddressBookDTO>(addressBookFromDB);
            return Ok(readAddressBookDTO);
        }
        
        [HttpPost]
        public async Task<ActionResult> AddNewAddressBook(AddAddressBookDTO newAddressBookDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            AddressBookEntry addressBookEntry = map.Map<AddressBookEntry>(newAddressBookDTO);
            await addressBookService.AddAsync(addressBookEntry);
            await addressBookService.SaveAsync();
            return Created();
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAddressBook(int id , UpdateAddressBookDTO updateAddressBookDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (id != updateAddressBookDTO.Id)
                return BadRequest("Mismatch Id");
            if (updateAddressBookDTO == null)
                return BadRequest();
            var updatedAddressBook = map.Map<AddressBookEntry>(updateAddressBookDTO);
            await addressBookService.UpdateAsync(updatedAddressBook);
            await addressBookService.SaveAsync();
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAddressBook(int id)
        {
            AddressBookEntry addressBookFromDB = await addressBookService.GetByIdAsync(id);
            if (addressBookFromDB == null)
                return NotFound();
            await addressBookService.DeleteAsync(id);
            await addressBookService.SaveAsync();
            ReadAddressBookDTO readDeletedAddressBook = map.Map<ReadAddressBookDTO>(addressBookFromDB);
            return Ok(readDeletedAddressBook);
        }  
        [HttpPost("Search")]
        public async Task<ActionResult> Search([FromBody] SearchAddressBookDTO searchDTO)
        {
            var results = await addressBookService.Search(searchDTO);
            List<ReadAddressBookDTO> readAddressBookDTO = map.Map<List<ReadAddressBookDTO>>(results);
            return Ok(readAddressBookDTO);
        }
    }
}
