using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.ViewModelReturnData
{
    public class ReturnStudentProfile
    {
        public string Logo { get; set; }
        public string userName { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public List<SubCategoryData> favorites { get; set; }
        public List<NewsListData> notifications { get; set; }
        public List<CourseListData> watchLater { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Error { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
    public class SubCategoryData
    {
        public int SubCategoryID { get; set; }
        public string SubCategoryName { get; set; }
        public string SubCategoryLogo { get; set; }
    }
    public class SubCategoryDataList
    {
        public List<SubCategoryData> SubCatObject{ get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Error { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
