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
    public class JobService : GenericService<Job>, IJobService
    {
        public JobService(IGenericRepository<Job> jobRepo):base(jobRepo)
        {
        }
        public async Task AddJobWithValidation(Job newJob)
        {
            var existingJobs = await genericRepo.GetAllAsync();
            if (existingJobs.Any(j => j.Title.Trim().Equals(newJob.Title.Trim(), StringComparison.OrdinalIgnoreCase)))
                throw new Exception("Job with the same title is already existing!");
            await genericRepo.AddAsync(newJob);
        }
    }
}
