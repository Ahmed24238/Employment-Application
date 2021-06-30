using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace website.Models
{
    public class jobapper
    {
        public string JobName { get; set; }
        public IEnumerable <ApplyForJob> Job { get; set; }
    }
}