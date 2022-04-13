using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommunityPortal.Models
{
    public class DiscussionTopic
    {
        [Key] public int DiscussionTopicId { get; set; }

        [Required] public int DiscussionForumId { get; set; }
        [Required] public DiscussionForum DiscussionForum { get; set; }

        [Required] public string Subject { get; set; }

        [Required] public string Content { get; set; }

        [Required] public DateTime Time { get; set; }

        public string AuthorId { get; set; }
        public CommunityUser Author { get; set; }

        public int NrOfViews { get; set; }
    }
}