using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.Models
{
    public class Course
    {
        public int CourseID { get; set; }
        public string Logo { get; set; }
        public string CourseName { get; set; }
        public string RelasedDate { get; set; }
        public bool Online { get; set; }
        public double Price { get; set; }
        public bool Options { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public float Stars { get; set; }
        public string QA_Following { get; set; }
        public string QA_FollowingWN { get; set; }
        public string choice { get; set; }
        public bool status { get; set; }
        public virtual Instructor Instructor { get; set; }
        public int? InstructorID { get; set; }
        public virtual Center Center { get; set; }
        public int? CenterID { get; set; }

        public ICollection<WatchLaterCourse> WatchLaterCourse { get; set; }

        public virtual SubCategory SubCategory { get; set; }
        public int SubCategoryID { get; set; }

    }
}
