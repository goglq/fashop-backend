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
            IEnumerable<Product> products = productService.GetProductsWithId(input.productIds.ToArray());
            Category category = categoryService.EditCategory(input.id, input.name, products);
            return new EditCategoryPayload(category);
        }
    }
}
