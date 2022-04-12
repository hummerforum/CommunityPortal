using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CommunityPortal.Models
{
    public class DiscussionGroupMembership
    {
        [Key]
        public int DiscussionGroupMembershipId { get; set; }
        
        [Required]
        public string CommunityUserId { get; set; }
        public CommunityUser CommunityUser { get; set; }

        [Required]
        public int DiscussionGroupId { get; set; }
        public DiscussionGroup DiscussionGroup { get; set; }
    }
}
