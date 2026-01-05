using AddressBook.Application.Interfaces;
using AddressBook.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Application.Services
{
    public class GenericService<T> : IGenericService<T> where T : class
    {
        protected IGenericRepository<T> genericRepo;
        public GenericService(IGenericRepository<T> genericRepo)
        {
            this.genericRepo = genericRepo;
        }
        public Task<List<T>> GetAllAsync()
        {
            return genericRepo.GetAllAsync();
        }

        public Task<T> GetByIdAsync(int Id)
        {
            return genericRepo.GetByIdAsync(Id);
        }
        public async Task AddAsync(T obj)
        {
           await genericRepo.AddAsync(obj);
        }
        public async Task UpdateAsync(T obj)
        {
           await genericRepo.UpdateAsync(obj);
        }
        public async Task DeleteAsync(int Id)
        {
           await genericRepo.DeleteAsync(Id);
        }
        public async Task SaveAsync()
        {
           await genericRepo.SaveAsync();
        }

    }
}
