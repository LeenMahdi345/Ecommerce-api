using KSHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KSHOP.DAL.Repository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> CreateAsync(T entity);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> filter, string[]? includes = null);
        Task<T> GetOne(Expression<Func<T, Boolean>> filter, string[]? includes = null);
        Task<bool> DeleteAsync(T entity);
        Task<bool> UpdateAsync(T entity);



    }
}
