using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.Models
{
    public class CurrentCoursePackage
    {
        public int CurrentCourseID { get; set; }
        public CurrentCourse CurrentCourse { get; set; }
        public int PackageID { get; set; }
        public Package Package { get; set; }

    }
}
