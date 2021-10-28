using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.Models
{
    public class Comment
    {
        public int CommentID { get; set; }
        public float understand_rate { get; set; }
        public string missing_explain { get; set; }
        public string missing_answers { get; set; }
        public virtual media Video { get; set; }
        public int VideoID { get; set; }
        public virtual Student Student { get; set; }
        public int StudentID { get; set; }

    }
}
