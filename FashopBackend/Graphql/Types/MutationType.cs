using HotChocolate.Types;

namespace FashopBackend.Graphql.Types;

public class MutationType : ObjectType<Mutation>
{
    protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
    {
        base.Configure(descriptor);
    }
}