using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CommunityPortal.Models
{
    public class DiscussionReply
    {
        [Key] public int DiscussionReplyId { get; set; }
        
        public int TopicId { get; set; }
        [Required] public DiscussionTopic Topic { get; set; }

        [Required] public string Content { get; set; }

        [Required] public DateTime Time { get; set; }

        public CommunityUser Author { get; set; }
        public string AuthorId { get; set; }
    }
}