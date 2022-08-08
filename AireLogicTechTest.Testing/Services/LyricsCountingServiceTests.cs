using AireLogicTechTest.Repositories;
using AireLogicTechTest.Services;

namespace AireLogicTechTest.Testing.Services;

public class LyricsCountingServiceTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public async Task CleanAndCountLyrics_returns_The_correct_number_of_lyrics()
    {
        var fakeLyricsWithSpecialChars = "Lyrics lyrics lyrics\r\nLine one of a song,\nthe next line. Of A song"; //14 words

        var service = new LyricsCountingService();

        var result = service.CleanAndCountLyrics(fakeLyricsWithSpecialChars);

        result.Should().Be(14);
        
    }
}