using KSHOP.DAL.Data.DTO.Request;
using KSHOP.DAL.Data.DTO.Response;
using KSHOP.DAL.Models;
using KSHOP.DAL.Repository;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace KSHOP.BLL.Service;

public class CartService : ICartService
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductRepository _productRepository;

    public CartService(ICartRepository cartRepository,IProductRepository productRepository)
    {
        _cartRepository = cartRepository;
        _productRepository = productRepository;
    }


    public async Task<bool> AddToCartAsync(AddToCart request, string userId)
    {
        var product = await _productRepository.GetOne(p=>p.Quantity==request.ProductId);
        if (product == null) return false;
        var existingCartItem = await _cartRepository.GetOne(c => c.UserId == userId && c.ProductId == request.ProductId);
        if (existingCartItem != null)
        {
            var currentCount = existingCartItem?.Count ?? 0;
            var newCount= currentCount+request.Count;
            if (newCount > product.Quantity) return false;

            existingCartItem.Count = newCount;
            await _cartRepository.UpdateAsync(existingCartItem);
        }
        else
        {
            var cartItem = request.Adapt<Cart>();
            cartItem.UserId = userId;
            await _cartRepository.CreateAsync(cartItem);

        }
        return true;

    }

    public Task<bool> ClearCart(string userId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<CartResponse>> GetCartItems(string userId)
    {
        var cartItems = await _cartRepository.GetAllAsync(
            c => c.UserId == userId,
            new string[] { nameof(Cart.Product) }
        );

        return cartItems.Adapt<List<CartResponse>>();
    }


    public Task<bool> RemoveItems(int productId, string userId)
    {
        throw new NotImplementedException();
    }

    public Task<bool> UpdateQuantity(int productId, string userId, int count)
    {
        throw new NotImplementedException();
    }
}
