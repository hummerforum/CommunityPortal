using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityPortal.Models
{
    public class CommunityUser : IdentityUser
    {

        List<DiscussionPost> DiscussionPosts { get; set; }

        List<DiscussionGroupMembership> DiscussionGroupMemberships { get; set; }
    }
}
