using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.Models
{
    public class CurrentCourse
    {
        public int CurrentCourseID { get; set; }
        public Student student{ get; set; }
        public int studentID { get; set; }
        public ICollection<CurrentCoursePackage> CurrentCourseCourse { get; set; }
    }
}
