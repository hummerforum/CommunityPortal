using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommunityPortal.Models
{
    public class DiscussionCategory
    {
        [Key] public int DiscussionCategoryId { get; set; }

        [Required] [Display(Name = "Name")] public string Name { get; set; }

        [Display(Name = "Description")] public string Description { get; set; }
        
        public IList<DiscussionGroupMembership> DiscussionGroupMemberships { get; set; }
    }
}