using KSHOP.DAL.Data.DTO.Request;
using KSHOP.DAL.Data.DTO.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KSHOP.BLL.Service;

public interface ICartService
{
    Task<bool>AddToCartAsync(AddToCart request,string userId);
    Task<List<CartResponse>> GetCartItems(string userId);
    Task<bool> UpdateQuantity(int productId, string userId, int count);
    Task<bool> RemoveItems(int productId, string userId);
    Task<bool> ClearCart(string userId);

}