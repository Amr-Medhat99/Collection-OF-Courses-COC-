using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.ViewModel
{
    public class AddCourseViewModel
    {
        [Required]
        [Display(Name = "Course Name")]
        [StringLength(30, ErrorMessage = "Max Length MustBe Less Than 30 Character And Greater Than 5 Character", MinimumLength = 5)]
        public string CourseName { get; set; }

        [Required]
        [Display(Name = "Course Require")]
        [StringLength(100, ErrorMessage = "Max Length MustBe Less Than 100 Character And Greater Than 5 Character", MinimumLength = 5)]
        public string CourseRequire{ get; set; }

        [Required]
        [Display(Name = "Course Description")]
        [StringLength(100, ErrorMessage = "Max Length MustBe Less Than 100 Character And Greater Than 5 Character", MinimumLength = 5)]
        public string CourseDescription{ get; set; }

        [Required]
        [Display(Name = "Course Category")]
        public int CourseSubCategory{ get; set; }
    }
}
