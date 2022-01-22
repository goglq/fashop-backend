using FashopBackend.SharedKernel.Interfaces;

namespace FashopBackend.Core.Aggregate.OrderAggregate;

public interface IOrderRepository : IRepository<Order>
{
    public Order GetStatusIncluded(int id);
}