using CommunityPortal.Models;
using System.Collections.Generic;

namespace CommunityPortal.Model.Services
{
    public interface IDiscussionGroupsService
    {
        public int CreateDiscussionGroup(DiscussionGroup discussionGroup);

        public List<DiscussionGroup> Read();

        public List<DiscussionGroup> FindDiscussionGroups(string searchString, bool caseSensitive);

        public DiscussionGroup FindDiscussionGroup(int discussionGroupId);

        public DiscussionGroup FindDiscussionGroup(string discussionGroupHeading);

        public void UpdateDiscussionGroup(DiscussionGroup discussionGroup);

        public void DeleteDiscussionGroup(int discussionGroupId);
    }
}
