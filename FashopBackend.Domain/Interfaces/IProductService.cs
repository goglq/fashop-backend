using FashopBackend.Core.Aggregate.CategoryAggregate;
using FashopBackend.Core.Aggregate.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashopBackend.Core.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetWithId(params int[] ids);

        IEnumerable<Product> GetByCategory(Category category);


        Task<Product> Create(Product product);
        
        Product Edit(int inputId, string inputName, IEnumerable<Category> categories);
        
        int Delete(int inputId);
    }
}
