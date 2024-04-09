using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Runtime.InteropServices;
using System.Security.Claims;
using TastyDelivery.Core.Contracts;
using TastyDelivery.Core.Models.ShoppingCart;
using TastyDelivery.Core.Services.Common;
using TastyDelivery.Infrastructure.Data.Models;

namespace TastyDelivery.Core.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository _repository;

        public ShoppingCartService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<CartItemViewModel> FindItemToAdd(int id, double price, int quantity)
        {
            return await _repository.AllReadOnly<ProductsRestaurants>()
                .Where(p => p.ProductId == id)
                .Select(p => new CartItemViewModel
                {
                    RestaurantName = p.Restaurant.Name,
                    Id = p.ProductId,
                    Name = p.Product.Name,
                    Price = price,
                    Quantity = quantity
                })
                .FirstOrDefaultAsync();
        }

        public async Task<CartItemViewModel> FindItemToRemove(int id)
        {
            return await _repository.AllReadOnly<Product>()
                .Where(p => p.Id == id)
                .Select (p => new CartItemViewModel
                {
                    Id = p.Id,
                })
                .FirstOrDefaultAsync();
        }
    }
}
