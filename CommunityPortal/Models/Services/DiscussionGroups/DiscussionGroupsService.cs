/*
using CommunityPortal.Data;
using CommunityPortal.Model.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommunityPortal.Models.Services
{
    public class DiscussionGroupsService : IDiscussionGroupsService
    {
        private readonly ApplicationDbContext db;

        public DiscussionGroupsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public int CreateDiscussionGroup(DiscussionGroup discussionGroup)
        {
            db.DiscussionGroups.Add(discussionGroup);
            db.SaveChanges();
            db.Entry(discussionGroup).GetDatabaseValues();
            return discussionGroup.DiscussionGroupId;
        }

        public List<DiscussionGroup> Read()
        {
            return db.DiscussionGroups
                .ToList();
        }

        public List<DiscussionGroup> FindDiscussionGroups(string searchString, bool caseSensitive)
        {
            List<DiscussionGroup> discussionGroupsToReturn = new List<DiscussionGroup>();
            List<DiscussionGroup> discussionGroups = db.DiscussionGroups.Where(discussionGroup =>
                discussionGroup.Name.Contains(searchString) ||
                discussionGroup.Description.Contains(searchString)
                )
                .ToList();

            if (caseSensitive)
            {
                foreach (var discussionGroup in discussionGroups)
                {
                    if (discussionGroup.Name.Contains(searchString) || discussionGroup.Description.Contains(searchString))
                    {
                        discussionGroupsToReturn.Add(discussionGroup);
                    }
                }
            }
            else
            {
                discussionGroupsToReturn = discussionGroups;
            }

            return discussionGroupsToReturn;
        }

        public DiscussionGroup FindDiscussionGroup(int discussionGroupId)
        {
            try
            {
                return db.DiscussionGroups.Where(discussionGroup => discussionGroup.DiscussionGroupId == discussionGroupId)
                    .ToList()[0];
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public DiscussionGroup FindDiscussionGroup(string discussionGroupHeading)
        {
            return db.DiscussionGroups.FirstOrDefault(discussionGroup => discussionGroup.Name.Trim().ToLower() == discussionGroupHeading.Trim().ToLower());
        }

        public void UpdateDiscussionGroup(DiscussionGroup discussionGroup)
        {
            DiscussionGroup discussionGroupToUpdate = FindDiscussionGroup(discussionGroup.DiscussionGroupId);
            if (discussionGroupToUpdate != null)
            {
                discussionGroupToUpdate.Name = discussionGroup.Name;
                discussionGroupToUpdate.Description = discussionGroup.Description;

                db.DiscussionGroups.Update(discussionGroupToUpdate);
                db.SaveChanges();
            }
        }

        public void DeleteDiscussionGroup(int discussionGroupId)
        {
            db.DiscussionGroups.Remove(FindDiscussionGroup(discussionGroupId));
            db.SaveChanges();
        }
    }
}
*/
