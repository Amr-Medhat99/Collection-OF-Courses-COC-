using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.Models
{
    public class Instructor
    {
        public int InstructorId { get; set; }
        public string InstructorName{ get; set; }
        public string cv{ get; set; }
        public string logo{ get; set; }
        public virtual IdentityUser AppUser { get; set; }
        public string AppUserId { get; set; }
       

    }
}
