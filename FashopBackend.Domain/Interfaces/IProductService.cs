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
        IEnumerable<Product> GetAllProducts();

        IEnumerable<Product> GetProductsWithId(params int[] ids);

        Product GetProductById(int id);

        Task<Product> CreateProduct(Product product);

        Product AddCategoryToProduct(int id, Category category);

    }
}
