using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IGenericRepository<T> where T : class  //This means T represents just class no other datatype
    {
        //return datatype always class In OUR CASE is User Class/ entity
        //Implementation will be in GenericRepository in infrastructure 
        Task<T> GetByIdAsync(int id);  
        IQueryable<T> GetAll(); 
        Task AddAsync(T entity);  //In case we dont have return datatype, we use Task instead of void
        void Update(T entity);
        void Remove(T entity);
        Task <int> SaveChangesAsync();
        Task InsertAsync(T entity);
    }
}


//How to create Generic class in OOP 
//Generic Class(what, why and how)
//A generic class is a class that works with a type parameter instead of a fixed data type.
//Instead of saying:
//“This class works only with int”
//you say:
//“This class works with any type (T)”

