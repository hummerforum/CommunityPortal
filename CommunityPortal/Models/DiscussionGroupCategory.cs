using System.ComponentModel.DataAnnotations;

namespace CommunityPortal.Models
{
    public class DiscussionGroupCategory
    {
        public DiscussionGroupCategory(int discussionGroupId, int categoryId)
        {
            DiscussionGroupId = discussionGroupId;
            CategoryId = categoryId;
        }

        [Key]
        public int DiscussionGroupCategoryId { get; set; }

        [Required]
        public int DiscussionGroupId { get; set; }

        [Required]
        public int CategoryId { get; set; }

        public DiscussionGroup DiscussionGroup { get; set; }

        public Category Category { get; set; }
    }
}
