using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.Models
{
    public class News
    {
        public int id { get; set; }
        public string logo { get; set; }
        public string modified_date { get; set; }
        public string what_is_new { get; set; }
        public virtual Course Course { get; set; }
        public int CourseID { get; set; }
    }
}
