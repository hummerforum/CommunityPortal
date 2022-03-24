using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommunityPortal.Models
{
    public class SubForum
    {
        [Key]
        public int SubForumId { get; set; }

        [Required]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        public DiscussionGroup DiscussionGroup { get; set; }

        public List<DiscussionPost> DiscussionPosts { get; set; }
    }
}
