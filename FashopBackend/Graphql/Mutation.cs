using FashopBackend.Core.Aggregate.CategoryAggregate;
using FashopBackend.Core.Aggregate.ProductAggregate;
using FashopBackend.Core.Interfaces;
using FashopBackend.Graphql.Categories;
using FashopBackend.Graphql.Products;
using HotChocolate;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FashopBackend.Graphql
{
    public class Mutation
    {
        public async Task<AddProductPayload> AddProduct(AddProductInput input, [Service] IProductService service)
        {
            var product = new Product()
            {
                Name = input.Name
            };

            await service.CreateProduct(product);

            return new AddProductPayload(product);
        }

        public EditProductPayload EditProduct(EditCategoryInput input, [Service] IProductService productService, [Service] ICategoryService categoryService)
        {
            IEnumerable<Category> categories = categoryService.GetCategoryByIds(input.ProductIds.ToArray());
            Product product = productService.EditProduct(input.Id, input.Name, categories);
            return new EditProductPayload(product);
        }

        public async Task<AddCategoryPayload> AddCategory(AddCategoryInput input, [Service]ICategoryService service)
        {
            var category = new Category()
            {
                Name = input.Name
            };

            await service.CreateCategory(category);

            return new AddCategoryPayload(category);
        }

        public EditCategoryPayload EditCategory(EditCategoryInput input, [Service] ICategoryService categoryService, [Service] IProductService productService)
        {
            IEnumerable<Product> products = productService.GetProductsWithId(input.ProductIds.ToArray());
            Category category = categoryService.EditCategory(input.Id, input.Name, products);
            return new EditCategoryPayload(category);
        }
    }
}
