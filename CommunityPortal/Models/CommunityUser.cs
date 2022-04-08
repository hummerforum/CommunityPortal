using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CommunityPortal.Models
{
    public class CommunityUser : IdentityUser
    {

        List<DiscussionPost> DiscussionPosts { get; set; }

        List<DiscussionGroupMembership> DiscussionGroupMemberships { get; set; }

        List<ReceivedPrivateMessage> ReceivedPrivateMessages { get; set; }

        List<SentPrivateMessage> SentPrivateMessages { get; set; }
    }
}
