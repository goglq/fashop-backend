using System.Collections.Generic;
using System.Linq;
using FashopBackend.Core.Aggregate.CartAggregate;
using FashopBackend.Core.Aggregate.OrderAggregate;
using FashopBackend.Core.Aggregate.OrderedProductAggregate;
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
            .Field(_ => _.Status)
            .Resolve(ctx => resolvers.GetOrderStatus(ctx.Parent<Order>(), ctx.Service<IOrderRepository>()));

        descriptor
            .Field(_ => _.OrderedProducts)
            .Resolve(ctx =>
                resolvers.GetOrderedProducts(ctx.Parent<Order>(), ctx.Service<IOrderedProductRepository>()));
    }

    private class Resolvers
    {
        public User GetUser(Order order, [Service] IUserRepository userRepository)
        {
            return userRepository.Get(order.UserId);
        }

        public OrderStatus GetOrderStatus(Order order, [Service] IOrderRepository orderRepository)
        {
            return orderRepository.GetStatusIncluded(order.Id).Status;
        }
        
        public IEnumerable<OrderedProduct> GetOrderedProducts(Order order, [Service] IOrderedProductRepository orderedProductRepository)
        {
            return orderedProductRepository.GetProductIncluded(orderedProduct => orderedProduct.OrderId == order.Id);
        }
    }
}