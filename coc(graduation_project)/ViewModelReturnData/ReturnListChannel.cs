using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.ViewModelReturnData
{
    public class ReturnListChannel
    {
        public List<ChannelData> lista{ get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get; set; }
        public IEnumerable<string> Error { get; set; }
        public DateTime? ExpireDate { get; set; }
    }
    public class ChannelData
    {
        public int ChannelID { get; set; }
        public string Logo { get; set; }
        public string CourseName { get; set; }
        public string ChannelName { get; set; }
        public string Link { get; set; }
    }

}
