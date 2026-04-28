using KSHOP.DAL.Data.DTO.Request;
using KSHOP.DAL.Data.DTO.Response;
using KSHOP.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KSHOP.BLL.Service
{
    public interface IBrandService
    {
        public Task<BrandResponse> CreateBrandAsync(BrandRequest request);
        public Task<List<BrandResponse>> GetAllBrandsAsync();
        public Task<BrandResponse> GetBrand(Expression<Func<Brand, bool>> filter);
         Task<bool> Delete(int id);
    }
}
