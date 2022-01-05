using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using FashopBackend.Core.Aggregate.ProductImageAggregate;

namespace FashopBackend.Infrastructure.Data.Repositories;

public class ProductImageRepository : RepositoryBase<ProductImage>, IProductImageRepository
{
    public ProductImageRepository(FashopContext context) : base(context)
    {
    }
}