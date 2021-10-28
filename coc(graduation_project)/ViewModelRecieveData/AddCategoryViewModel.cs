using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.ViewModel
{
    public class AddCategoryViewModel
    {
        [Required]
        [Display(Name = "MainCategory Name")]
        public string MainCategoryName { get; set; }
        [Display(Name = "MainCategory Logo")]
        public IFormFile MainCategoryLogo { get; set; }
    }
}
