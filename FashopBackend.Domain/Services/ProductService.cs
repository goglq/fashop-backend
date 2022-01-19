using FashopBackend.Core.Aggregate.CategoryAggregate;
using FashopBackend.Core.Aggregate.ProductAggregate;
using FashopBackend.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FashopBackend.Core.Aggregate.BrandAggregate;
using FashopBackend.Core.Aggregate.ProductImageAggregate;

namespace FashopBackend.Core.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        
        public IEnumerable<Product> GetWithId(params int[] ids)
        {
            return _productRepository.GetAll(product => ids.Contains(product.Id));
        }

        public IEnumerable<Product> GetByCategory(Category category)
        {
            return _productRepository.GetAll(product => product.Categories.Contains(category));
        }

        public async Task<Product> Create(Product product)
        {
            await _productRepository.Create(product);
            await _productRepository.SaveAsync();
            return product;
        }

        public Product Edit(int inputId, string inputName, string description, double price, int sale, Brand brand,
            IEnumerable<Category> categories, IEnumerable<ProductImage> productImages)
        {
            Product product = _productRepository.GetIncluded(inputId);
            product.Name = inputName;
            product.Description = description;
            product.Price = price;
            product.Sale = sale;
            product.Categories.Clear();
            product.Categories.AddRange(categories);
            product.ProductImages.Clear();
            product.ProductImages.AddRange(productImages);
            
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

        public IEnumerable<Product> GetByBrand(Brand parent)
        {
            return _productRepository.GetAll(product => product.BrandId == parent.Id);
        }
    }
}
