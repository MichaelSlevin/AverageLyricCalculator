namespace AireLogicTechTest.Services
{
    public interface ILyricsService
    {
        Task<AverageWordCountResponse> GetAverageWordCountForArtistAndSongs(string artistName, IEnumerable<string> songTitles);
    }

    public class AverageWordCountResponse 
    {
        public double AverageWordCount { get; set; }
        public int  NumberOfSongsNotFound { get; set; }
    }
}
