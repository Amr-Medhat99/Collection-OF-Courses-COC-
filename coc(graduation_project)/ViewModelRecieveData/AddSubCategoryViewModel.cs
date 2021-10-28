using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.ViewModelRecieveData
{
    public class AddSubCategoryViewModel
    {
        [Required]
        [Display(Name = "SubCategory Name")]
        public string SubCategoryName { get; set; }
        [Display(Name = "SubCategory Logo")]
        public IFormFile SubCategoryLogo { get; set; }
    }
}
