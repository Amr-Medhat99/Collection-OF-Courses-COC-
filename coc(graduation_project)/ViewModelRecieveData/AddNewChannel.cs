using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.ViewModelRecieveData
{
    public class AddNewChannel
    {
        public IFormFile logo { get; set; }
        [Required]
        public string CourseName { get; set; }
        [Required]
        public string ChannelName { get; set; }
        [Required]
        public string Link { get; set; }
        [Required]
        public int SubCategoryID { get; set; }
    }
}
