using CommunityPortal.Models;
using System.Collections.Generic;

namespace CommunityPortal.Model.Services
{
    public interface IDiscussionForumService
    {
        public int CreateDiscussionForum(DiscussionForum discussionForum);

        public List<DiscussionForum> List();
        
        public List<DiscussionTopic> Forum(int Id);
        
        public List<DiscussionReply> Topic(int Id);

        public void UpdateDiscussionForum(DiscussionForum discussionForum);

        public void DeleteDiscussionForum(int discussionForumId);
    }
}
