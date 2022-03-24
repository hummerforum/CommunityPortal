using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityPortal.Models
{
    public class DiscussionGroup
    {
        [Key]
        public int DiscussionGroupId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        public List<CommunityUser> DiscussionGroupMembers { get; set; }
    }
}
