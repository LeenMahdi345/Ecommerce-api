using KSHOP.DAL.Data.DTO.Request;
using KSHOP.DAL.Data.DTO.Response;
using KSHOP.DAL.Models;
using KSHOP.DAL.Repository;
using Mapster;
using System.Globalization;
using System.Linq.Expressions;

namespace KSHOP.BLL.Service
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileService _fileService;

        public ProductService(IProductRepository productRepository, IFileService fileService)
        {
            _productRepository = productRepository;
            _fileService = fileService;
        }

        public async Task<ProductResponse> CreateProductAsync(ProductRequest request)
        {
            var product = request.Adapt<Product>();
            if (request.MainImage != null)
            {
                var imagePath = await _fileService.UploadAsync(request.MainImage);
                product.MainImage = imagePath;
            }
            var created = await _productRepository.CreateAsync(product);

            return created.Adapt<ProductResponse>();
        }

        public async Task<List<ProductResponse>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync(p=>p.Status==EntityStatus.Active,new string[]
                {
          nameof(Product.Translations),nameof(Product.CreatedBy)
                }
                );
            foreach (var product in products)
            {
                if (product.Translations == null)
                {
                    product.Translations = new List<ProductTranslation>();
                    Console.WriteLine("nulll");
                }
                else
                {
                    Console.WriteLine(" Not nulll");
                }
            }
            // DEBUG

            return products.Adapt<List<ProductResponse>>();
        }

        public async Task<ProductResponse> GetProduct(Expression<Func<Product, bool>> filter)
        {
            var product = await _productRepository.GetOne(filter, new string[]
            {
                    nameof(Product.Translations), nameof(Product.CreatedBy)
            });
            if (product == null)
                return null;

            return product.Adapt<ProductResponse>();
        }
        public async Task<bool> Delete(int id)
        {
            var product = await _productRepository.GetOne(c => c.Id == id);
            if (product != null)
            {
                if (!string.IsNullOrEmpty(product.MainImage))
                {
                    _fileService.Delete(product.MainImage);
                }
                await _productRepository.DeleteAsync(product);
                return true;
            }
            return false;
        }

        public async Task<bool> UpdateProductAsync(int id, ProductUpdateRequest request)
        {

            var product = await _productRepository.GetOne(c => c.Id == id, new string[] { nameof(Product.Translations) });
            if (product == null) return false;

            request.Adapt(product);
            if (request.Translations != null)
            {
                foreach (var translation in request.Translations)
                {
                    var existingTranslation = product.Translations.FirstOrDefault(t => t.Language == translation.Language);
                    if (existingTranslation != null)
                    {
                        if (existingTranslation.Name != null)
                        {
                            existingTranslation.Name = translation.Name;
                        }
                        if (existingTranslation.Description != null)
                        {
                            existingTranslation.Description = translation.Description;
                        }
                        // تحديث أي حقول أخرى حسب الحاجة
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            var oldImage = product.MainImage;
            // تحديث الصورة إذا تم توفير صورة جديدة
            if (request.MainImage != null)
            {
                // حذف الصورة القديمة إذا كانت موجودة
                if (request.MainImage != null)
                {
                    _fileService.Delete(product.MainImage);

                    // رفع الصورة الجديدة وتحديث المسار
                    var imagePath = await _fileService.UploadAsync(request.MainImage);
                    product.MainImage = imagePath;
                }
                else
                {
                    product.MainImage = oldImage; // الاحتفاظ بالصورة القديمة إذا لم يتم توفير صورة جديدة
                }
            }
            return await _productRepository.UpdateAsync(product);
        }

        public async Task<bool> ToggleStatus(int id)
        {
            var product = await _productRepository.GetOne(c => c.Id == id);
            if (product is null) return false;
/*هون:

حسبنا القيمة الجديدة
وخزّناها داخل product.Status
*/
            product.Status = product.Status == EntityStatus.Active ? EntityStatus.InActive : EntityStatus.Active;
            return await _productRepository.UpdateAsync(product);
        }
    }
}
