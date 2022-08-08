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

    [Test]
    [AutoData]
    public async Task GetSongsByArtistId_Calls_Client_GetWorksByArtistId_WithCorrectArtistIdInitialOffsetAndLimit(
        Mock<IMusicBrainzClient> client,
        string artistId,
        WorkResponse work1
    )
    {
        var worksFullResponse = new WorksFullResponse {
            Works = new List<WorkResponse> {
                work1
            },
            WorkCount = 1
        };

        client.Setup(x => x.GetWorksByArtistId(artistId, 0, 100))
            .ReturnsAsync(worksFullResponse);
        var repo = new ArtistRepository(client.Object);
        var result = await repo.GetSongsByArtistId(artistId);
        
        client.Verify(x => 
            x.GetWorksByArtistId(artistId, 0, 100), 
            Times.Once
        );
    }

    [Test]
    [AutoData]
    public async Task GetSongsByArtistId_Calls_Client_GetWorksByArtistId_UntilAllTheResultsHaveBeenFetched(
        Mock<IMusicBrainzClient> client,
        string artistId,
        WorkResponse workResponse
    )
    {
        var worksFullResponse = new WorksFullResponse();
        //if there are 290 results, then it should be called 3 times
        worksFullResponse.WorkCount = 290;
        var works = new List<WorkResponse>();

        for (int i = 0; i < 100; i++)
        {
            works.Add(workResponse);
        }

        worksFullResponse.Works = works;

        client.Setup(x => x.GetWorksByArtistId(artistId, It.IsAny<int>(), 100))
            .ReturnsAsync(worksFullResponse);
        var repo = new ArtistRepository(client.Object);
        var result = await repo.GetSongsByArtistId(artistId);
        
        client.Verify(x => 
            x.GetWorksByArtistId(artistId, 0, 100), 
            Times.Once
        );
        client.Verify(x => 
            x.GetWorksByArtistId(artistId, 100, 100), 
            Times.Once
        );
        client.Verify(x => 
            x.GetWorksByArtistId(artistId, 200, 100), 
            Times.Once
        );
    }

    [Test]
    [AutoData]
    public async Task GetSongsByArtistId_Calls_Client_GetWorksByArtistId_OnceIf0ThereAreResults(
        Mock<IMusicBrainzClient> client,
        string artistId,
        WorksFullResponse worksFullResponse
    )
    {
        //if there are 0 results, then it should be called 1 time
        worksFullResponse.WorkCount = 0;

        client.Setup(x => x.GetWorksByArtistId(artistId, 0, 100))
            .ReturnsAsync(worksFullResponse);
        var repo = new ArtistRepository(client.Object);
        var result = await repo.GetSongsByArtistId(artistId);
        
        client.Verify(x => 
            x.GetWorksByArtistId(artistId, 0, 100), 
            Times.Once
        );
        client.Verify(x => 
            x.GetWorksByArtistId(artistId, 100, 100), 
            Times.Never()
        );
    }

    [Test]
    [AutoData]
    public async Task GetSongsByArtistId_Returns_EmptyListIfNoResults(
        Mock<IMusicBrainzClient> client,
        string artistId,
        WorksFullResponse worksFullResponse
    )
    {
        worksFullResponse.WorkCount = 0;
        worksFullResponse.Works = new List<WorkResponse>();

        client.Setup(x => x.GetWorksByArtistId(artistId, 0, 100))
            .ReturnsAsync(worksFullResponse);
        var repo = new ArtistRepository(client.Object);
        var result = await repo.GetSongsByArtistId(artistId);
        
        result.Count().Should().Be(0);
    }

    [Test]
    [AutoData]
    public async Task GetSongsByArtistId_Returns_TheNamesOfTheSongs(
        Mock<IMusicBrainzClient> client,
        string artistId,
        WorkResponse work1,
        WorkResponse work2,
        WorkResponse work3
    )
    {
        work1.Type = "Song";
        work2.Type = "Song";
        work3.Type = "Song";
        var response = new WorksFullResponse {
            WorkCount = 3,
            Works = new List<WorkResponse> {
                work1,
                work2,
                work3
            },        
        };

        client.Setup(x => x.GetWorksByArtistId(artistId, 0, 100))
            .ReturnsAsync(response);
        var repo = new ArtistRepository(client.Object);
        var result = await repo.GetSongsByArtistId(artistId);
        
        result.Count().Should().Be(3);
        result.Contains(work1.Title);
        result.Contains(work2.Title);
        result.Contains(work3.Title);
    }
}