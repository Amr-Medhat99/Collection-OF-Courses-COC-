using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.Models
{
    public class media
    {
        public int MediaID { get; set; }
        public string VideoName { get; set; }
        public string VideoURL { get; set; }
        public virtual Component Component { get; set; }
        public int ComponentID { get; set; }
    }
}
