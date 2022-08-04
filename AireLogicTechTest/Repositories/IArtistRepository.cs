namespace AireLogicTechTest.Repositories
{
    public interface IArtistRepository
    {
        string GetArtistId(string artistName);
        string GetSongsByArtistId(string artistId);
    }
}
