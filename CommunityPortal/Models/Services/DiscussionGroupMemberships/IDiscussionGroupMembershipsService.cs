using CommunityPortal.Models;
using System.Collections.Generic;

namespace CommunityPortal.Model.Services
{
    public interface IDiscussionGroupMembershipsService
    {
        public int CreateDiscussionGroupMembership(DiscussionGroupMembership discussionGroupMembership);

        public List<DiscussionGroupMembership> Read();

        public DiscussionGroupMembership FindDiscussionGroupMembership(int discussionGroupMembershipId);

        public void UpdateDiscussionGroupMembership(DiscussionGroupMembership discussionGroupMembership);

        public void DeleteDiscussionGroupMembership(int discussionGroupMembershipId);
    }
}
