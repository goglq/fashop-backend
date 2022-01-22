using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using FashopBackend.Core.Aggregate.OrderedProductAggregate;
using Microsoft.EntityFrameworkCore;

namespace FashopBackend.Infrastructure.Data.Repositories;

public class OrderedProductRepository : RepositoryBase<OrderedProduct>, IOrderedProductRepository
{
    public OrderedProductRepository(FashopContext context) : base(context)
    {
    }


    public IEnumerable<OrderedProduct> GetProductIncluded(Expression<Func<OrderedProduct, bool>> filter = null)
    {
        return filter is null ? 
            DbSet
                .Include(i => i.Product)
                .ToList() : 
            DbSet
                .Include(i => i.Product)
                .Where(filter)
                .ToList();
    }
}