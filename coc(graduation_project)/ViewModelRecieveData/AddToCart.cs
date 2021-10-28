using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.ViewModel
{
    public class AddToCart
    {
        [Required]
        public int CartID { get; set; }
        [Required]
        public int PackageID { get; set; }
    }
}
