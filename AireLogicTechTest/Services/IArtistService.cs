namespace AireLogicTechTest.Services
{
    public interface IArtistService
    {
        IEnumerable<string> GetSongsByArtist(string artistName);
    }
}
