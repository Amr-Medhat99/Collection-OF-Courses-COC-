using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.Models
{
    public class Branch
    {
        public int BranchId { get; set; }
        public string BranchPhone { get; set; }
        public string BranchAddress { get; set; }
        public string BranchName { get; set; }
        public virtual Center Center { get; set; }
        public int CenterId { get; set; }
    }
}
