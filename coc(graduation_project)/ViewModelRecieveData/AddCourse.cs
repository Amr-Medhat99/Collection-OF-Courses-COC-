using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.ViewModelRecieveData
{
    public class AddCourse
    { 
        public IFormFile logoo { get; set; }
        [Required]
        public string courseName{ get; set; }
        [Required]
        public double price { get; set; }
        [Required]
        public bool options { get; set; }
        [Required]
        public string choice { get; set; }
        [Required]
        public string description { get; set; }
        [Required]
        public string requirements { get; set; }
    }
}
