using System.Collections.Generic;
using System.Linq;

namespace CommunityPortal.Models.Repos
{

    public class CategoryRepository : ICategoryRepository
    {
        private static int idCounter = 0;
        private static List<Category> Categories = new List<Category>();
        
        public Category Create(Category category)
        {
            Category newCategory = new Category();
            newCategory = category;
            newCategory.CategoryId = ++idCounter;
            Categories.Add(newCategory);
            return newCategory;
        }

        public List<Category> Read()
        {
            return Categories;
        }

        public Category Read(int categoryId)
        {
            return Categories.FirstOrDefault(c => c.CategoryId == categoryId);
        }

        public Category Update(Category category)
        {
            int id = category.CategoryId;
            Category categoryToUpdate = Read(id);
            if (categoryToUpdate == null)
                return null;

            categoryToUpdate = category;
            categoryToUpdate.CategoryId = id;
            return categoryToUpdate;
        }

        public bool Delete(int categoryId)
        {
            Category categoryToDelete = Read(categoryId);
            if (categoryToDelete == null)
                return false;

            return Categories.Remove(categoryToDelete);
        }
    }

}