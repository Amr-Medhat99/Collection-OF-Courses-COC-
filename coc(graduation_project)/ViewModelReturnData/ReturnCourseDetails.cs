using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.ViewModelReturnData
{
    public class ReturnCourseDetails
    {
        public int CourseID { get; set; }
        public string Logo { get; set; }
        public string CourseName { get; set; }
        public string RelasedDate { get; set; }
        public string InstructorName { get; set; }
        public string CV { get; set; }
        public string QA_FollowingWN { get; set; }
        public bool Online { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public double Price { get; set; }
        public bool Options { get; set; }
        public float Stars { get; set; }
        public string Description { get; set; }
        public string Requirements { get; set; }
        public string Email { get; set; }
        public List<FreeVideosCourseDetails> FreeVideos { get; set; }
        public List<ComponentCourseDetails> Components { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Error { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
    public class FreeVideosCourseDetails
    {
        public int VideoID { get; set; }
        public string VideoName { get; set; }
        public string VideoURL { get; set; }
    }
    public class ComponentCourseDetails
    {
        public int ComponentID { get; set; }
        public string ComponentName { get; set; }
        public List<VideoCourseDetails> Video { get; set; }
    }
    public class VideoCourseDetails
    {
        public int VideoID { get; set; }
        public string VideoName { get; set; }
        public string VideoURL  { get; set; }
    }
}
