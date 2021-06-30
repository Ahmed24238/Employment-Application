using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace website.Models
{
    public class contact
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [AllowHtml]
        public string phone { get; set; }
        public string Subject { get; set; }
        [Required]
        public string Message { get; set; }
        [Required]
        [Display(Name = "fileUploader")]
        public string fileUploader { get; set; }
    }
}