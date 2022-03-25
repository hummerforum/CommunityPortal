using CommunityPortal.Data;
using CommunityPortal.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommunityPortal.Models.Services
{
    public class DiscussionGroupCategoriesService : IDiscussionGroupCategoriesService
    {
        private readonly ApplicationDbContext db;

        public DiscussionGroupCategoriesService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public int CreateDiscussionGroupCategory(DiscussionGroupCategory discussionGroupCategory)
        {
            db.DiscussionGroupCategories.Add(discussionGroupCategory);
            db.SaveChanges();
            db.Entry(discussionGroupCategory).GetDatabaseValues();
            return discussionGroupCategory.DiscussionGroupCategoryId;
        }

        public List<DiscussionGroupCategory> Read()
        {
            return db.DiscussionGroupCategories
                .ToList();
        }

        public DiscussionGroupCategory FindDiscussionGroupCategory(int discussionGroupCategoryId)
        {
            try
            {
                return db.DiscussionGroupCategories.Where(discussionGroupCategory => discussionGroupCategory.DiscussionGroupCategoryId == discussionGroupCategoryId)
                    .ToList()[0];
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public void UpdateDiscussionGroupCategory(DiscussionGroupCategory discussionGroupCategory)
        {
            DiscussionGroupCategory discussionGroupCategoryToUpdate = FindDiscussionGroupCategory(discussionGroupCategory.DiscussionGroupCategoryId);
            if (discussionGroupCategoryToUpdate != null)
            {
                discussionGroupCategoryToUpdate.DiscussionGroupId = discussionGroupCategory.DiscussionGroupId;
                discussionGroupCategoryToUpdate.CategoryId = discussionGroupCategory.CategoryId;

                db.DiscussionGroupCategories.Update(discussionGroupCategoryToUpdate);
                db.SaveChanges();
            }
        }

        public void DeleteDiscussionGroupCategory(int discussionGroupCategoryId)
        {
            db.DiscussionGroupCategories.Remove(FindDiscussionGroupCategory(discussionGroupCategoryId));
            db.SaveChanges();
        }
    }
}
