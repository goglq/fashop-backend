using System.Collections.Generic;
using FashopBackend.Core.Aggregate.CategoryAggregate;
using FashopBackend.Core.Aggregate.ProductAggregate;
using FashopBackend.Core.Interfaces;
using FashopBackend.Infrastructure.Data;
using HotChocolate;
using HotChocolate.Types;
namespace FashopBackend.Graphql.Types
{
    public class ProductType : ObjectType<Product>
    {
        protected override void Configure(IObjectTypeDescriptor<Product> descriptor)
        {
            base.Configure(descriptor);

            Resolvers resolvers = new Resolvers();

            descriptor.Description("Represents product model");

            descriptor
                .Field(p => p.Id)
                .Description("This is unique identifier for product");
            
            descriptor
                .Field(p => p.Id)
                .Description("This is a name of product");
            
            descriptor
                .Field(p => p.Categories)
                .Resolve(ctx => resolvers.GetCategories(ctx.Parent<Product>(), ctx.Service<ICategoryService>()))
                .Description("This is list of categories for this product");
        }

        private class Resolvers
        {
            public IEnumerable<Category> GetCategories(Product product, [Service] ICategoryService service)
            {
                return service.GetCategoryByProduct(product);
            }
        }
    }
}
