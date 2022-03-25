using CommunityPortal.Models;
using System.Collections.Generic;

namespace CommunityPortal.Model.Services
{
    public interface IDiscussionGroupCategoriesService
    {
        public int CreateDiscussionGroupCategory(DiscussionGroupCategory discussionGroupCategory);

        public List<DiscussionGroupCategory> Read();

        public DiscussionGroupCategory FindDiscussionGroupCategory(int discussionGroupCategoryId);

        public void UpdateDiscussionGroupCategory(DiscussionGroupCategory discussionGroupCategory);

        public void DeleteDiscussionGroupCategory(int discussionGroupCategoryId);
    }
}
