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
        IEnumerable<Category> GetAllCategories();
        
        IEnumerable<Category> GetCategoryByIds(params int[] ids);
        
        IEnumerable<Category> GetCategoryByProduct(Product product);

        Category GetCategoryById(int id);

        Task<Category> CreateCategory(Category product);

        Category EditCategory(int id, string name, IEnumerable<Product> products);
    }
}
