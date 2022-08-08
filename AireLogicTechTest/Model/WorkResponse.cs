using Newtonsoft.Json;

namespace AireLogicTechTest.Repositories
{
    public class WorkResponse
    {
        [JsonProperty("title")]
        public string Title { get; set; }
        
        [JsonProperty("type")]
        public string Type { get; set; }

    }

    public class WorksFullResponse
    {
        [JsonProperty("works")]
        public IEnumerable<WorkResponse> Works { get; set; }

        [JsonProperty("work-count")]
        public int WorkCount { get; set; }
    }

}