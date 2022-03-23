using System.Collections.Generic;
using System.Linq;
using CommunityPortal.Models.Repos;

namespace CommunityPortal.Models.Services
{

    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;

        public CategoryService()
        {
            _categoryRepository = new CategoryRepository();
        }

        public Category Add(Category category)
        {
            return _categoryRepository.Create(category);
        }

        public Category GetById(int categoryId)
        {
            return _categoryRepository.Read(categoryId);
        }

        public List<Category> GetList()
        {
            return _categoryRepository.Read();
        }

        public bool Delete(int categoryId)
        {
            return _categoryRepository.Delete(categoryId);
        }
    }

}