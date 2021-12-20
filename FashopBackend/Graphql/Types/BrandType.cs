using FashopBackend.Core.Aggregate.BrandAggregate;
using HotChocolate.Types;

namespace FashopBackend.Graphql.Types;

public class BrandType : ObjectType<Brand>
{
    protected override void Configure(IObjectTypeDescriptor<Brand> descriptor)
    {
        base.Configure(descriptor);
    }
}