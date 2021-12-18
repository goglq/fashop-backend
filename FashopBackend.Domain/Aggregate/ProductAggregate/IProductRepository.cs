using FashopBackend.Core.Aggregate.CategoryAggregate;
using FashopBackend.SharedKernel.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashopBackend.Core.Aggregate.ProductAggregate
{
    public interface IProductRepository : IRepository<Product>
    {
        Product GetIncluded(int id);
    }
}