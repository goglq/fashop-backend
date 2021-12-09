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
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<Category> GetAllCategories() => _categoryRepository.GetAll();

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

            foreach (Product product in products)
                if(!product.Categories.Contains(category))
                    product.Categories.Add(category);


            category.Products.AddRange(products);

            _categoryRepository.Update(category);

            _categoryRepository.Save();

            return category;
        }
    }
}
