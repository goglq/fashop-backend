using System;
using FashopBackend.Core.Aggregate.CategoryAggregate;
using FashopBackend.Core.Aggregate.ProductAggregate;
using HotChocolate;
using HotChocolate.Data;
using System.Collections.Generic;
using System.Security.Authentication;
using FashopBackend.Core.Aggregate.BrandAggregate;
using FashopBackend.Core.Aggregate.TokenAggregate;
using FashopBackend.Core.Aggregate.UserAggregate;
using FashopBackend.Core.Interfaces;
using FashopBackend.SharedKernel.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace FashopBackend.Graphql
{
    public class Query
    {
        [UseFiltering]
        [UseSorting]
        public IEnumerable<Product> GetProducts([Service] IProductRepository repository) => repository.GetAll();

        public Product GetProduct(int id, [Service]IProductRepository repository) => repository.Get(id);

        [UseFiltering]
        [UseSorting]
        public IEnumerable<Category> GetCategories([Service] ICategoryRepository repository) => repository.GetAll();

        public Category GetCategory(int id, [Service] ICategoryRepository repository) => repository.Get(id);

        [UseFiltering]
        [UseSorting]
        public IEnumerable<Brand> GetBrands([Service] IBrandRepository repository) => repository.GetAll();

        public Brand GetBrand(int id, [Service] IBrandRepository repository) => repository.Get(id);

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
    }
}
