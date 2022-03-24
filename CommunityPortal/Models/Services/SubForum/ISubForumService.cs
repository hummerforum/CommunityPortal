using CommunityPortal.Models;
using System.Collections.Generic;

namespace CommunityPortal.Model.Services
{
    public interface ISubForumService
    {
        public int CreateSubForum(SubForum subForum);

        public List<SubForum> Read();

        public List<SubForum> FindSubForum(string searchString, bool caseSensitive);

        public SubForum FindSubForum(int subForumId);

        public SubForum FindSubForum(string subForumName);

        public void UpdateSubForum(SubForum subForum);

        public void DeleteSubForum(int subForumId);
    }
}
