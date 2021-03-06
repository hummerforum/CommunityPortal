using System.Collections.Generic;
using System.Linq;
using CommunityPortal.Data;

namespace CommunityPortal.Models.Services
{

    public class CategoryService : ICategoryService
    {
        private readonly ApplicationDbContext _appDbContext;

        public CategoryService(ApplicationDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Category Add(Category category)
        {
            Category newCategory = new Category();
            newCategory = category;
            _appDbContext.Categories.Add(newCategory);
            _appDbContext.SaveChanges();
            return newCategory;
        }

        public Category GetById(int categoryId)
        {
            return _appDbContext.Categories.Where(c => c.CategoryId == categoryId).SingleOrDefault();
        }

        public List<Category> GetList()
        {
            return _appDbContext.Categories.ToList();
        }

        public Category Update(Category category)
        {
            int id = category.CategoryId;
            Category categoryToUpdate = GetById(id);
            if (categoryToUpdate == null)
                return null;

            categoryToUpdate.CategoryId = id;
            categoryToUpdate.Title = category.Title;
            categoryToUpdate.Description = category.Description;
            _appDbContext.Categories.Update(categoryToUpdate);
            _appDbContext.SaveChanges();
            return categoryToUpdate;
        }

        public bool Delete(int categoryId)
        {
            Category categoryToDelete = GetById(categoryId);
            if (categoryToDelete == null)
                return false;

            _appDbContext.Categories.Remove(categoryToDelete);
            _appDbContext.SaveChanges();
            return true;
        }
    }

}