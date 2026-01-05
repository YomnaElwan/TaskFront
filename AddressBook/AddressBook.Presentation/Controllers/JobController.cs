using AddressBook.Application.Interfaces;
using AddressBook.Application.Services;
using AddressBook.Domain.Entities;
using AddressBook.Presentation.DTOs.JobDTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;

namespace AddressBook.Presentation.Controllers
{
    [Authorize]

    [Route("api/[controller]")]
    [ApiController]
    public class JobController : ControllerBase
    {
        IJobService jobService;
        IMapper _map;
        public JobController(IJobService jobService, IMapper _map)
        {
            this.jobService = jobService;
            this._map = _map;
        }
        [HttpGet]
        public async Task<List<ReadJobsDTO>> JobList()
        {
            List<Job>listOfJobs=await jobService.GetAllAsync();
            List<ReadJobsDTO> jobListDTO = _map.Map<List<ReadJobsDTO>>(listOfJobs);
            return jobListDTO;
        }
        [HttpGet("{id:int}")]
        public async Task<ActionResult<ReadJobsDTO>> GetJobById(int id)
        {
            Job jobFromDB = await jobService.GetByIdAsync(id);
            if (jobFromDB == null)
                return NotFound();
            ReadJobsDTO readJobDTO = _map.Map<ReadJobsDTO>(jobFromDB);
            return Ok(readJobDTO);
        }
        [HttpPost]
        public async Task<ActionResult> AddJob([FromBody]AddJobDTO newJobDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            Job newJob = _map.Map<Job>(newJobDTO);
            try
            {
                await jobService.AddJobWithValidation(newJob);
                await jobService.SaveAsync();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
            return Created($"api/Job/{newJob.Id}", newJobDTO);
        }
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateJob(int id,[FromBody]UpdateJobDTO updateJobDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            if (id != updateJobDTO.Id)
                return BadRequest("Mismatch Id");
            if (updateJobDTO == null)
                return BadRequest();
            Job jobToUpdate = _map.Map<Job>(updateJobDTO);
            await jobService.UpdateAsync(jobToUpdate);
            await jobService.SaveAsync();
            return NoContent();

        }
        [HttpDelete("{jobId}")]
        public async Task<IActionResult> DeleteJob(int jobId)
        {
            Job jobFromDB = await jobService.GetByIdAsync(jobId);
            if (jobFromDB == null)
                return NotFound();
            await jobService.DeleteAsync(jobId);
            await jobService.SaveAsync();
            ReadJobsDTO deletedJobDTO = _map.Map<ReadJobsDTO>(jobFromDB);
            return Ok(deletedJobDTO);
        }

    }
}
