﻿using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace CommunityPortal.Models
{
    public class CommunityUser : IdentityUser
    {

        List<DiscussionPost> DiscussionPosts { get; set; }

        public string Id2()
        {
            return this.Id;
        
        }

    }
}
