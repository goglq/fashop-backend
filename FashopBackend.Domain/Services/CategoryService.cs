using FashopBackend.Core.Aggregate.CategoryAggregate;
using FashopBackend.Core.Aggregate.ProductAggregate;
using FashopBackend.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FashopBackend.Core.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<Category> GetAllCategories() => _categoryRepository.GetAll();

        public IEnumerable<Category> GetCategoryByProduct(Product product)
        {
            return _categoryRepository.GetAll(category => category.Products.Contains(product));
        }

        public Category GetCategoryById(int id) => _categoryRepository.Get(id);

        public async Task<Category> CreateCategory(Category category)
        {
            await _categoryRepository.Create(category);
            await _categoryRepository.SaveAsync();
            return category;
        }

        public Category EditCategory(int id, string name, IEnumerable<Product> products)
        {
            Category category = _categoryRepository.Get(id);
            
            category.Name = name;
            category.Products = products.ToList();
            
            _categoryRepository.Update(category);
            _categoryRepository.Save();
            
            return category;
        }

        public IEnumerable<Category> GetCategoryByIds(params int[] ids)
        {
            IEnumerable<Category> categories = _categoryRepository.GetAll(category => ids.Contains(category.Id));
            return categories;
        }
    }
}
