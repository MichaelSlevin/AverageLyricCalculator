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

            var works = new List<WorkResponse>();

            while(totalNumberOfResults == -1 || offset < totalNumberOfResults) 
            {
                var response = await _client.GetWorksByArtistId(artistId, offset, resultsReturnedLimit);
                works.AddRange(response.Works);
                if(totalNumberOfResults == - 1)
                    totalNumberOfResults = response.WorkCount;
                offset = offset + resultsReturnedLimit;
            }

            return works.Where(x => x.Type == "Song").Select(x => x.Title);
        }
    }
}
