using FashopBackend.Core.Aggregate.CategoryAggregate;
using FashopBackend.Core.Aggregate.ProductAggregate;
using FashopBackend.Core.Interfaces;
using HotChocolate;
using HotChocolate.Data;
using System.Collections.Generic;
using FashopBackend.Core.Aggregate.BrandAggregate;
using HotChocolate.Types;

namespace FashopBackend.Graphql
{
    public class Query
    {
        [UseFiltering]
        [UseSorting]
        public IEnumerable<Product> GetProducts([Service] IProductService service) => service.GetAll();

        public Product GetProduct(int id, [Service]IProductService service) => service.GetById(id);

        [UseFiltering]
        [UseSorting]
        public IEnumerable<Category> GetCategories([Service] ICategoryService service) => service.GetAll();

        public Category GetCategory(int id, [Service] ICategoryService service) => service.GetCategoryById(id);

        [UseFiltering]
        [UseSorting]
        public IEnumerable<Brand> GetBrands([Service] IBrandRepository repository) => repository.GetAll();

        public Brand GetBrand(int id, [Service] IBrandRepository repository) => repository.Get(id);
    }
}
