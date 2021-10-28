using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace coc_graduation_project_.ViewModelReturnData
{
    public class RootCurrentCourse
    {
        [JsonProperty("lista")]
        public List<ListaData> Lista { get; set; }

        [JsonProperty("issucess")]
        public bool Issucess { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("ExpireDate")]
        public string ExpireDate { get; set; }
    }
    public class ListaData
    {
        [JsonProperty("courseName")]
        public string CourseName { get; set; }

        [JsonProperty("Components")]
        public List<ComponentData> Components { get; set; }
    }
    public class ComponentData
    {
        [JsonProperty("ComponentID")]
        public int ComponentID { get; set; }

        [JsonProperty("ComponentName")]
        public string ComponentName { get; set; }

        [JsonProperty("Video")]
        public List<Video> Video { get; set; }
    }
    public class Video
    {
        [JsonProperty("VideoID")]
        public int VideoID { get; set; }

        [JsonProperty("VideoName")]
        public string VideoName { get; set; }

        [JsonProperty("VideoURL")]
        public string VideoURL { get; set; }
    }
}
