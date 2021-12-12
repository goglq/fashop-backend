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
        IEnumerable<Category> GetAll();
        
        IEnumerable<Category> GetByIds(params int[] ids);
        
        IEnumerable<Category> GetByProduct(Product product);

        Category GetCategoryById(int id);

        Task<Category> Create(Category product);

        Category Edit(int id, string name, IEnumerable<Product> products);
    }
}
