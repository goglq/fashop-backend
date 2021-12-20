using HotChocolate.Types;

namespace FashopBackend.Graphql.Types;

public class QueryType : ObjectType<Query>
{
    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        base.Configure(descriptor);

        descriptor
            .Field(_ => _.GetProduct(default, default))
            .Description("Gets Product By Id");

        descriptor
            .Field(_ => _.GetUser(default, default))
            .Authorize(new []{"admin"});
        
        descriptor
            .Field(_ => _.GetUsers(default))
            .Authorize(new []{"admin"});
    }
}