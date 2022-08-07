namespace AireLogicTechTest.Repositories
{
    public interface IArtistRepository
    {
        Task<string> GetArtistId(string artistName);
        Task<IEnumerable<string>> GetSongsByArtistId(string artistId);
    }
}
