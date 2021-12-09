using FashopBackend.Core.Aggregate.CategoryAggregate;
using FashopBackend.Core.Interfaces;
using HotChocolate;
using HotChocolate.Types;
using System.Collections.Generic;

namespace FashopBackend.Graphql.Types
{
    public class CategoryType : ObjectType<Category>
    {
        protected override void Configure(IObjectTypeDescriptor<Category> descriptor)
        {
            base.Configure(descriptor);

            descriptor.Description("Represents category model");
        }

        private class Resolvers
        {
            
        }
    }
}
