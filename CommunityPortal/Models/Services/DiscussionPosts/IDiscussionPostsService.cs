using CommunityPortal.Models;
using System.Collections.Generic;

namespace CommunityPortal.Model.Services
{
    public interface IDiscussionPostsService
    {
        public int CreateDiscussionPost(DiscussionPost discussionPost);

        public List<DiscussionPost> Read();
        public DiscussionPost GetSingeDiscussionPost(int discussionPostId);

        public void IncreaseViewCount(int discussionPostId);

        public List<DiscussionPost> FindDiscussionPosts(string searchString, bool caseSensitive);

        public DiscussionPost FindDiscussionPost(int discussionPostId);

        public DiscussionPost FindDiscussionPost(string discussionPostHeading);

        public void UpdateDiscussionPost(DiscussionPost discussionPost);

        public void DeleteDiscussionPost(int discussionPostId);
    }
}
