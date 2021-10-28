using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.Models
{
    public class QAndAVideo
    {
        public int QAndAVideoID { get; set; }
        public string VideoName { get; set; }
        public string VideoURL { get; set; }
        public virtual media Video { get; set; }
        public int VideoID { get; set; }
    }
}
