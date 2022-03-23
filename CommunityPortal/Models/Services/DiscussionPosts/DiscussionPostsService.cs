using CommunityPortal.Data;
using CommunityPortal.Model.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommunityPortal.Models.Services
{
    public class DiscussionPostsService : IDiscussionPostsService
    {
        private readonly ApplicationDbContext db;

        public DiscussionPostsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public int CreateDiscussionPost(DiscussionPost discussionPost)
        {
            db.DiscussionPosts.Add(discussionPost);
            db.SaveChanges();
            db.Entry(discussionPost).GetDatabaseValues();
            return discussionPost.DiscussionPostId;
        }

        public List<DiscussionPost> Read()
        {
            return db.DiscussionPosts
                .ToList();
        }

        public List<DiscussionPost> FindDiscussionPosts(string searchString, bool caseSensitive)
        {
            List<DiscussionPost> discussionPostsToReturn = new List<DiscussionPost>();
            List<DiscussionPost> discussionPosts = db.DiscussionPosts.Where(discussionPost =>
                discussionPost.Heading.Contains(searchString) ||
                discussionPost.Content.Contains(searchString)
                )
                .ToList();

            if (caseSensitive)
            {
                foreach (var discussionPost in discussionPosts)
                {
                    if (discussionPost.Heading.Contains(searchString) || discussionPost.Content.Contains(searchString))
                    {
                        discussionPostsToReturn.Add(discussionPost);
                    }
                }
            }
            else
            {
                discussionPostsToReturn = discussionPosts;
            }

            return discussionPostsToReturn;
        }

        public DiscussionPost FindDiscussionPost(int discussionPostId)
        {
            try
            {
                return db.DiscussionPosts.Where(discussionPost => discussionPost.DiscussionPostId == discussionPostId)
                    .ToList()[0];
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public DiscussionPost FindDiscussionPost(string discussionPostHeading)
        {
            return db.DiscussionPosts.FirstOrDefault(discussionPost => discussionPost.Heading.Trim().ToLower() == discussionPostHeading.Trim().ToLower());
        }

        public void UpdateDiscussionPost(DiscussionPost discussionPost)
        {
            DiscussionPost discussionPostToUpdate = FindDiscussionPost(discussionPost.DiscussionPostId);
            if (discussionPostToUpdate != null)
            {
                discussionPostToUpdate.Heading = discussionPost.Heading;
                discussionPostToUpdate.Content = discussionPost.Content;
                discussionPostToUpdate.Category = discussionPost.Category;
                discussionPostToUpdate.Time = discussionPost.Time;

                db.DiscussionPosts.Update(discussionPostToUpdate);
                db.SaveChanges();
            }
        }

        public void DeleteDiscussionPost(int discussionPostId)
        {
            db.DiscussionPosts.Remove(FindDiscussionPost(discussionPostId));
            db.SaveChanges();
        }
    }
}
