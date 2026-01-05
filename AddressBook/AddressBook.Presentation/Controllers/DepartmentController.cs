using AddressBook.Application.Interfaces;
using AddressBook.Application.Services;
using AddressBook.Domain.Entities;
using AddressBook.Presentation.DTOs.DepartmentDTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace AddressBook.Presentation.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        IDepartmentService deptService;
        IMapper _mapper;
        public DepartmentController(IDepartmentService deptService, IMapper _mapper)
        {
            this.deptService = deptService;
            this._mapper=_mapper;
        }
        [HttpGet]
        public async Task<List<ReadDepartmentDTO>> GetDepartments()
        {
            List<Department> departmentsList = await deptService.GetAllAsync();
            List<ReadDepartmentDTO> departmentListDTO = _mapper.Map<List<ReadDepartmentDTO>>(departmentsList);
            return departmentListDTO;
          
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ReadDepartmentDTO>> GetDepartmentById(int id)
        {
            Department deptById = await deptService.GetByIdAsync(id);
            if (deptById == null)
                return NotFound();
            ReadDepartmentDTO deptDTO = _mapper.Map<ReadDepartmentDTO>(deptById);
            return Ok(deptDTO);
        }
        [HttpPost]
        public async Task<ActionResult> AddDepartment([FromBody]AddDepartmentDTO newDepartmentDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Department newDepartment = _mapper.Map<Department>(newDepartmentDTO);
            try {
                await deptService.AddNewDeptWithValidation(newDepartment);
                await deptService.SaveAsync();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
           
            return Created($"api/Department/{newDepartment.Id}",newDepartmentDTO);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateDepartment(int id,[FromBody] UpdateDepartmentDTO deptDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (id != deptDTO.Id)
                return BadRequest("Mismatch Id!");
            if (deptDTO == null)
                return BadRequest();
            Department deptToUpdate = _mapper.Map<Department>(deptDTO);
            await deptService.UpdateAsync(deptToUpdate);
            await deptService.SaveAsync();
            return NoContent();
        }
        
           
        [HttpDelete("{deptId}")]
        public async Task<ActionResult> DeleteDepartment(int deptId)
        {
            Department deptToDelete =  await deptService.GetByIdAsync(deptId);
            if (deptToDelete == null)
                return NotFound();
            await deptService.DeleteAsync(deptId);
            await deptService.SaveAsync();
            ReadDepartmentDTO deleteDeptDTO = _mapper.Map<ReadDepartmentDTO>(deptToDelete);
            return Ok(deleteDeptDTO);
        }
    }
}
