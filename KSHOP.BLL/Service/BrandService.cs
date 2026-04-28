using KSHOP.DAL.Data.DTO.Request;
using KSHOP.DAL.Data.DTO.Response;
using KSHOP.DAL.Models;
using KSHOP.DAL.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace KSHOP.BLL.Service
{
    public class BrandService : IBrandService
    {
        private readonly IBrandRepository _brandRepository;

        public BrandService(IBrandRepository brandRepository)
        {
            _brandRepository = brandRepository;
        }
        public async Task<BrandResponse> CreateBrandAsync(BrandRequest request)
        {
            var brand = request.Adapt<Brand>();
            var created =await _brandRepository.CreateAsync(brand);
            return created.Adapt<BrandResponse>();

        }

        public async Task<bool> Delete(int id)
        {
            var brand=await  _brandRepository.GetOne(c=>c.Id==id);
            if (brand == null)
            {
                return false;
            }
            else
            {

                var result = await _brandRepository.DeleteAsync(brand);
                return true;
            }
        
        }
        
        public async Task<List<BrandResponse>> GetAllBrandsAsync()
        {

            var brands=await _brandRepository.GetAllAsync(b=>b.Status==EntityStatus.Active, new string[] { nameof(Brand.Translations), nameof(Brand.CreatedBy)});
            foreach (var brand in brands)
            {
                if (brand.Translations == null)
                {
                    brand.Translations = new List<BrandTranslation>();
                }
            }
            return brands.Adapt<List<BrandResponse>>();
        }

        public  async Task<BrandResponse> GetBrand(Expression<Func<Brand, bool>> filter)
        {
                var brand = await  _brandRepository.GetOne(filter, new string[] { nameof(Brand.Translations) });
                if (brand == null)
                    return null;
            return brand.Adapt<BrandResponse>();
        }
        
    }
}
