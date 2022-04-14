using CommunityPortal.Models;
using System.Collections.Generic;

namespace CommunityPortal.Model.Services
{
    public interface IDiscussionForumService
    {
        public int CreateDiscussionForum(DiscussionForum discussionForum);

        public List<DiscussionForum> Overview(bool isAdmin);
        
        public List<DiscussionTopic> Topics(int Id);
        
        public List<DiscussionTopic> Topic(int Id);
        
        public List<DiscussionReply> Replies(int Id);
        
        public string CreateTopic(string userId, int forumId, string subject, string content);
        
        public string CreateReply(string userId, int id, string content);

        public void UpdateDiscussionForum(DiscussionForum discussionForum);

        public void DeleteDiscussionForum(int discussionForumId);
    }
}
