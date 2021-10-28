using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.Models
{
    public class WatchLaterCourse
    {
        public int WatchLaterID { get; set; }
        public WatchLater WatchLater { get; set; }
        public int CourseID { get; set; }
        public Course Course { get; set; }
    }
}
