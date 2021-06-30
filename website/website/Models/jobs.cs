using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace website.Models
{
    public class jobs
    {
        public int id { get; set; }
        [Required]
        [Display(Name="Job Name")]
        public string JobName { get; set; }
        [Required]
        [Display(Name = "Job description")]
        public string jobdescription { get; set; }
       
        [Display(Name ="Job photo")]
        public string Jobphoto { get; set; }
        [Required]
        [Display(Name ="Job types")]
        public int categoryid { get; set; }
        public virtual category category { get; set; }

    }
}