using KSHOP.DAL.Data;
using KSHOP.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace KSHOP.DAL.Repository
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public GenericRepository(ApplicationDbContext context)
        {
            _context = context;

        }


        public async Task<T> CreateAsync(T entity)
        {
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<List<T>> GetAllAsync( Expression <Func<T,bool>>filter,string[]? includes = null)
        {
            IQueryable<T> query = _context.Set<T>();
            if (filter != null)
            {
                query = query.Where(filter);
            }
            if (includes != null)
                foreach (var include in includes)
                {

                    query = query.Include(include);
                }
            //Include(c => c.Translations).ToListAsync();
            //return await _context.Set<T>().ToListAsync();
            return await query.ToListAsync();
        }


        public async Task<bool> DeleteAsync(T entity)
        {
            _context.Remove(entity);
            var affected = await _context.SaveChangesAsync();
            return affected > 0;
        }

        public Task<T> GetOne(Expression<Func<T, bool>> filter, string[]? includes = null)
        {

            IQueryable<T> querey = _context.Set<T>();
            if (includes != null)
                foreach (var include in includes)
                {

                    querey = querey.Include(include);
                }
            return querey.FirstOrDefaultAsync(filter);
        }

        public async Task<bool> UpdateAsync(T entity)
        {
       
          _context.Set<T>().Update(entity); // أخبر EF أن هذا الكائن محدث
            //await _context.SaveChangesAsync(); // احفظ التغييرات
            //return entity; // رجع الكائن بعد التحديث
            var affected = await _context.SaveChangesAsync();
            return affected > 0;

        }

     
    }
}