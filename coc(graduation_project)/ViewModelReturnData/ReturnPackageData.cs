using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.ViewModelReturnData
{
    public class ReturnPackageData
    {
        [JsonProperty("lista")]
        public List<Lista> lista { get; set; }
        [JsonProperty("message")]
        public string Message { get; set; }
        [JsonProperty("issuccess")]
        public bool IsSuccess { get; set; }
        [JsonProperty("error")]
        public IEnumerable<string> Error { get; set; }
        [JsonProperty("expireDate")]
        public DateTime? ExpireDate { get; set; }
    }
    public class Lista
    {
        [JsonProperty("PackageID")]
        public int PackageID { get; set; }

        [JsonProperty("PackageNumber")]
        public int PackageNumber { get; set; }

        [JsonProperty("PackageCost")]
        public double PackageCost { get; set; }

        [JsonProperty("Components")]
        public List<Components> Components { get; set; }
    }
    public class Components
    {
        [JsonProperty("ComponentName")]
        public string ComponentName { get; set; }
    }
}
