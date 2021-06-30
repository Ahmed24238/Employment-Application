using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication2.Models;

namespace website.Models
{
    public class jobs
    {
        public int id { get; set; }
        [Required]
        [Display(Name = "Job Name")]
        public string JobName { get; set; }
        [Required]
        [Display(Name = "Job description")]
        [AllowHtml]
        public string jobdescription { get; set; }

        [Display(Name = "Job photo")]
        public string Jobphoto { get; set; }
        [Required]
        [Display(Name = "Job types")]
        public int categoryid { get; set; }
        public virtual category category { get; set; }
        public virtual ApplicationUser User { get; set; }
        public string UserId { get; set; }

        internal static object ToList()
        {
            throw new NotImplementedException();
        }
    }

}