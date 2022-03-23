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
        [Display(Name = "Username")]
        public string UserName { get; set; }
        public CommunityUser User { get; set; }

        [Required]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }
    }

}