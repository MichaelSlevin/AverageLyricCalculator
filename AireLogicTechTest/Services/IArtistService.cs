namespace AireLogicTechTest.Services
{
    public interface IArtistService
    {
        Task<IEnumerable<string>> GetSongsByArtist(string artistName);
    }
}
