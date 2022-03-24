using System;
using System.ComponentModel.DataAnnotations;

namespace CommunityPortal.Models
{

    public class PrivateMessage
    {
        [Key]
        public int PrivateMessageId { get; set; }

        [Required]
        [Display(Name = "Header")]
        public string Heading { get; set; }

        [Required]
        [Display(Name = "Message")]
        public string Message { get; set; }

        [Required]
        [Display(Name = "From")]
        public string FromUserName { get; set; }
        public CommunityUser FromUser { get; set; }

        [Required]
        [Display(Name = "To")]
        public string ToUserName { get; set; }
        public CommunityUser ToUser { get; set; }

        [Required]
        [Display(Name = "Date")]
        public DateTime Date { get; set; }
    }

}