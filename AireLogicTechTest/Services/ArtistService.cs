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
        public async Task<IEnumerable<string>> GetSongsByArtist(string artistName)
        {
            return await _artistRepo.GetSongsByArtistId(
                await _artistRepo.GetArtistId(artistName)
            );
        }
    }
}
