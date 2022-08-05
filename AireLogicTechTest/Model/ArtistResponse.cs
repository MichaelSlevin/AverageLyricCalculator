using Newtonsoft.Json;

namespace AireLogicTechTest.Repositories
{
    public class ArtistResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }

    }

    public class ArtistSearchFullResponse
    {
        [JsonProperty("artists")]
        public IEnumerable<ArtistResponse> Artists {get; set;}
    }
}