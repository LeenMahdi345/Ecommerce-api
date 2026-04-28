using KSHOP.DAL.Data.DTO.Request;
using KSHOP.DAL.Data.DTO.Response;
using KSHOP.DAL.Models;
using KSHOP.DAL.Repository;
using Mapster;
using System.Linq.Expressions;

namespace KSHOP.BLL.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryResponse> CreateCategoryAsync(CategoryRequest request)
        {
            var category = request.Adapt<Category>();

            var created = await _categoryRepository.CreateAsync(category);

            return created.Adapt<CategoryResponse>();
        }

        public async Task<List<CategoryResponse>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync(c => c.Status==EntityStatus.Active, new string[] { nameof(Category.Translations)
            , nameof(Category.CreatedBy) });
            foreach (var category in categories)
            {
                if (category.Translations == null)
                {
                    category.Translations = new List<CategoryTranslation>();
                }

                // لو عندك خصائص أخرى ممكن تكون null، تعمل نفس الشيء
                // category.OtherCollection ??= new List<OtherType>();
            }

            return categories.Adapt<List<CategoryResponse>>();
        }
        public async Task<CategoryResponse?> GetCategory(Expression<Func<Category, bool>> filter)
        {
            var category = await _categoryRepository.GetOne(filter, new string[] { nameof(Category.Translations) });
            if (category == null)
                return null;
            return category.Adapt<CategoryResponse>();
        }
        public async Task<bool> Delete(int id)
        {
            var categry = await _categoryRepository.GetOne(c => c.Id == id);
            if (categry != null)
            {
                await _categoryRepository.DeleteAsync(categry);
                return true;


            }
            return false;

        }
      
    }
}
