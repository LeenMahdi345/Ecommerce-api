using KSHOP.DAL.Data;
using KSHOP.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KSHOP.DAL.Repository
{
    public class CategoryRepository: GenericRepository<Category>, ICategoryRepository
    {
        private readonly ApplicationDbContext _context;

        public CategoryRepository(ApplicationDbContext context): base(context)
        {
            _context = context;

        }

      


    }
}
