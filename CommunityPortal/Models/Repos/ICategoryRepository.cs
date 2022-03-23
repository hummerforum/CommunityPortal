using System.Collections.Generic;

namespace CommunityPortal.Models.Repos
{

    public interface ICategoryRepository
    {
        public Category Create(Category category);
        public List<Category> Read();
        public Category Read(int categoryId);
        public Category Update(Category category);
        public bool Delete(int categoryId);
    }

}