using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Claims;
using System.Threading.Tasks;
using Newtonsoft.Json;


namespace CommunityPortal.Models
{
    public class CommunityUser : IdentityUser
    {

        List<DiscussionReply> DiscussionReply { get; set; }

        public IList<DiscussionGroupMembership> DiscussionGroupMemberships { get; set; }

        List<ReceivedPrivateMessage> ReceivedPrivateMessages { get; set; }

        List<SentPrivateMessage> SentPrivateMessages { get; set; }
        public List<DiscussionGroupMembership> DiscussionGroupMembership { get; set; }
        
        [JsonIgnore] 
        [IgnoreDataMember]
        public List<DiscussionTopic> DiscussionTopics { get; set; }
    }

    // bara för senare bruk om vi behöver ha direkt access till variabler 
    public class AppClaimsPrincipalFactory : UserClaimsPrincipalFactory<CommunityUser, IdentityRole>
    {
        public AppClaimsPrincipalFactory(
            UserManager<CommunityUser> userManager
            , RoleManager<IdentityRole> roleManager
            , IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, roleManager, optionsAccessor)
        {
        }
        //public async override Task<ClaimsPrincipal> CreateAsync(CommunityUser user)
        //{
          //  var principal = await base.CreateAsync(user);
          //  if (!string.IsNullOrWhiteSpace(user.FirstName))
          //  {
          //      ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
          //  new Claim(ClaimTypes.GivenName, user.FirstName)
          //});
          //  }

          //  if (!string.IsNullOrWhiteSpace(user.LastName))
          //  {
          //      ((ClaimsIdentity)principal.Identity).AddClaims(new[] {
          //  new Claim(ClaimTypes.Surname, user.LastName),
          //});
          //  }
          //  return principal;
            //}
    }
}
