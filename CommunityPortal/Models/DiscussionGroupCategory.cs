using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommunityPortal.Models
{
    public class DiscussionGroupCategory
    {
        [Key]
        public int DiscussionGroupCategoryId { get; set; }
        
        public int DiscussionGroupId { get; set; }
        public DiscussionGroup Group { get; set; }
        
        public int DiscussionCategoryId { get; set; }
        public DiscussionCategory Category { get; set; }
        public IList<DiscussionGroupMembership> DiscussionGroupMembership { get; set; }
    }
}