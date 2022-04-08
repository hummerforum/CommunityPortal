using System.ComponentModel.DataAnnotations;

namespace CommunityPortal.Models
{
    public class DiscussionGroupMembership
    {
        public DiscussionGroupMembership(int discussionGroupMemberId, int discussionGroupId)
        {
            DiscussionGroupMemberId = discussionGroupMemberId;
            DiscussionGroupId = discussionGroupId;
        }

        [Key]
        public int DiscussionGroupMembershipId { get; set; }

        
        
        [Required]
        public int DiscussionGroupMemberId { get; set; }
        public CommunityUser DiscussionGroupMember { get; set; }




        [Required]
        public int DiscussionGroupId { get; set; }
        public DiscussionGroup DiscussionGroup { get; set; }
    }
}
