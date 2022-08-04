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

    [Test]
    [AutoData]
    public void Calls_IArtistReposity_GetSongsByArtistId_with_the_correct_id(
        Mock<IArtistRepository> artistRepository,
        string artistName
    )
    {
        var expected = "expected";
        artistRepository.Setup(x => x.GetArtistId(artistName)).Returns(expected);
        var service = new ArtistService(artistRepository.Object);

        service.GetSongsByArtist(artistName);

        artistRepository.Verify(
            (x) => x.GetSongsByArtistId(expected),
            Times.Once
        );
        
    }
}