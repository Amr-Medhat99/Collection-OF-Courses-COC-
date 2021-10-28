using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.Models
{
    public class Component
    {
        public int ComponentID { get; set; }
        public string ComponentName { get; set; }
        public virtual Package Package { get; set; }
        public int PackageID { get; set; }
    }
}
