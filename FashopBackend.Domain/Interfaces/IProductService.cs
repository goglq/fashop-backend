using FashopBackend.Core.Aggregate.CategoryAggregate;
using FashopBackend.Core.Aggregate.ProductAggregate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FashopBackend.Core.Aggregate.BrandAggregate;

namespace FashopBackend.Core.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetWithId(params int[] ids);

        IEnumerable<Product> GetByCategory(Category category);

        Task<Product> Create(Product product);
        
        Product Edit(int inputId, string inputName, Brand brand, IEnumerable<Category> categories);
        
        int Delete(int inputId);
        IEnumerable<Product> GetByBrand(Brand parent);
    }
}
