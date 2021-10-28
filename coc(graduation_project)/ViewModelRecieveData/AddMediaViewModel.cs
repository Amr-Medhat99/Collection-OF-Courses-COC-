using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.ViewModel
{
    public class AddMediaViewModel
    {
        [Required]
        public IFormFile videoURL { get; set; }

        [Required]
        public string videoName { get; set; }
    }
}
