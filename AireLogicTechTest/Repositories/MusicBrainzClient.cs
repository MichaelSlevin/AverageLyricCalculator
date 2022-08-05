namespace AireLogicTechTest.Repositories
{
    public interface IMusicBrainzClient
    {
        ArtistSearchFullResponse SearchArtistByName(string artistName);
        
    }

    public class MusicBrainzClient : IMusicBrainzClient
    {
        public ArtistSearchFullResponse SearchArtistByName(string artistName)
        {
           throw new NotImplementedException();
        }
    }
}
