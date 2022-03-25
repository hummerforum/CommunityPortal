using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommunityPortal.Models
{
    public class DiscussionPost
    {
        [Key]
        public int DiscussionPostId { get; set; }

        [Required]
        public string Heading { get; set; }

        [Required]
        public string Content { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        public CommunityUser CommunityUser { get; set; }

        public DiscussionPost DiscussionPostReply { get; set; }

        public Category Category { get; set; }
    }
}
