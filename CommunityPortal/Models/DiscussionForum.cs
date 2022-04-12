using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommunityPortal.Models
{
    public class DiscussionForum
    {
        [Key] public int DiscussionForumId { get; set; }

        [Required] public string Name { get; set; }

        public string Description { get; set; }

        List<DiscussionGroupMembership> DiscussionGroupMemberships { get; set; }

        public int DiscussionCategoryId { get; set; }
        public DiscussionCategory DiscussionCategory { get; set; }
    }
}