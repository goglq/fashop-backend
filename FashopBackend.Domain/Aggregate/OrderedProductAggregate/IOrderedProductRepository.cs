using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using FashopBackend.SharedKernel.Interfaces;

namespace FashopBackend.Core.Aggregate.OrderedProductAggregate;

public interface IOrderedProductRepository : IRepository<OrderedProduct>
{
    public IEnumerable<OrderedProduct> GetProductIncluded(Expression<Func<OrderedProduct, bool>> filter = null);
}