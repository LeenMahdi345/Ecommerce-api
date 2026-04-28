using KSHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KSHOP.DAL.Repository
{
    public interface IProductRepository : IGenericRepository<Product>
    {
       //Task <List<Category>> GetAllAsync(string[]? includes=null);
       //Task <Category> CreateAsync(Category category);
    
      
    }
}
