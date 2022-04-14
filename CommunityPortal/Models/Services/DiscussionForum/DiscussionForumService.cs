using CommunityPortal.Data;
using CommunityPortal.Model.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommunityPortal.Models.Services
{
    public class DiscussionForumService : IDiscussionForumService
    {
        private readonly ApplicationDbContext db;
        private UserManager<CommunityUser> userManager;

        public DiscussionForumService(ApplicationDbContext db, UserManager<CommunityUser> _userManager)
        {
            userManager = _userManager;
            this.db = db;
        }

        public int CreateDiscussionForum(DiscussionForum discussionForum)
        {
            throw new NotImplementedException();
        }

        public List<DiscussionForum> List(bool isAdmin)
        {
            List < DiscussionForum > dis = db.DiscussionForums.Include(df => df.DiscussionCategory).ToList();
            if (!isAdmin)
            {
                DiscussionForum d = db.DiscussionForums.First(df => df.Name == "Secret");
                dis.Remove(d);
            }
            return dis;
        }




        public List<DiscussionTopic> Forum(int id)
        {
            // TODO: filter stuff from Author
            return db.DiscussionTopics.Include(t => t.Author).Where(t => t.DiscussionForumId == id).ToList();
        }

        public List<DiscussionReply> Topic(int id)
        {
            // TODO: filter stuff from Author
            return db.DiscussionReplies.Include(r => r.Author).Where(r => r.TopicId == id).ToList();
        }

        
        public void UpdateDiscussionForum(DiscussionForum discussionForum)
        {
            throw new NotImplementedException();
        }

        public void DeleteDiscussionForum(int discussionForumId)
        {
            throw new NotImplementedException();
        }
    }
}