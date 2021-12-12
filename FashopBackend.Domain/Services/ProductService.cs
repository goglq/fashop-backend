using FashopBackend.Core.Aggregate.CategoryAggregate;
using FashopBackend.Core.Aggregate.ProductAggregate;
using FashopBackend.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Product> GetAll()
        {
            return _productRepository.GetAll();
        }

        public IEnumerable<Product> GetWithId(params int[] ids)
        {
            return _productRepository.GetAll(product => ids.Contains(product.Id));
        }

        public IEnumerable<Product> GetByCategory(Category category)
        {
            return _productRepository.GetAll(product => product.Categories.Contains(category));
        }

        public Product GetById(int id)
        {
            return _productRepository.Get(id);
        }

        public async Task<Product> Create(Product product)
        {
            await _productRepository.Create(product);
            await _productRepository.SaveAsync();
            return product;
        }

        public Product Edit(int inputId, string inputName, IEnumerable<Category> categories)
        {
            Product product = _productRepository.Get(inputId);
            product.Name = inputName;
            product.Categories = categories.ToList();
            
            _productRepository.Update(product);
            _productRepository.Save();

            return product;
        }

        public int Delete(int inputId)
        {
            Product product = _productRepository.Get(inputId);
            _productRepository.Remove(product);
            _productRepository.Save();
            return product.Id;
        }
    }
}
