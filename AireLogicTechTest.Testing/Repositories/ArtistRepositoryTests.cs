using AireLogicTechTest.Repositories;

namespace AireLogicTechTest.Testing.Services;

public class ArtistRepositoryTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    [AutoData]
    public async Task GetArtistId_Calls_The_Client(
        Mock<IMusicBrainzClient> client,
        IEnumerable<ArtistResponse> artistListRepsonse,
        string artistName
    )
    {
        var response = new ArtistSearchFullResponse {
            Artists = artistListRepsonse
        };
        client.Setup(x => x.SearchArtistByName(artistName)).ReturnsAsync(response);
        var repo = new ArtistRepository(client.Object);
        await repo.GetArtistId(artistName);
        client.Verify(
            (x) => x.SearchArtistByName(artistName),
            Times.Once
        );
    }

    [Test]
    [AutoData]
    public async Task GetArtistId_Returns_The_Correct_Id(
        Mock<IMusicBrainzClient> client,
        ArtistResponse artistResponse,
        string artistName
    )
    {
        var response = new ArtistSearchFullResponse {
            Artists = new List<ArtistResponse> {
                artistResponse
            }
        };
        client.Setup(x => x.SearchArtistByName(artistName)).ReturnsAsync(response);
        var repo = new ArtistRepository(client.Object);
        var result = await repo.GetArtistId(artistName);
        
        result.Should().Be(artistResponse.Id);
    }
}