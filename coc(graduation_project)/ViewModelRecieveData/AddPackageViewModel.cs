using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.ViewModel
{
    public class AddPackageViewModel
    {
        [Required]
        public int packageNumber { get; set; }
        [Required]
        public double packageCost { get; set; }
    }
    //public class ReturnDataOfPackage
    //{
    //    public int packageID { get; set; }
    //    public int packageNumber { get; set; }
    //    public double packageCost { get; set; }
    //    public string Message { get; set; }
    //    public bool IsSuccess { get; set; }
    //    public IEnumerable<string> Error { get; set; }
    //    public DateTime? ExpireDate { get; set; }
    //}
}
