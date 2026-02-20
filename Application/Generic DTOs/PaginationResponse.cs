using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Generic_DTOs
{
    public class PaginationResponse<T> where T : class // This means dynamic type, one can use list, string, class, ...etc
    {
        public List<T> Items { get; set; }
        public int Count { get; set; }
        
    }
}


// Design pattern Generic Class
// A generic class is a class that works with any data type, using a type parameter instead of a fixed type.
//T is a placeholder type/ 1. Why using Generic Class: 1 Reusability
//Instead of creating:
//UserRepository
//ProductRepository
//OrderRepository