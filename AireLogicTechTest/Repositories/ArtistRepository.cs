namespace AireLogicTechTest.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly IMusicBrainzClient _client;
        public ArtistRepository(IMusicBrainzClient client)
        {
            _client = client;
        }
        public string GetArtistId(string artistName)
        {
            return _client
                .SearchArtistByName(artistName)
                .Artists
                .First()
                .Id;
        }

        public IEnumerable<string> GetSongsByArtistId(string artistId)
        {
            throw new NotImplementedException();
        }
    }
}
