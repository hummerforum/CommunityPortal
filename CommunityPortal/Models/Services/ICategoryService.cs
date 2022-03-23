using System.Collections.Generic;

namespace CommunityPortal.Models.Services
{

    public interface ICategoryService
    {
        public Category Add(Category category);
        public Category GetById(int categoryId);
        public List<Category> GetList();
        public bool Delete(int categoryId);
    }

}