using FashopBackend.Core.Aggregate.CategoryAggregate;
using FashopBackend.Core.Aggregate.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FashopBackend.Infrastructure.Data.Repositories
{
    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(FashopContext context) : base(context)
        {

        }

        public Product GetIncluded(int id)
        {
            return DbSet
                .Include(p => p.Categories)
                .Include(p => p.Brand)
                .FirstOrDefault(p => p.Id == id);
        }
    }
}
