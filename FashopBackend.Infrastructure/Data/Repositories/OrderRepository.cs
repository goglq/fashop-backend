using FashopBackend.Core.Aggregate.OrderAggregate;

namespace FashopBackend.Infrastructure.Data.Repositories;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(FashopContext context) : base(context)
    {
    }
}