using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace website.Models
{
    public class category
    {
        public int id { get; set; }
        [Required]
        [Display(Name ="Category type")]
        public string categoryName { get; set; }
        [Required]
        [Display(Name = "Category Description")]
        public string Description { get; set; }
        public virtual ICollection<jobs>jobs { get; set; }

    }
}