using System.Collections.Generic;
using FashopBackend.Core.Aggregate.BrandAggregate;
using FashopBackend.Core.Aggregate.BrandImageAggregate;
using FashopBackend.Core.Aggregate.ProductAggregate;
using FashopBackend.Core.Interfaces;
using HotChocolate;
using HotChocolate.Types;

namespace FashopBackend.Graphql.Types;

public class BrandType : ObjectType<Brand>
{
    protected override void Configure(IObjectTypeDescriptor<Brand> descriptor)
    {
        base.Configure(descriptor);

        Resolvers resolvers = new Resolvers();

        descriptor
            .Field(_ => _.Products)
            .Resolve(ctx => resolvers.GetProducts(ctx.Parent<Brand>(), ctx.Service<IProductService>()));

        descriptor
            .Field(_ => _.BrandImageId)
            .Ignore();

        descriptor
            .Field(_ => _.BrandImage)
            .Resolve(ctx => resolvers.GetBrandImage(ctx.Parent<Brand>(), ctx.Service<IBrandImageRepository>()));
    }

    private class Resolvers
    {
        public IEnumerable<Product> GetProducts(Brand parent, [Service]IProductService service)
        {
            return service.GetByBrand(parent);
        }

        public BrandImage GetBrandImage(Brand parent, [Service]IBrandImageRepository brandImageRepository)
        {
            return brandImageRepository.GetByBrandId(parent.BrandImageId);
        }
    }
}