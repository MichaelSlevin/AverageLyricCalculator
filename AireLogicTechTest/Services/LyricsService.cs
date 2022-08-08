using System.Collections.Concurrent;
using AireLogicTechTest.Repositories;

namespace AireLogicTechTest.Services
{
    public class LyricsService : ILyricsService
    {
        private readonly ILyricsClient _client;
        private readonly ILyricsCountingService _countingService;

        public LyricsService(ILyricsClient client, ILyricsCountingService countingService)
        {
            _client = client;
            _countingService = countingService;
            
        }
        
        public async Task<AverageWordCountResponse> GetAverageWordCountForArtistAndSongs(string artistName, IEnumerable<string> songTitles)
        {

            var lyricsCountList = new ConcurrentBag<int>();
            int songsNotFound = 0;
            Parallel.ForEach(songTitles, async (title)=> {
                try {
                    var lyrics = await _client.GetLyrics(artistName, title);
                    var lyricsCount = _countingService.CleanAndCountLyrics(lyrics);
                    lyricsCountList.Add(lyricsCount);
                } catch (SongNotFoundException e)
                {
                    Console.WriteLine(e.Message);
                    Interlocked.Increment(ref songsNotFound);
                }
            });
            return new AverageWordCountResponse {
                AverageWordCount = lyricsCountList.Average(),
                NumberOfSongsNotFound = songsNotFound
            };
        }
    }
}
