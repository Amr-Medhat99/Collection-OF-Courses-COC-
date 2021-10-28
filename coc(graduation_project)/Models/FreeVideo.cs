using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.Models
{
    public class FreeVideo
    {
        public int FreeVideoID{ get; set; }
        public string FreeVideoName { get; set; }
        public string FreeVideoURL { get; set; }
        public virtual Component Component { get; set; }
        public int ComponentID { get; set; }

    }
}
