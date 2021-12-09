using FashopBackend.Core.Aggregate.CategoryAggregate;
using FashopBackend.Core.Aggregate.ProductAggregate;
using FashopBackend.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FashopBackend.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            return _productRepository.GetAll();
        }

        public IEnumerable<Product> GetProductsWithId(params int[] ids)
        {
            return _productRepository.GetAll(product => ids.Contains(product.Id));
        }

        public Product GetProductById(int id)
        {
            return _productRepository.Get(id);
        }

        public async Task<Product> CreateProduct(Product product)
        {
            await _productRepository.Create(product);
            await _productRepository.SaveAsync();
            return product;
        }

        public Product AddCategoryToProduct(int id, Category category)
        {
            throw new NotImplementedException();
        }
    }
}
