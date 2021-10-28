using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.Models
{
    public class Package
    {
        public int PackageID { get; set; }
        public virtual Course Course { get; set; }
        public int CourseID { get; set; }
        public int PackageNumber { get; set; }
        public double PackageCost { get; set; }
        public ICollection<CoursePackage> CoursePackage { get; set; }
        public ICollection<CurrentCoursePackage> CurrentCoursePackage { get; set; }

    }
}
