using System;
using System.ComponentModel.DataAnnotations;

namespace CommunityPortal.Models
{

    public class NewsPost
    {
        [Key]
        public int NewsPostId { get; set; }

        [Required]
        public int PostType { get; set; }

        public string Tag { get; set; }
        public string Description { get; set; }

        [Required]
        [Display(Name = "Header")]
        public string Heading { get; set; }
        [Required]
        [Display(Name = "Message")]
        public string Information { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public Category Category { get; set; }

        [Required]
        public string UserName { get; set; }
        public CommunityUser User { get; set; }

        [Required]
        public DateTime CreatedDate { get; set; }
        [Required]
        public DateTime UpdatedDate { get; set; }
    }

}