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
            var offset = 0;
            var resultsReturnedLimit = 100;
            int totalNumberOfResults = -1;

            while(totalNumberOfResults == -1 || offset < totalNumberOfResults) 
            {
                var works = await _client.GetWorksByArtistId(artistId, offset, resultsReturnedLimit);
                if(totalNumberOfResults == - 1)
                    totalNumberOfResults = works.WorkCount;
                offset = offset + resultsReturnedLimit;
            }
            return new List<string>();
        }
    }
}
