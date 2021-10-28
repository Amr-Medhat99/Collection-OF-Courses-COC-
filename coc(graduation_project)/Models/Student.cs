using coc_graduation_project_.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.Models
{
    public class Student
    {
        [Key]
        public int StudentId { get; set; }
        public string StudentName { get; set; }
        public string Picture { get; set; }
        public virtual Cart cart { get; set; }
        public virtual WatchLater watchLater{ get; set; }
        public virtual CurrentCourse CurrentCourse { get; set; }
        public virtual IdentityUser AppUser { get; set; }
        public virtual Favorite Favorite{ get; set; }
        public string AppUserId { get; set; }
    }
}
