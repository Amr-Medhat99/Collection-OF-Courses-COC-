using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.Models
{
    public class WatchLater
    {
        public int WatchLaterId { get; set; }
        public Student student { get; set; }
        public int studentID { get; set; }
        public ICollection<WatchLaterCourse> WatchLaterCourse { get; set; }
    }
}
