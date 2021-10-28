using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.Models
{
    public class CoursePackage
    {
        public int CartId { get; set; }
        public Cart Cart { get; set; }
        public int PackageID { get; set; }
        public Package Package { get; set; }
    }
}