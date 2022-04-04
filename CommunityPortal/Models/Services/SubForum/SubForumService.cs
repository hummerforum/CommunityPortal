using CommunityPortal.Data;
using CommunityPortal.Model.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CommunityPortal.Models.Services
{
    public class SubForumService : ISubForumService
    {
        private readonly ApplicationDbContext db;

        public SubForumService(ApplicationDbContext db)
        {
            this.db = db;
        }

        public int CreateSubForum(SubForum subForum)
        {
            db.SubForum.Add(subForum);
            db.SaveChanges();
            db.Entry(subForum).GetDatabaseValues();
            return subForum.SubForumId;
        }

        public List<SubForum> Read()
        {
            return db.SubForum
                .ToList();
        }

        public List<SubForum> FindSubForum(string searchString, bool caseSensitive)
        {
            List<SubForum> subForumToReturn = new List<SubForum>();
            List<SubForum> subForumList = db.SubForum.Where(subForum =>
                subForum.Name.Contains(searchString) ||
                subForum.Description.Contains(searchString)
                )
                .ToList();

            if (caseSensitive)
            {
                foreach (var subForum in subForumList)
                {
                    if (subForum.Name.Contains(searchString) || subForum.Description.Contains(searchString))
                    {
                        subForumToReturn.Add(subForum);
                    }
                }
            }
            else
            {
                subForumToReturn = subForumList;
            }

            return subForumToReturn;
        }

        public SubForum FindSubForum(int subForumId)
        {
            try
            {
                return db.SubForum.Where(subForum => subForum.SubForumId == subForumId)
                    .ToList()[0];
            }
            catch (ArgumentNullException)
            {
                return null;
            }
        }

        public SubForum FindSubForum(string subForumName)
        {
            return db.SubForum.FirstOrDefault(subForum => subForum.Name.Trim().ToLower() == subForumName.Trim().ToLower());
        }

        public void UpdateSubForum(SubForum subForum)
        {
            SubForum subForumToUpdate = FindSubForum(subForum.SubForumId);
            if (subForumToUpdate != null)
            {
                subForumToUpdate.Name = subForum.Name;
                subForumToUpdate.Description = subForum.Description;

                db.SubForum.Update(subForumToUpdate);
                db.SaveChanges();
            }
        }

        public void DeleteSubForum(int subForumId)
        {
            db.SubForum.Remove(FindSubForum(subForumId));
            db.SaveChanges();
        }
    }
}
