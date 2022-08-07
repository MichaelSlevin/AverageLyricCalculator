namespace AireLogicTechTest.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private readonly IMusicBrainzClient _client;
        public ArtistRepository(IMusicBrainzClient client)
        {
            _client = client;
        }
        public async Task<string> GetArtistId(string artistName)
        {
            return (await _client.SearchArtistByName(artistName))
                .Artists
                .First()
                .Id;
        }

        public async Task<IEnumerable<string>> GetSongsByArtistId(string artistId)
        {
            throw new NotImplementedException();
        }
    }
}
