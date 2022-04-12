/*
using CommunityPortal.Data;
using CommunityPortal.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommunityPortal.Models.Services
{
    public class DiscussionGroupMembershipsService : IDiscussionGroupMembershipsService
    {
        private readonly ApplicationDbContext db;

        public DiscussionGroupMembershipsService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public int CreateDiscussionGroupMembership(DiscussionGroupMembership discussionGroupMembership)
        {
            db.DiscussionGroupMemberships.Add(discussionGroupMembership);
            db.SaveChanges();
            db.Entry(discussionGroupMembership).GetDatabaseValues();
            return discussionGroupMembership.DiscussionGroupMembershipId;
        }

        public List<DiscussionGroupMembership> Read()
        {
            return db.DiscussionGroupMemberships
                .ToList();
        }

        public DiscussionGroupMembership FindDiscussionGroupMembership(int discussionGroupMembershipId)
        {
            try
            {
                return db.DiscussionGroupMemberships.Where(discussionGroupMembership => discussionGroupMembership.DiscussionGroupMembershipId == discussionGroupMembershipId)
                    .ToList()[0];
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public void UpdateDiscussionGroupMembership(DiscussionGroupMembership discussionGroupMembership)
        {
            DiscussionGroupMembership discussionGroupMembershipToUpdate = FindDiscussionGroupMembership(discussionGroupMembership.DiscussionGroupMembershipId);
            if (discussionGroupMembershipToUpdate != null)
            {
                discussionGroupMembershipToUpdate.DiscussionGroupId = discussionGroupMembership.DiscussionGroupId;
                discussionGroupMembershipToUpdate.DiscussionGroupMemberId = discussionGroupMembership.DiscussionGroupMemberId;

                db.DiscussionGroupMemberships.Update(discussionGroupMembershipToUpdate);
                db.SaveChanges();
            }
        }

        public void DeleteDiscussionGroupMembership(int discussionGroupMembershipId)
        {
            db.DiscussionGroupMemberships.Remove(FindDiscussionGroupMembership(discussionGroupMembershipId));
            db.SaveChanges();
        }
    }
}
*/
