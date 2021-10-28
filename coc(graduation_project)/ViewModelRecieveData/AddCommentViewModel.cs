using coc_graduation_project_.Models;
using coc_graduation_project_.ViewModelRecieveData;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.ViewModel
{
    public class AddCommentViewModel
    {
        [Required]
        public string missing_Explain{ get; set; }
        [Required]
        public string missing_Answer { get; set; }
        [Required]
        public float understand_Rate { get; set; }
    }
}
