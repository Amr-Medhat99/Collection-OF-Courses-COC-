using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.Models
{
    public class HelpFullChannel
    {
        public int ID { get; set; }
        public string logo { get; set; }
        public string courseName { get; set; }
        public string channelName { get; set; }
        public string Link { get; set; }
        public virtual SubCategory SubCategory { get; set; }
        public int SubCategoryID { get; set; }
    }
}
