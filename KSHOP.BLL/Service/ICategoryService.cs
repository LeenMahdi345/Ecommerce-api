using KSHOP.DAL.Data.DTO.Request;
using KSHOP.DAL.Data.DTO.Response;
using KSHOP.DAL.Models;
using System.Linq.Expressions;

public interface ICategoryService 
{
    Task<CategoryResponse> CreateCategoryAsync(CategoryRequest request);
    Task<List<CategoryResponse>> GetAllCategoriesAsync();

    Task<CategoryResponse> GetCategory(Expression<Func<Category, bool>> filter);
   Task<bool> Delete (int id);
    //Task<CategoryResponse> Update(CategoryRequest request);
}
