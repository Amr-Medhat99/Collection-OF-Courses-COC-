using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.Models
{
    public class Center
    {
        public int CenterId { get; set; }
        public string CenterName { get; set; }
        public string address { get; set; }
        public string logo { get; set; }
        public virtual IdentityUser AppUser { get; set; }
        public string AppUserId { get; set; }
        //public ICollection<CourseCenter> CourseCenter { get; set; }
    }
}