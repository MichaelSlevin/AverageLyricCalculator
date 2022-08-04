using AireLogicTechTest.Repositories;
using AireLogicTechTest.Services;

namespace AireLogicTechTest.Testing.Services;

public class ArtistServiceTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    [AutoData]
    public void Calls_IArtistReposity_GetArtistID(
        Mock<IArtistRepository> artistRepository,
        string artistName
    )
    {
        var expected = "expected";
        artistRepository.Setup(x => x.GetArtistId(artistName)).Returns(expected);
        var service = new ArtistService(artistRepository.Object);

        service.GetSongsByArtist(artistName);

        artistRepository.Verify(
            (x) => x.GetArtistId(artistName),
            Times.Once
        );
        
    }
}