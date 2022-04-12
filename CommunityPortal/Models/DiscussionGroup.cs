using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommunityPortal.Models
{
    public class DiscussionGroup
    {
        [Key]
        public int DiscussionGroupId { get; set; }

        [Required]
        public string DiscussionGroupName { get; set; }
        
        public List<DiscussionGroupMembership> DiscussionGroupMembership { get; set; }
        }
    
    
}
