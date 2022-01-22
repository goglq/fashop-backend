using System.Linq;
using FashopBackend.Core.Aggregate.OrderAggregate;
using Microsoft.EntityFrameworkCore;

namespace FashopBackend.Infrastructure.Data.Repositories;

public class OrderRepository : RepositoryBase<Order>, IOrderRepository
{
    public OrderRepository(FashopContext context) : base(context)
    {
    }

    public Order GetStatusIncluded(int id)
    {
        return DbSet.Include(order => order.Status).FirstOrDefault(order => order.Id == id);
    }
}