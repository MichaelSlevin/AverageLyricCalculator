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
    public async Task GetSongsByArtist_Calls_IArtistReposity_GetArtistID(
        Mock<IArtistRepository> artistRepository,
        string artistName
    )
    {
        var expected = "expected";
        artistRepository.Setup(x => x.GetArtistId(artistName)).ReturnsAsync(expected);
        var service = new ArtistService(artistRepository.Object);

        await service.GetSongsByArtist(artistName);

        artistRepository.Verify(
            (x) => x.GetArtistId(artistName),
            Times.Once
        );
        
    }

    [Test]
    [AutoData]
    public async Task GetSongsByArtist_Calls_IArtistReposity_GetSongsByArtistId_with_the_correct_id(
        Mock<IArtistRepository> artistRepository,
        string artistName
    )
    {
        var expected = "expected";
        artistRepository.Setup(x => x.GetArtistId(artistName)).ReturnsAsync(expected);
        var service = new ArtistService(artistRepository.Object);

        await service.GetSongsByArtist(artistName);

        artistRepository.Verify(
            (x) => x.GetSongsByArtistId(expected),
            Times.Once
        );
    }

    [Test]
    [AutoData]
    public async Task GetSongsByArtist_Returns_the_correct_songs(
        Mock<IArtistRepository> artistRepository,
        string artistName,
        string artistId,
        List<string> expectedSongList
    )
    {
        artistRepository.Setup(x => x.GetArtistId(artistName)).ReturnsAsync(artistId);
        artistRepository.Setup(x => x.GetSongsByArtistId(artistId)).ReturnsAsync(expectedSongList);
        var service = new ArtistService(artistRepository.Object);

        var result = await service.GetSongsByArtist(artistName);

        result.Should().BeEquivalentTo(expectedSongList);
    }
}