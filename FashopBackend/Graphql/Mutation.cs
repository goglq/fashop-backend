using System;
using FashopBackend.Core.Aggregate.CategoryAggregate;
using FashopBackend.Core.Aggregate.ProductAggregate;
using FashopBackend.Core.Interfaces;
using FashopBackend.Graphql.Categories;
using FashopBackend.Graphql.Products;
using HotChocolate;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using FashopBackend.Core.Aggregate.BrandAggregate;
using FashopBackend.Core.Aggregate.BrandImageAggregate;
using FashopBackend.Core.Aggregate.CartAggregate;
using FashopBackend.Core.Aggregate.OrderAggregate;
using FashopBackend.Core.Aggregate.ProductImageAggregate;
using FashopBackend.Core.Aggregate.RoleAggregate;
using FashopBackend.Core.Aggregate.TokenAggregate;
using FashopBackend.Core.Aggregate.UserAggregate;
using FashopBackend.Core.Error;
using FashopBackend.Graphql.Brands;
using FashopBackend.Graphql.Carts;
using FashopBackend.Graphql.Orders;
using FashopBackend.Graphql.Users;
using FashopBackend.SharedKernel.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
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
                Description = input.Descriptions,
                Price = input.Price,
                Sale = input.Sale,
                Categories = categories.ToList(),
                BrandId = input.BrandId,
                ProductImages = input.ImageUrls.Select(imageUrl => new ProductImage() {Url = imageUrl}).ToList()
            };

            await productService.Create(product);

            return new AddProductPayload(product);
        }

        public EditProductPayload EditProduct(EditProductInput input, [Service] IProductService productService, [Service] ICategoryService categoryService, [Service] IBrandRepository repository)
        {
            IEnumerable<Category> categories = categoryService.GetByIds(input.CategoryIds.ToArray());
            Brand brand = repository.Get(input.BrandId);
            IEnumerable<ProductImage> productImages =
                input.ImageUrls.Select(imageUrl => new ProductImage() {Url = imageUrl});
            Product product = productService.Edit(input.Id, input.Name, input.Descriptions, input.Price, input.Sale, brand, categories, productImages);
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
                Name = input.Name,
                BrandImage = new BrandImage()
                {
                    Thumbnail = input.Thumbnail,
                    Header = input.Header
                }
            };
            
            await repository.Create(brand);
            await repository.SaveAsync();
            
            return new AddBrandPayload(brand);
        }

        public EditBrandPayload EditBrand(EditBrandInput input, [Service] IBrandRepository repository)
        {
            Brand brand = repository.Get(input.Id);
            brand.Name = input.Name;
            repository.Update(brand);
            repository.Save();
            return new EditBrandPayload(brand);
        }
        
        public DeleteBrandPayload DeleteBrand(DeleteBrandInput input, [Service] IBrandRepository repository)
        {
            Brand brand = repository.Get(input.BrandId);
            repository.Remove(brand);
            repository.Save();
            return new DeleteBrandPayload(brand.Id);
        }

        #endregion

        #region User Mutations

        public async Task<RegisterUserPayload> RegisterUser(RegsiterUserInput input, [Service] IUserRepository userRepository, [Service]  IRoleRepository roleRepository)
        {
            var emailValidator = new EmailAddressAttribute();

            if (!emailValidator.IsValid(input.Email))
                throw new Exception("почта не подходит");
            
            if (string.IsNullOrWhiteSpace(input.Password))
                throw new Exception("заполните пароль");

            User candidate = userRepository.GetAll().Find(u => u.Email == input.Email);
            Role role = roleRepository.GetAll().First(u => u.Name == "user");

            if (candidate is not null)
                throw new Exception("User already exists");

            string hashedPassword = BC.HashPassword(input.Password);

            User user = new User()
            {
                Email = input.Email,
                Password = hashedPassword,
                RoleId = role.Id,
                IsEmailVerified = false
            };

            await userRepository.Create(user);
            await userRepository.SaveAsync();

            return new RegisterUserPayload(user);
        }
        
        public LoginUserPayload LoginUser(
            LoginUserInput input,
            [Service] IUserRepository userRepository, 
            [Service] ITokenService tokenService,
            [Service] IOptions<AccessTokenSettings> accessTokenSettings,
            [Service] IOptions<RefreshTokenSettings> refreshTokenSettings,
            [Service] IHttpContextAccessor httpContextAccessor
            )
        {
            var emailValidator = new EmailAddressAttribute();

            if (!emailValidator.IsValid(input.Email))
                throw new Exception("почта не подходит");
            
            if (string.IsNullOrWhiteSpace(input.Password))
                throw new Exception("заполните пароль");
            
            User user = userRepository.GetUsersWithRoles().Find(u => u.Email == input.Email);

            if (user is null)
                throw new NotRegisteredEmail();

            if (!BC.Verify(input.Password, user.Password))
                throw new NotMatchingPasswordException();

            Tokens tokens = tokenService.GenerateToken(user.Id, user.Email, user.Role.Name, accessTokenSettings.Value, refreshTokenSettings.Value);
            user.Token = tokens.RefreshToken;
            
            if (httpContextAccessor.HttpContext is not null)
            {
                httpContextAccessor.HttpContext.Response.Cookies.Append("refreshToken", tokens.RefreshToken,new CookieOptions()
                {
                    HttpOnly= true,
                    Expires = DateTimeOffset.Now.AddMonths(4),
                    //Domain = "*.herokuapp.com",
                    Secure = true,
                    Path = "/",
                    SameSite = SameSiteMode.None
                });
            }
            userRepository.Update(user);
            userRepository.Save();

            return new LoginUserPayload(tokens);
        }

        public LogoutUserPayload Logout([Service] IUserRepository userRepository, [Service] IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext is null)
                throw new NullReferenceException("HttpContext is null");
            
            User user = userRepository.GetUserByToken(
                httpContextAccessor.HttpContext.Request.Cookies["refreshToken"]);

            if (user is null)
                throw new NullReferenceException("User not found by token");
            
            user.Token = "";
            
            userRepository.Update(user);
            userRepository.Save();

            return new LogoutUserPayload(user.Id);
        }

        #endregion

        #region Cart Mutations

        public AddCartPayload AddCart(AddCartInput input,[Service] ICartRepository cartRepository, [Service] IUserRepository userRepository, [Service] IProductRepository productRepository, [Service] IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext is null)
                throw new NullReferenceException("HttpContext is null");

            User user = userRepository.GetUserByToken(httpContextAccessor.HttpContext.Request.Cookies["refreshToken"]);

            if (user is null)
                throw new NullReferenceException("User is null");
            
            Product product = productRepository.Get(input.ProductId);

            if (product is null)
                throw new NullReferenceException("Product not exists");

            
            Cart cart = cartRepository.GetCartByUserAndProductId(user.Id, product.Id);

            if (cart is not null)
            {
                cart.Count++;
                cartRepository.Update(cart);
            }
            else
            {
                cart = new Cart()
                {
                    Count = input.Count,
                    UserId = user.Id,
                    ProductId = product.Id,
                };

                cartRepository.Create(cart);
            }

            
            cartRepository.Save();

            return new AddCartPayload(cart);
        }

        public DeleteCartPayload DeleteCart(int id, [Service] ICartRepository cartRepository)
        {
            Cart cart = cartRepository.Get(id);

            if (cart is null)
                throw new NullReferenceException("Cart not exists");
            
            cartRepository.Remove(cart);
            cartRepository.Save();
            return new DeleteCartPayload(id);
        }

        #endregion

        #region Order Mutations

        public AddOrderPayload AddOrder(AddOrderInput input, [Service] ICartRepository cartRepository, [Service] IOrderRepository orderRepository, [Service] IUserRepository userRepository, [Service] IHttpContextAccessor httpContextAccessor)
        {
            if (httpContextAccessor.HttpContext is null)
                throw new NullReferenceException("HttpContext is null");

            User user = userRepository.GetUserByToken(httpContextAccessor.HttpContext.Request.Cookies["refreshToken"]);

            List<Cart> carts = cartRepository.GetAll(cart => input.CartIds.Contains(cart.Id));
            
            Order order = new Order()
            {
                OrderStatusId = OrderStatusId.Confirming,
                Address = input.Address,
                UserId = user.Id,
                Carts = carts
            };

            orderRepository.Create(order);
            orderRepository.Save();

            return new AddOrderPayload(order);
        }
        
        public DeleteOrderPayload DeleteOrder(int id, [Service] IOrderRepository orderRepository)
        {
            Order order = orderRepository.Get(id);

            if (order is null)
                throw new NullReferenceException("Order not exists");

            orderRepository.Remove(order);
            orderRepository.Save();
            
            return new DeleteOrderPayload(id);
        }
        

        #endregion
    }
}
