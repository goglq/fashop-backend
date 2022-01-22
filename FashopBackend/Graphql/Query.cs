using System;
using FashopBackend.Core.Aggregate.CategoryAggregate;
using FashopBackend.Core.Aggregate.ProductAggregate;
using HotChocolate;
using HotChocolate.Data;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using FashopBackend.Core.Aggregate.BrandAggregate;
using FashopBackend.Core.Aggregate.TokenAggregate;
using FashopBackend.Core.Aggregate.UserAggregate;
using FashopBackend.Core.Interfaces;
using FashopBackend.SharedKernel.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using FashopBackend.Core.Aggregate.CartAggregate;
using FashopBackend.Core.Aggregate.CommercialAggregate;
using FashopBackend.Core.Aggregate.OrderAggregate;
using HotChocolate.Types;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace FashopBackend.Graphql
{
    public class Query
    {
        #region Products
        
        [UsePaging]
        [UseFiltering]
        [UseSorting]
        public IEnumerable<Product> GetProducts([Service] IProductRepository repository) => repository.GetAll();
        
        [UsePaging]
        public IEnumerable<Product> GetRandomProducts([Service] IProductRepository repository) => repository.GetRandomProducts();

        public IEnumerable<Product> SearchProducts(string text, [Service] IProductRepository repository)
        {
            string textLower = text.ToLower();
            return repository.GetAll(product => product.Name.ToLower().Contains(textLower) || product.Description.ToLower().Contains(textLower));
        }

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

        public IEnumerable<Brand> GetUserBrands([Service] IBrandRepository brandRepository, [Service] IUserRepository userRepository, [Service] IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext is null)
                throw new Exception("HttpContext is null");
            
            User user = userRepository.GetUserByToken(httpContextAccessor.HttpContext.Request.Cookies["refreshToken"]);

            if (user is null)
                throw new Exception("Пользователь не авторизован");
            
            return brandRepository.GetAll(brand => brand.UserId == user.Id);
        }

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
            [Service] IUserRepository userRepository, 
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

            var jwt = new JwtSecurityTokenHandler();

            jwt.ValidateToken(refreshToken, new TokenValidationParameters()
            {
                ValidIssuer = refreshTokenSettings.Value.Issuer,
                ValidateIssuer = true,
                ValidAudience = refreshTokenSettings.Value.Audience,
                ValidateAudience = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(refreshTokenSettings.Value.Key)),
                ValidateIssuerSigningKey = true
            }, out SecurityToken validatedToken);

            Console.WriteLine($"token valid to {validatedToken.ValidTo}");
            Console.WriteLine($"now {DateTime.UtcNow}");
            Console.WriteLine($"is token still valid {validatedToken.ValidTo > DateTime.UtcNow}");
            
            if (validatedToken.ValidTo < DateTime.UtcNow)
                throw new Exception("refresh token is not valid");
            
            User user = userRepository.GetUserByToken(refreshToken);
            
            if (user is null)
                throw new AuthenticationException("Refresh Tokens are not matched");

            Tokens tokens = tokenService.GenerateToken(user.Id, user.Email, user.Role.Name, accessTokenSettings.Value, refreshTokenSettings.Value);
            
            httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", tokens.RefreshToken,new CookieOptions()
            {
                HttpOnly= true,
                Expires = DateTimeOffset.Now.AddMonths(4),
                //Domain = "*.herokuapp.com",
                Secure = true,
                Path = "/",
                SameSite = SameSiteMode.None
            });

            user.Token = tokens.RefreshToken;
            userRepository.Update(user);
            userRepository.Save();

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

        public IEnumerable<Cart> GetUserCarts([Service] ICartRepository cartRepository, [Service] IUserRepository userRepository, [Service] IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext is null)
                throw new NullReferenceException("HttpContext is null");

            User user = userRepository.GetUserByToken(httpContextAccessor.HttpContext.Request.Cookies["refreshToken"]);

            if (user is null)
                throw new NullReferenceException("User is null");
            
            return cartRepository.GetAll(cart => cart.UserId == user.Id);
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

        public IEnumerable<Order> GetUserOrders(
            [Service] IOrderRepository orderRepository,
            [Service] IUserRepository userRepository, 
            [Service] IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext is null)
                throw new NullReferenceException("HttpContext is null");
            
            User user = userRepository.GetUserByToken(httpContextAccessor.HttpContext.Request.Cookies["refreshToken"]);


            return orderRepository.GetAll(order => order.UserId == user.Id);
        }

        public Order GetOrder(int id, [Service]IOrderRepository orderRepository)
        {
            return orderRepository.Get(id);
        }

        #endregion

        #region Commercials

        [UsePaging]
        public IEnumerable<Commercial> GetCommercials([Service] ICommercialRepository commercialRepository)
        {
            return commercialRepository.GetAll();
        }

        #endregion
    }
}
