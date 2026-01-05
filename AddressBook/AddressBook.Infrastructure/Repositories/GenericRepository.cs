using AddressBook.Domain.Interfaces;
using AddressBook.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AddressBook.Infrastructure.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        AddressBookDbContext context;
        public GenericRepository(AddressBookDbContext context)
        {
            this.context = context;
        }
        public async Task<List<T>> GetAllAsync()
        {
            return await context.Set<T>().ToListAsync();
        }
        public async Task<T> GetByIdAsync(int Id)
        {
            return await context.Set<T>().FindAsync(Id);
        }

        public async Task AddAsync(T obj)
        {
            await context.Set<T>().AddAsync(obj);
        }
        public async Task UpdateAsync(T obj)
        {
            var existing = await context.Set<T>().FindAsync(obj.GetType().GetProperty("Id").GetValue(obj));
            if (existing != null)
            {
                context.Entry(existing).CurrentValues.SetValues(obj);
            }
        }

        public async Task DeleteAsync(int Id)
        {
            var obj = await context.Set<T>().FindAsync(Id);
            if (obj != null)
            {
                context.Set<T>().Remove(obj);
            }
        }
      
        public async Task SaveAsync()
        {
            await context.SaveChangesAsync();
        }

        public IQueryable<T> GetQueryable()
        {
            return context.Set<T>().AsQueryable();
        }
    }
}
