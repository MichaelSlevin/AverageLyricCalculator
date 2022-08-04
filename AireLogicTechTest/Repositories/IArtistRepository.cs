namespace AireLogicTechTest.Repositories
{
    public interface IArtistRepository
    {
        string GetArtistId(string artistName);
        IEnumerable<string> GetSongsByArtistId(string artistId);
    }
}
