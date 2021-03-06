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

        public IEnumerable<Category> GetByProduct(Product product)
        {
            return _categoryRepository.GetAll(category => category.Products.Contains(product));
        }

        public async Task<Category> Create(Category category)
        {
            await _categoryRepository.Create(category);
            await _categoryRepository.SaveAsync();
            return category;
        }

        public Category Edit(int id, string name)
        {
            Category category = _categoryRepository.Get(id);
            
            category.Name = name;
            _categoryRepository.Update(category);
            _categoryRepository.Save();
            
            return category;
        }

        public int Delete(int inputId)
        {
            Category category = _categoryRepository.Get(inputId);
            _categoryRepository.Remove(category);
            _categoryRepository.Save();
            return category.Id;
        }

        public IEnumerable<Category> GetByIds(params int[] ids)
        {
            IEnumerable<Category> categories = _categoryRepository.GetAll(category => ids.Contains(category.Id));
            return categories;
        }
    }
}
