using FashopBackend.Core.Aggregate.CategoryAggregate;
using FashopBackend.Core.Aggregate.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashopBackend.Core.Interfaces
{
    public interface ICategoryService
    {
        IEnumerable<Category> GetByIds(params int[] ids);
        
        IEnumerable<Category> GetByProduct(Product product);

        Task<Category> Create(Category product);

        Category Edit(int id, string name);
        
        int Delete(int inputId);
    }
}
