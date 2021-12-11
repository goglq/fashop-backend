using FashopBackend.Core.Aggregate.CategoryAggregate;
using FashopBackend.Core.Interfaces;
using HotChocolate;
using HotChocolate.Types;
using System.Collections.Generic;
using FashopBackend.Core.Aggregate.ProductAggregate;
using FashopBackend.Core.Services;

namespace FashopBackend.Graphql.Types
{
    public class CategoryType : ObjectType<Category>
    {
        protected override void Configure(IObjectTypeDescriptor<Category> descriptor)
        {
            base.Configure(descriptor);
            
            Resolvers resolvers = new Resolvers();

            descriptor.Description("Represents category model");

            descriptor
                .Field(c => c.Id)
                .Description("This is unique identifier for product");
            
            descriptor
                .Field(c => c.Name)
                .Description("This is a name of product");

            descriptor
                .Field("products")
                .Resolve(ctx => resolvers.GetProducts(ctx.Parent<Category>(), ctx.Service<IProductService>()))
                .Description("This is list of product for this category");
        }

        private class Resolvers
        {
            public IEnumerable<Product> GetProducts(Category category, [Service] IProductService service)
            {
                return service.GetProductByCategory(category);
            }
        }
    }
}
