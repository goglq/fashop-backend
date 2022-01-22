using HotChocolate.Types;

namespace FashopBackend.Graphql.Types;

public class MutationType : ObjectType<Mutation>
{
    protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
    {
        base.Configure(descriptor);

        descriptor
            .Field(_ => _.AddBrand(default, default, default, default));

        descriptor
            .Field(_ => _.EditBrand(default, default));

        descriptor
            .Field(_ => _.DeleteBrand(default, default));

        descriptor
            .Field(_ => _.AddProduct(default, default, default, default, default, default, default));

        descriptor
            .Field(_ => _.EditProduct(default, default, default, default));

        descriptor
            .Field(_ => _.DeleteProduct(default, default));

        descriptor
            .Field(_ => _.AddCategory(default, default))
            .Authorize("admin");
        
        descriptor
            .Field(_ => _.EditCategory(default, default, default))
            .Authorize("admin");
        
        descriptor
            .Field(_ => _.DeleteCategory(default, default))
            .Authorize("admin");
    }
}