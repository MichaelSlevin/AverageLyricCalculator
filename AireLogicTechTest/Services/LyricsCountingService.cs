namespace AireLogicTechTest.Services
{
    public class LyricsCountingService : ILyricsCountingService
    {
        public int CleanAndCountLyrics(string lyrics)
        {
            lyrics = lyrics.Replace("\r", " ").Replace("\n", " ");
            var lyricsSplit = lyrics.Split(" ", StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);
            return lyricsSplit.Count();
        }
    }
}
