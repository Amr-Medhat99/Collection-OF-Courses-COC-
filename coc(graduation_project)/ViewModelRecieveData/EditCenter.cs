using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.ViewModelRecieveData
{
    public class EditCenter
    {
        [Required]
        public IFormFile logo { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string phone { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string address { get; set; }
    }
    public class EditInstructor
    {
        [Required]
        public IFormFile logo { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string phone { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public IFormFile cv { get; set; }
    }
    public class EditStudent
    {
        [Required]
        public IFormFile logo { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string phone { get; set; }
        [Required]
        public string email { get; set; }
    }
}
