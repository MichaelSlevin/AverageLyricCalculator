using AireLogicTechTest.Repositories;

namespace AireLogicTechTest.Services
{
    public class ArtistService : IArtistService
    {

        private readonly IArtistRepository _artistRepo;

        public ArtistService(IArtistRepository artistRepo)
        {
            _artistRepo = artistRepo;
        }
        public IEnumerable<string> GetSongsByArtist(string artistName)
        {
            _artistRepo.GetSongsByArtistId(
                _artistRepo.GetArtistId(artistName)
            );
            return new List<string>();
        }
    }
}
