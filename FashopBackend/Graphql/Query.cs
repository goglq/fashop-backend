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

        public string AccessToken(
            string refreshToken, 
            [Service] IUserRepository repository, 
            [Service] ITokenService tokenService, 
            [Service] IOptions<AccessTokenSettings> accessTokenSettings,
            [Service] IOptions<RefreshTokenSettings> refreshTokenSettings
            )
        {
            User user = repository.GetUserByToken(refreshToken);
            if (user.Token != refreshToken)
                throw new AuthenticationException("Refresh Tokens are not matched");
        
            Tokens tokens = tokenService.GenerateToken(user.Id, user.Email, user.Role.Name, accessTokenSettings.Value, refreshTokenSettings.Value);
            return tokens.AccessToken;
        }
    }
}
