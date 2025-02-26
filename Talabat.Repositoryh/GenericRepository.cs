using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Interfaces_Or_Repository;
using Talabat.Core.Models;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;

namespace Talabat.Repository
{

    public class GenericRepository<T> : IGenericRepository<T> where T : BaseModel
   {
        private readonly StoreContext _context;

        public GenericRepository(StoreContext context)
        {
            _context = context; // Dependency Injection by3ml connection lel database
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task<T> GetByIdWithSpcAsync(ISpecifications<T> spec)
        {
            return await ApplySpec(spec).FirstOrDefaultAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllWithSpecAsync(ISpecifications<T> spec)
        {
            return await ApplySpec(spec).ToListAsync();
        }
        private IQueryable<T> ApplySpec(ISpecifications<T> spec)
        {
         return  SpecificationEvalutor<T>.GetQuery(_context.Set<T>(), spec);
        }

        public async Task<int> CountAsync(ISpecifications<T> spec)
        {
            return await ApplySpec(spec).CountAsync();
        }

        public async Task AddAsync(T entity)
        => _context.Set<T>().Add(entity);

        public void UpdateAsync(T entity)
        => _context.Set<T>().Update(entity);

        public void DeleteAsync(T entity)
        => _context.Set<T>().Remove(entity);

       
    }
}
