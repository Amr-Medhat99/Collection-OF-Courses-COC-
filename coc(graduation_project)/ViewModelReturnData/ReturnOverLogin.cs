using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.ViewModelReturnData
{
    public class ReturnOverLogin
    {
        public int StudentID { get; set; }
        public int InstructorID { get; set; }
        public int CenterID { get; set; }
        public int FavoriteID { get; set; }
        public int CurrentCoursesID { get; set; }
        public int WatchLaterID { get; set; }
        public int CartID { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public bool admin { get; set; }
        public IEnumerable<string> Error { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
}
