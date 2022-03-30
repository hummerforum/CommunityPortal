using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CommunityPortal.Models
{
    public class CommunityUser : IdentityUser
    {
        public string UserName2 { get; set; }

        List<DiscussionPost> DiscussionPosts { get; set; }

        List<DiscussionGroupMembership> DiscussionGroupMemberships { get; set; }
    }
}
