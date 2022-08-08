using Newtonsoft.Json;

namespace AireLogicTechTest.Repositories
{

    public class LyricsClient : ILyricsClient
    {
      private readonly HttpClient _client;
        public LyricsClient(IHttpClientFactory httpClientFactory)
        {
            _client = httpClientFactory.CreateClient("lyrics-ovh");
            _client.BaseAddress = new Uri("https://api.lyrics.ovh");
        }
        public async Task<string> GetLyrics(string artistName, string songTitle)
        {
            var response = await _client.GetAsync($"/ws/2/artist/?query={artistName}&fmt=json&limit=1");
            if(!response.IsSuccessStatusCode) {
                throw new SongNotFoundException($"No results from for {songTitle} by {artistName}");
            }
            var artistSearchFullResponse = await response.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<LyricsResponse>(artistSearchFullResponse).Lyrics;
        }
    }
}
