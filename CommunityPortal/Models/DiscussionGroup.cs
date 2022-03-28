using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommunityPortal.Models
{
    public class DiscussionGroup
    {
        [Key]
        public int DiscussionGroupId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        List<DiscussionGroupMembership> DiscussionGroupMemberships { get; set; }

        List<Category> Categories { get; set; }
    }
}
