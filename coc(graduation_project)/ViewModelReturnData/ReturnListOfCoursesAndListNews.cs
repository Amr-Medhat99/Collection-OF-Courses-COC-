using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.ViewModelReturnData
{
    public class ReturnListOfCoursesAndListNews
    {
        public List<CourseListData> c_list { get; set; }
        public List<NewsListData> n_list { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Error { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
    public class CourseListData
    {
        public int CourseID { get; set; }
        public string Logo { get; set; }
        public string CourseName { get; set; }
        public string RelasedDate { get; set; }
        public string InstructorName { get; set; }
        public string CenterName { get; set; }
        public string QA_Following { get; set; }
        public bool Online { get; set; }
        public string Address { get; set; }
        public double Price { get; set; }
        public bool Options { get; set; }
        public float Stars { get; set; }
    }
    public class ListOfCourseForAdmin
    {
        public List<CourseListData> c_list { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Error { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
    public class NewsListData
    {
        public int id { get; set; }
        public string course_name { get; set; }
        public string logo { get; set; }
        public string instructor_name { get; set; }
        public string center_name { get; set; }
        public string modified_date { get; set; }
        public string  what_is_new { get; set; }
    }
}