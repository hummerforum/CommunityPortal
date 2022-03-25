using System.ComponentModel.DataAnnotations;

namespace CommunityPortal.Models
{

    public class Category
    {
        [Key]
        public int CategoryId { get; set; }

        [Required]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        public int ParentId { get; set; }
    }

}