using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.ViewModelReturnData
{
    public class ReturnCenterProfile
    {
        public string logo { get; set; }
        public string userName { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string address { get; set; }
        public List<CourseListData> myCourses { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Error { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
