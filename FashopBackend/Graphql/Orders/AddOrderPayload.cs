using FashopBackend.Core.Aggregate.OrderAggregate;

namespace FashopBackend.Graphql.Orders;

public record AddOrderPayload(Order order);