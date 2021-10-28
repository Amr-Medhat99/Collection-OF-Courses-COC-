using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.ViewModelReturnData
{
    public class ReturnCartData
    {
        public List<PackageCartData> lista { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Error { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
    public class PackageCartData
    {
        public int PackageID { get; set; }
        public int PackageNumber { get; set; }
        public double PackageCost { get; set; }
        public List<ComponentCartData> Components { get; set; }

    }
    public class ComponentCartData
    {
        public string ComponentName { get; set; }

    }
}