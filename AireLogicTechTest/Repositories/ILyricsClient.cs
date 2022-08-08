namespace AireLogicTechTest.Repositories
{
    public interface ILyricsClient
    {
        Task<string> GetLyrics(string artistName, string songTitle);
    }
}
