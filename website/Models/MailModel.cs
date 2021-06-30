using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
namespace SendMailwithAttachment.Models
{
    public class MailModel
    {
        [Required]
        [Display(Name = "Category type")]
        public string To { get; set; }
        [Required]
        [Display(Name = "Subject")]
        public string Subject { get; set; }
        [Required]
        [Display(Name = "Message and Communication methods")]
        public string Body { get; set; }
    }
}