using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.ViewModel
{
    public class BranchViewModel
    {
        [Required]
        public string branchPhone { get; set; }
        [Required]
        public string branchAddress { get; set; }
        [Required]
        public string branchName { get; set; }
    }
}
