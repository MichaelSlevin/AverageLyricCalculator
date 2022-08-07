using Newtonsoft.Json;

namespace AireLogicTechTest.Repositories
{
    public interface IMusicBrainzClient
    {
         Task<ArtistSearchFullResponse> SearchArtistByName(string artistName);
         Task<WorksFullResponse> GetWorksByArtistId(string artistId, int offset, int resultsReturnedLimit);
        
    }

    public class MusicBrainzClient : IMusicBrainzClient
    {
        private readonly HttpClient _client;
        public MusicBrainzClient(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("musicbrainz");
            _client.BaseAddress = new Uri("https://musicbrainz.org");
            _client.DefaultRequestHeaders.Add("User-Agent", "TechTest/1.0.0 ( mikeslevin@gmail.com )");
        }

        public async Task<ArtistSearchFullResponse> SearchArtistByName(string artistName)
        {
            
            var response = await _client.GetAsync($"/ws/2/artist/?query={artistName}&fmt=json&limit=1");
            var artistSearchFullResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<ArtistSearchFullResponse>(artistSearchFullResponse);
        }

        public async Task<WorksFullResponse> GetWorksByArtistId(string artistId, int offset, int resultsReturnedLimit)
        {
            var response = await _client.GetAsync($"/ws/2/work?artist={artistId}&fmt=json&offset={offset}&limit={resultsReturnedLimit}");
            var worksFullResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<WorksFullResponse>(worksFullResponse);
        }
    }
}
