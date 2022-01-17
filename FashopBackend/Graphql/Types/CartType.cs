using System;
using FashopBackend.Core.Aggregate.CartAggregate;
using FashopBackend.Core.Aggregate.ProductAggregate;
using FashopBackend.Core.Aggregate.UserAggregate;
using HotChocolate;
using HotChocolate.Types;

namespace FashopBackend.Graphql.Types;

public class CartType : ObjectType<Cart>
{
    protected override void Configure(IObjectTypeDescriptor<Cart> descriptor)
    {
        base.Configure(descriptor);

        Resolvers resolvers = new Resolvers();

        descriptor
            .Field(_ => _.User)
            .Resolve(ctx => resolvers.GetUser(ctx.Parent<Cart>(), ctx.Service<IUserRepository>()));
        
        descriptor
            .Field(_ => _.Product)
            .Resolve(ctx => resolvers.GetProduct(ctx.Parent<Cart>(), ctx.Service<IProductRepository>()));
    }

    private class Resolvers
    {
        public User GetUser(Cart cart, [Service] IUserRepository userRepository)
        {
            Console.WriteLine(cart.UserId);
            return userRepository.Get(cart.UserId);
        }

        public Product GetProduct(Cart cart, [Service] IProductRepository productRepository)
        {
            return productRepository.Get(cart.ProductId);
        }
    }
}