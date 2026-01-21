using Application.Repositories;
using Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.RepositoriesInf
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        //Creating instance and Constructor dbContext
        private readonly BookingSAppContext _appContext; //instance 
        private readonly DbSet<T> _dbSet; // DbSet Instance //Represents a table in the database for the entity type 
        public GenericRepository(BookingSAppContext context)
        {
            _appContext = context;
            _dbSet = _appContext.Set<T>();

        }
        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id); //Represents a table in the database for the entity type 
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public async Task AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            _dbSet.Update(entity);
            
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);

        }

        public async Task<int> SaveChangesAsync()
        {
            return await _appContext.SaveChangesAsync();
        }

      

        
    }
}


//After finishing => start with registeration in independancy injection lifeCycle in Program.cs