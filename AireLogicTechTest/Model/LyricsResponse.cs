using Newtonsoft.Json;

namespace AireLogicTechTest.Repositories
{
    public class LyricsResponse
    {
        [JsonProperty("lyrics")]
        public string Lyrics {get; set;}
    }
}