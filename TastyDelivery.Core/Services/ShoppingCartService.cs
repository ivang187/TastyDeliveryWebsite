using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System.Security.Claims;
using TastyDelivery.Core.Contracts;
using TastyDelivery.Core.Models.ShoppingCart;
using TastyDelivery.Core.Services.Common;
using TastyDelivery.Infrastructure.Data.Models;
using TastyDelivery.Infrastructure.Migrations;

namespace TastyDelivery.Core.Services
{
    public class ShoppingCartService : IShoppingCartService
    {
        private readonly IRepository _repository;

        public ShoppingCartService(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<CartItemViewModel> AddToCart(int id, double price, int quantity)
        {
            return await _repository.AllReadOnly<Product>()
                .Where(p => p.Id == id)
                .Select(p => new CartItemViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = price,
                    Quantity = quantity
                })
                .FirstOrDefaultAsync();
        }
    }
}
