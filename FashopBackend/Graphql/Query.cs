﻿using FashopBackend.Core.Aggregate.CategoryAggregate;
using FashopBackend.Core.Aggregate.ProductAggregate;
using FashopBackend.Core.Interfaces;
using FashopBackend.Core.Services;
using HotChocolate;
using HotChocolate.Data;
using System.Collections.Generic;
using System.Linq;

namespace FashopBackend.Graphql
{
    public class Query
    {
        [UseFiltering]
        [UseSorting]
        public IEnumerable<Product> GetProducts([Service] IProductService service) => service.GetAllProducts();

        public Product GetProduct(int id, [Service]IProductService service) => service.GetProductById(id);

        [UseFiltering]
        [UseSorting]
        public IEnumerable<Category> GetCategories([Service] ICategoryService service) => service.GetAllCategories();

        public Category GetCategory(int id, [Service] ICategoryService service) => service.GetCategoryById(id);
    }
}
