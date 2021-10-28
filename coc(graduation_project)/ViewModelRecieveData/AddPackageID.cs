using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.ViewModelRecieveData
{
    public class AddPackageID
    {
        [Required]
        [JsonProperty("PackageID")]
        public int PackageID { get; set; }
    }
    public class Root
    {
        [JsonProperty("PackageIDs")]
        public List<AddPackageID> PackageIDs { get; set; }
    }
}
