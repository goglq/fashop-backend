using FashopBackend.Core.Aggregate.CategoryAggregate;
using FashopBackend.Core.Aggregate.ProductAggregate;
using FashopBackend.Core.Interfaces;
using HotChocolate;
using HotChocolate.Types;
using System;
using System.Collections.Generic;

namespace FashopBackend.Graphql.Types
{
    public class ProductType : ObjectType<Product>
    {
        protected override void Configure(IObjectTypeDescriptor<Product> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Description("Represents product model");

            //descriptor
            //    .Field(p => p.Categories)
            //    .ResolveWith<Resolvers>(p => p.GetCategories(default!, default))
            //    .Description("This is list of categories for this product");
        }

        private class Resolvers
        {
            public IEnumerable<Category> GetCategories(Product product, [Service] ICategoryRepository service)
            {
                return service.GetAll();
            }
        }
    }
}
