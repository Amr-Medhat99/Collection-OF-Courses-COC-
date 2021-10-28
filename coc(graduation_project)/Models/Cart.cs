using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.Models
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        public virtual Student Student { get; set; }
        public int StudentId { get; set; }
        public ICollection<CoursePackage> CourseCart { get; set; }

    }
}
