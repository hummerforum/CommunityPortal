using CommunityPortal.Data;
using CommunityPortal.Model.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

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

        public List<DiscussionForum> Overview(bool isAdmin)
        {
            List<DiscussionForum> dis = db.DiscussionForums.Include(df => df.DiscussionCategory).ToList();
            if (isAdmin) return dis;
            {
                DiscussionForum d = db.DiscussionForums.First(df => df.Name == "Secret");
                dis.Remove(d);
            }

            return dis;
        }
        
        public List<DiscussionTopic> Topics(int id)
        {
            // TODO: filter stuff from Author
            return db.DiscussionTopics.Include(t => t.Author).Where(t => t.DiscussionForumId == id).ToList();
        }

        public List<DiscussionTopic> Topic(int id)
        {
            // TODO: filter stuff from Author
            return db.DiscussionTopics.Include(t => t.Author).Where(t => t.DiscussionTopicId == id).ToList();
        }
        
        public List<DiscussionReply> Replies(int id)
        {
            // TODO: filter stuff from Author
            return db.DiscussionReplies.Include(r => r.Author).Where(r => r.TopicId == id).ToList();
        }

        private class CreateResponse
        {
            public int forumId;
            public int topicId;
        }
        
        public string CreateTopic(string userId, int forumId, string subject, string content)
        {
            DiscussionTopic topic = new()
            {
                DiscussionForumId = forumId,
                AuthorId = userId,
                Subject = subject,
                Content = content,
                Time = DateTime.Now
            };
            db.DiscussionTopics.Add(topic);
            int result = db.SaveChanges();
            CreateResponse response = new()
            {
                forumId = topic.DiscussionForumId,
                topicId = topic.DiscussionTopicId
            };
            return result == 1 ? JsonConvert.SerializeObject(response) : "500";
        }

        public string CreateReply(string userId, int id, string content)
        {
            DiscussionReply reply = new()
            {
                TopicId = id,
                AuthorId = userId,
                Content = content,
                Time = DateTime.Now
            };
            db.DiscussionReplies.Add(reply);
            int result = db.SaveChanges();
            CreateResponse response = new()
            {
                topicId = reply.TopicId
            };
            return result == 1 ? JsonConvert.SerializeObject(response) : "500";
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