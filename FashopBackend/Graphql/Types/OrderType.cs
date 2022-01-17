using System.Collections.Generic;
using System.Linq;
using FashopBackend.Core.Aggregate.CartAggregate;
using FashopBackend.Core.Aggregate.OrderAggregate;
using FashopBackend.Core.Aggregate.ProductAggregate;
using FashopBackend.Core.Aggregate.UserAggregate;
using HotChocolate;
using HotChocolate.Types;

namespace FashopBackend.Graphql.Types;

public class OrderType : ObjectType<Order>
{
    protected override void Configure(IObjectTypeDescriptor<Order> descriptor)
    {
        base.Configure(descriptor);

        Resolvers resolvers = new Resolvers();

        descriptor
            .Field(_ => _.User)
            .Resolve(ctx => resolvers.GetUser(ctx.Parent<Order>(), ctx.Service<IUserRepository>()));

        descriptor
            .Field(_ => _.Carts)
            .Resolve(ctx => resolvers.GetCarts(ctx.Parent<Order>(), ctx.Service<ICartRepository>()));
    }

    private class Resolvers
    {
        public User GetUser(Order order, [Service] IUserRepository userRepository)
        {
            return userRepository.Get(order.UserId);
        }

        public IEnumerable<Cart> GetCarts(Order order, [Service] ICartRepository cartRepository)
        {
            return cartRepository.GetAll(cart => cart.Order.Id == order.Id);
        }
    }
}