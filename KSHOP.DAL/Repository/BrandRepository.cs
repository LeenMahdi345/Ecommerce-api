using KSHOP.DAL.Data;
using KSHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSHOP.DAL.Repository
{
    public class BrandRepository:GenericRepository<Brand>, IBrandRepository
    {
        private readonly ApplicationDbContext _context;
        
        public BrandRepository(ApplicationDbContext context):base(context) {
         _context=context;        

        }
      
        
            
        }
    }

