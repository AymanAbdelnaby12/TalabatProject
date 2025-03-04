using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core;
using Talabat.Core.Interfaces_Or_Repository;
using Talabat.Core.Models;
using Talabat.Repository;
using Talabat.Repository.Data;

namespace Talabat.Services
{
    // this class is collection of all repositories in the project and it is responsible for changing the database by dbcontext
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StoreContext _context;
        private Hashtable _repositories;// hashTable is a collection of key value pairs i used it here to store the repositories
        public UnitOfWork(StoreContext context)
        {
            _context = context;
            _repositories = new Hashtable(); 
        }

        public async Task<int> completeAsync()
            => await _context.SaveChangesAsync();
       

        public async ValueTask DisposeAsync()
            => _context.DisposeAsync();

        // this method is responsible for creating the repository and store it in the hashTable
        public IGenericRepository<TEntity> Repository <TEntity>() where TEntity : BaseModel
        {
           var type=typeof(TEntity);
            // check if the repository is null
            if (!_repositories.ContainsKey(type)) //create new object from repository for first time
            {
                var repository=new GenericRepository<TEntity>(_context);
                _repositories.Add(type, repository);
            }
            // in hashTable we can do one Casting only when we get the value other wise use dictionary you need to use casting every time you get the value
            return _repositories[type] as IGenericRepository<TEntity>; 
        }
    }
}
