using System;
using FashopBackend.Core.Aggregate.CategoryAggregate;
using FashopBackend.Core.Aggregate.ProductAggregate;
using HotChocolate;
using HotChocolate.Data;
using System.Collections.Generic;
using System.Linq;
using System.Security.Authentication;
using FashopBackend.Core.Aggregate.BrandAggregate;
using FashopBackend.Core.Aggregate.TokenAggregate;
using FashopBackend.Core.Aggregate.UserAggregate;
using FashopBackend.Core.Interfaces;
using FashopBackend.SharedKernel.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using FashopBackend.Core.Aggregate.CartAggregate;
using FashopBackend.Core.Aggregate.OrderAggregate;

namespace FashopBackend.Graphql
{
    public class Query
    {
        #region Products

        [UseFiltering]
        [UseSorting]
        public IEnumerable<Product> GetProducts([Service] IProductRepository repository) => repository.GetAll();

        public Product GetProduct(int id, [Service]IProductRepository repository) => repository.Get(id);

        #endregion

        #region Categories

        [UseFiltering]
        [UseSorting]
        public IEnumerable<Category> GetCategories([Service] ICategoryRepository repository) => repository.GetAll();

        public Category GetCategory(int id, [Service] ICategoryRepository repository) => repository.Get(id);

        #endregion

        #region Brands

        [UseFiltering]
        [UseSorting]
        public IEnumerable<Brand> GetBrands([Service] IBrandRepository repository) => repository.GetAll();

        public Brand GetBrand(int id, [Service] IBrandRepository repository) => repository.Get(id);

        #endregion

        #region Users

        [UseFiltering]
        [UseSorting]
        public IEnumerable<User> GetUsers([Service] IUserRepository repository) => repository.GetAll();

        public User GetUser(int id, [Service] IUserRepository repository) => repository.Get(id);

        public User GetSelf([Service] IUserRepository repository, [Service] IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext is null)
                throw new Exception("Http Context is null");

            Console.WriteLine($"refresh token: {httpContextAccessor.HttpContext.Request.Cookies["refreshToken"]}");
            
            User user = repository.GetUserByToken(httpContextAccessor.HttpContext.Request.Cookies["refreshToken"]);

            return user;
        }
        
        #endregion

        #region Tokens
        public string AccessToken(
            [Service] IUserRepository repository, 
            [Service] ITokenService tokenService, 
            [Service] IOptions<AccessTokenSettings> accessTokenSettings,
            [Service] IOptions<RefreshTokenSettings> refreshTokenSettings,
            [Service] IHttpContextAccessor httpContextAccessor
        )
        {
            if (httpContextAccessor.HttpContext is null)
            {
                throw new Exception("HttpContext is null");
            }
            string refreshToken = httpContextAccessor.HttpContext.Request.Cookies["refreshToken"];

            User user = repository.GetUserByToken(refreshToken);
            if (user is null)
                throw new AuthenticationException("Refresh Tokens are not matched");
        
            Tokens tokens = tokenService.GenerateToken(user.Id, user.Email, user.Role.Name, accessTokenSettings.Value, refreshTokenSettings.Value);
            return tokens.AccessToken;
        }

        public string RefreshToken([Service]IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor is null)
            {
                return "http context accessor is null";
            }
            if (httpContextAccessor.HttpContext is null)
            {
                return "context is null";
            }
            foreach(string key in httpContextAccessor.HttpContext.Request.Cookies.Keys)
            {
                Console.WriteLine(key);
            }
            if (httpContextAccessor.HttpContext.Request.Cookies["refreshToken"] is null)
            {
                return "refreshToken is not in cookie";
            }
            return httpContextAccessor.HttpContext.Request.Cookies["refreshToken"];
        }

        #endregion

        #region Carts
        
        public IEnumerable<Cart> GetCarts([Service] ICartRepository cartRepository)
        {
            return cartRepository.GetAll();
        }

        public Cart GetCart(int id, [Service]ICartRepository cartRepository)
        {
            return cartRepository.Get(id);
        }

        #endregion
        
        #region Orders
        
        public IEnumerable<Order> GetOrders([Service] IOrderRepository orderRepository)
        {
            return orderRepository.GetAll();
        }

        public Order GetOrder(int id, [Service]IOrderRepository orderRepository)
        {
            return orderRepository.Get(id);
        }

        #endregion
    }
}
