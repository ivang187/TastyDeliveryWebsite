using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TastyDelivery.Core.Models.AdminModels;
using TastyDelivery.Core.Contracts;
using TastyDelivery.Core.Services.Common;
using TastyDelivery.Infrastructure.Data.Models;
using TastyDelivery.Infrastructure.Data.Models.Enums;
using TastyDelivery.Infrastructure.Data.Models.IdentityModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace TastyDelivery.Core.Services
{
    public class AdminService : IAdminService
    {
        private readonly IRepository repository;
        private readonly UserManager<ApplicationUser> userManager;
        
        public AdminService(IRepository _repository, UserManager<ApplicationUser> _userManager) 
        {
            repository = _repository;
            userManager = _userManager;
        }
        public void CreateRestaurant(AddRestaurantFormViewModel model)
        {
            var restaurant = NewRestaurant(model);

            repository.AddNew(restaurant);
            repository.SaveChanges();
        }

        private Restaurant NewRestaurant(AddRestaurantFormViewModel model)
        {
            return new Restaurant
            {
                Name = model.Name,
                Location = model.Location,
                Type = model.Type,
                WorkingHours = model.WorkingHours
            };
        }

        public ProductsRestaurants CreateProduct(int restaurantId, int productId, string name, string description, ProductCategory category, double price)
        {
            if (productId == 0)
            {
                var product = new Product { Name = name, Category = category, Description = description };
                var model = NewProduct(product, restaurantId, price);
                repository.AddNew(model);
                repository.SaveChanges();
                return model;
            }
            else
            {
                var product = repository.AllReadOnly<Product>().FirstOrDefault(p => p.Id == productId);

                var model = ExistingProduct(product, restaurantId, price);

                if (!repository.AllReadOnly<ProductsRestaurants>().Contains(model))
                {
                    repository.AddNew(model);
                }

                product.Category = category;
                product.Description = description;
                product.Name = name;
                model.Price = price;

                repository.Update(product);
                repository.SaveChanges();

                return model;
            }
        }

        private ProductsRestaurants ExistingProduct(Product product, int restaurantId, double price)
        {
            var productRestaurants = repository.AllReadOnly<ProductsRestaurants>()
                .Select(pr => new ProductsRestaurants
                {
                    Product = product,
                    ProductId = product.Id,
                    Price = price,
                    RestaurantId = restaurantId
                }).FirstOrDefault();

            return productRestaurants;
        }

        private ProductsRestaurants NewProduct(Product product, int restaurantId, double price)
        {
            var productRestaurants = new ProductsRestaurants
            {
                RestaurantId = restaurantId,
                Product = product,
                Price = price,
            };

            return productRestaurants;
        }

        public async Task CreateDriver(AppointDriverModel model)
        {
            var user = await userManager.FindByEmailAsync(model.Email);

            if (user == null)
            {
                user = CreateNewDriver(model);
                repository.AddNew(user);
            }
            else
            {
                await UpdateUserRole(user);
                user.Role = UserRole.DeliveryMan;
                user.HomeAddress = null;
                repository.Update(user);
            }

            await repository.SaveChanges();
        }

        private async Task UpdateUserRole(ApplicationUser user)
        {
            var existingRoles = await userManager.GetRolesAsync(user);
            var removeFromRoles = await userManager.RemoveFromRolesAsync(user, existingRoles);

            var addToRoleResult = await userManager.AddToRoleAsync(user, UserRole.DeliveryMan.ToString());
        }

        private ApplicationUser CreateNewDriver(AppointDriverModel model)
        {
            var user = new ApplicationUser
            {
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber,
                Role = UserRole.DeliveryMan,
                UserName = model.Email,
                NormalizedEmail = model.Email.ToUpper(),
                NormalizedUserName = model.Email.ToUpper()
            };

            var passwordHasher = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHasher.HashPassword(user, model.Password);

            userManager.AddToRoleAsync(user, UserRole.DeliveryMan.ToString());

            return user;
        }

        public List<CompletedDeliveriesAdminViewModel> GetCompletedDeliveries()
        {
            var orders = repository.AllReadOnly<Order>().Where(o => o.Status == DeliveryStatus.Delivered).ToList();

            var model = new List<CompletedDeliveriesAdminViewModel>();

            if(orders.Any())
            {
                foreach (var order in orders)
                {
                    var item = CreateCompleteDeliveryViewModel(order);
                    model.Add(item);
                }

                return model;
            }

            return null;
            
        }

        private CompletedDeliveriesAdminViewModel CreateCompleteDeliveryViewModel(Order order)
        {
            var deliveryMan = repository.AllReadOnly<ApplicationUser>().FirstOrDefault(u => u.Id == order.DeliveryManId);
            var restaurantName = repository.AllReadOnly<Restaurant>().Where(r => r.Id == order.RestaurantId).Select(r => r.Name).FirstOrDefault();
            var user = repository.AllReadOnly<ApplicationUser>().FirstOrDefault(u => u.Id == order.UserId);

            return new CompletedDeliveriesAdminViewModel
            {
                DeliveryMan = deliveryMan,
                DeliveryManName = deliveryMan.FirstName + " " + deliveryMan.LastName,
                TimeDelivered = order.TimeDelivered,
                TimeOrdered = order.TimeOrdered,
                Order = order,
                OrderId = order.Id,
                RestaurantName = restaurantName,
                User = user,
                UserName = user.FirstName + " " + user.LastName,
            };
        }

        public ProductsRestaurants GetProductById(int id)
        {
            var item = repository.AllReadOnly<ProductsRestaurants>()
                .Include(p => p.Product)
                .Include(r => r.Restaurant)
                .FirstOrDefault(p => p.ProductId == id);

            return item;
        }

        public void DeleteProduct(ProductsRestaurants product)
        {
            repository.Delete(product);
            repository.SaveChanges();
        }

        public List<PendingDeliveriesViewModel> GetPendingDeliveries()
        {
            var model = repository.AllReadOnly<Order>()
                .Include(r => r.Restaurant)
                .Include(u => u.User)
                .Where(o => o.Status == DeliveryStatus.Pending)
                .Select(o => new PendingDeliveriesViewModel
                {
                    Order = o,
                    OrderId = o.Id,
                    TimeOrdered = o.TimeOrdered,
                    Customer = o.User,
                    CustomerName = o.User.FirstName + " " + o.User.LastName,
                    Restaurant = o.Restaurant,
                    RestaurantName = o.Restaurant.Name
                })
                .ToList();

            return model;
        }
    }
}
