using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.ViewModelRecieveData
{
    public class AddVideo
    {
        [Required]
        public string videoName { get; set; }
        public IFormFile videoURL { get; set; }
    }
}
