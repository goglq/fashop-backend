using System;
using FashopBackend.Core.Aggregate.CategoryAggregate;
using FashopBackend.Core.Aggregate.ProductAggregate;
using FashopBackend.Core.Interfaces;
using FashopBackend.Graphql.Categories;
using FashopBackend.Graphql.Products;
using HotChocolate;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashopBackend.Core.Aggregate.BrandAggregate;
using FashopBackend.Core.Aggregate.UserAggregate;
using FashopBackend.Graphql.Brands;
using FashopBackend.Graphql.Users;
using FashopBackend.Shared;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using BC = BCrypt.Net.BCrypt;

namespace FashopBackend.Graphql
{
    public class Mutation
    {
        #region Product Mutations

        public async Task<AddProductPayload> AddProduct(AddProductInput input, [Service] IProductService productService, [Service] ICategoryService categoryService)
        {
            IEnumerable<Category> categories = categoryService.GetByIds(input.CategoryIds.ToArray());

            Product product = new Product()
            {
                Name = input.Name,
                Categories = categories.ToList()
            };

            await productService.Create(product);

            return new AddProductPayload(product);
        }

        public EditProductPayload EditProduct(EditProductInput input, [Service] IProductService productService, [Service] ICategoryService categoryService)
        {
            IEnumerable<Category> categories = categoryService.GetByIds(input.CategoryIds.ToArray());
            Product product = productService.Edit(input.Id, input.Name, categories);
            return new EditProductPayload(product);
        }

        public DeleteProductPayload DeleteProduct(DeleteProductInput input, [Service] IProductService service)
        {
            int id = service.Delete(input.Id);
            return new DeleteProductPayload(id);
        }

        #endregion

        #region Category Mutations

        public async Task<AddCategoryPayload> AddCategory(AddCategoryInput input, [Service]ICategoryService service)
        {
            var category = new Category()
            {
                Name = input.Name
            };

            await service.Create(category);

            return new AddCategoryPayload(category);
        }

        public EditCategoryPayload EditCategory(EditCategoryInput input, [Service] ICategoryService categoryService, [Service] IProductService productService)
        {
            Category category = categoryService.Edit(input.Id, input.Name);
            return new EditCategoryPayload(category);
        }
        
        public DeleteCategoryPayload DeleteCategory(DeleteCategoryInput input, [Service] ICategoryService service)
        {
            int id = service.Delete(input.Id);
            return new DeleteCategoryPayload(id);
        }

        #endregion

        #region Brand Mutations

        public async Task<AddBrandPayload> AddBrand(AddBrandInput input, [Service] IBrandRepository repository)
        {
            Brand brand = new Brand()
            {
                Name = input.Name
            };
            
            await repository.Create(brand);
            await repository.SaveAsync();
            
            return new AddBrandPayload(brand);
        }

        public EditBrandPayload AddBrand(EditBrandInput input, [Service] IBrandRepository repository)
        {
            Brand brand = repository.Get(input.Id);
            brand.Name = input.Name;
            repository.Update(brand);
            repository.Save();
            return new EditBrandPayload(brand);
        }

        #endregion

        #region User Mutations

        public async Task<RegisterUserPayload> RegisterUser(RegsiterUserInput input, [Service] IUserRepository userRepository)
        {
            User candidate = userRepository.GetAll().Find(u => u.Email == input.Email);

            if (candidate is not null)
                throw new Exception("User already exists");

            string hashedPassword = BC.HashPassword(input.Password);

            User user = new User()
            {
                Email = input.Email,
                Password = hashedPassword,
                IsEmailVerified = false
            };

            return new RegisterUserPayload(user);
        }
        
        public LoginUserPayload LoginUser(LoginUserInput input, [Service]IOptions<TokenSettings> tokenSettings, [Service] IUserRepository userRepository)
        {
            User user = userRepository.GetAll().Find(u => u.Email == input.Email);
            
            if (user is null) 
                throw new Exception("Not authorized");

            if (!BC.Verify(input.Password, user.Password))
                throw new Exception("Invalid Password");
            
            SymmetricSecurityKey symmetricSecurityKey = new (Encoding.UTF8.GetBytes(tokenSettings.Value.Key));
            SigningCredentials credentials = new (symmetricSecurityKey, SecurityAlgorithms.HmacSha256);
 
            JwtSecurityToken jwtToken = new (
                issuer: tokenSettings.Value.Issuer,
                audience: tokenSettings.Value.Audience,
                expires: DateTime.Now.AddMinutes(20),
                signingCredentials: credentials
            );
 
            string token = new JwtSecurityTokenHandler().WriteToken(jwtToken);

            return new LoginUserPayload(token);
        }

        #endregion
    }
}
