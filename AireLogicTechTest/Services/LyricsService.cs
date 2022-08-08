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

            Parallel.ForEach(
                songTitles, 
                new ParallelOptions { MaxDegreeOfParallelism = 100},
                async (title) => {
                    try {
                        var lyrics = _client.GetLyrics(artistName, title).Result;
                        var lyricsCount = _countingService.CleanAndCountLyrics(lyrics);
                        lyricsCountList.Add(lyricsCount);
                    } 
                    catch (AggregateException e)
                    {
                        Console.WriteLine(e.InnerException.Message);
                        Interlocked.Increment(ref songsNotFound);
                    }
                }
            );
            return new AverageWordCountResponse {
                AverageWordCount = lyricsCountList.Count() > 0 ? lyricsCountList.Average() : -1,
                NumberOfSongsNotFound = songsNotFound
            };
        }
    }
}
