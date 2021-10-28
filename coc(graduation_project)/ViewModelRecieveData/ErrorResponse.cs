using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.ViewModel
{
    public class ErrorResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Error { get; set; }
        public DateTime? ExpireDate{ get; set; }
    }
    public class CourseErrorResponse
    {
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Error { get; set; }
        public int courseID { get; set; }
    }
}