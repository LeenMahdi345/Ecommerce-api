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
    public interface IProductService
    {
            Task<ProductResponse> CreateProductAsync(ProductRequest request);
            Task<List<ProductResponse>> GetAllProductsAsync();
                    Task<ProductResponse> GetProduct(Expression<Func<Product, bool>> filter);   
             Task<bool> Delete(int id);
        Task<bool> UpdateProductAsync(int id, ProductUpdateRequest request);
        Task<bool> ToggleStatus(int id);
    }
}
