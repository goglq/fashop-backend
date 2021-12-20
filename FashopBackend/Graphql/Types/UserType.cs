using FashopBackend.Core.Aggregate.RoleAggregate;
using FashopBackend.Core.Aggregate.UserAggregate;
using HotChocolate;
using HotChocolate.Types;

namespace FashopBackend.Graphql.Types;

public class UserType : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        base.Configure(descriptor);

        Resolvers resolvers = new Resolvers();

        descriptor
            .Field(_ => _.Password)
            .Ignore();
        
        descriptor
            .Field(_ => _.Token)
            .Ignore();

        descriptor
            .Field(_ => _.Role)
            .Resolve(ctx => resolvers.GetRole(ctx.Parent<User>(), ctx.Service<IRoleRepository>()))
            .Description("User role");
    }

    private class Resolvers
    {
        public Role GetRole(User user, [Service] IRoleRepository roleRepository)
        {
            return roleRepository.Get(user.RoleId);
        }
    }
}