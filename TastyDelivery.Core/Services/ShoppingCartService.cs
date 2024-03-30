using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
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

        public async Task<CartItemViewModel> AddToCart(int id, double price, int quantity)
        {
            var product = await _repository.AllReadOnly<Product>()
                .Where(p => p.Id == id)
                .Select(p => new CartItemViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Price = price,
                    Quantity = quantity
                })
                .FirstOrDefaultAsync();

            return product;
        }
    }
}
