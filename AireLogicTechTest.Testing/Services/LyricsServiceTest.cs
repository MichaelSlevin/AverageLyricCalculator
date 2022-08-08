using AireLogicTechTest.Repositories;
using AireLogicTechTest.Services;

namespace AireLogicTechTest.Testing.Services;

public class LyricsServiceTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    [AutoData]
    public async Task GetAverageWordCountForArtistAndSongs_Successfully_returns_word_count_for_One_Song(
        Mock<ILyricsClient> client,
        Mock <ILyricsCountingService> countingService
    )
    {
        var fakeLyrics14Words = "Lyrics lyrics lyrics\r\nLine one of a song,\nthe next line. Of A song"; //14 words
        var artist = "Fake Artist";
        var fakesong1 = "fake song";
        var songTitles = new List<string> {
            fakesong1
        };

        client.Setup(x=> x.GetLyrics(artist, fakesong1)).ReturnsAsync(fakeLyrics14Words);
        countingService.Setup(x=> x.CleanAndCountLyrics(fakeLyrics14Words)).Returns(14);

        var service = new LyricsService(client.Object, countingService.Object);

        var result = await service.GetAverageWordCountForArtistAndSongs(artist, songTitles);

        result.AverageWordCount.Should().Be(14);
        result.NumberOfSongsNotFound.Should().Be(0);
        
    }

    [Test]
    [AutoData]
    public async Task GetAverageWordCountForArtistAndSongs_Successfully_returns_word_count_for_Multiple_songs(
        Mock<ILyricsClient> client,
        Mock <ILyricsCountingService> countingService
    )
    {
        var fakeLyrics14Words = "Lyrics lyrics lyrics\r\nLine one of a song,\nthe next line. Of A song"; //14 words
        var artist = "Fake Artist";
        var fakesong1 = "fake song";
        var songTitles = new List<string> {
            fakesong1
        };

        for (var i = 0; i < 400; i++)
        {
            songTitles.Add(fakesong1);
        }

        client.Setup(x=> x.GetLyrics(artist, fakesong1)).ReturnsAsync(fakeLyrics14Words);
        countingService.Setup(x=> x.CleanAndCountLyrics(fakeLyrics14Words)).Returns(14);

        var service = new LyricsService(client.Object, countingService.Object);

        var result = await service.GetAverageWordCountForArtistAndSongs(artist, songTitles);

        result.AverageWordCount.Should().Be(14);
        result.NumberOfSongsNotFound.Should().Be(0);        
    }


    [Test]
    [AutoData]
    public async Task GetAverageWordCountForArtistAndSongs_Successfully_returns_word_count_for_Multiple_songs_With_different_lengths(
        Mock<ILyricsClient> client,
        Mock <ILyricsCountingService> countingService
    )
    {
        var fakeLyrics11Words = "Lyrics lyrics lyrics\r\nLine one of a song,\nthe next line"; //11 words
        var fakeLyrics9Words = "Lyrics lyrics lyrics\r\nLine one song,\nthe next line"; //9 words
        var fakeLyrics7Words = "Lyrics lyrics\r\none of a song,\nthe"; //7 words
        var artist = "Fake Artist";
        var fakesong1 = "fake song1";
        var fakesong2 = "fake song2";
        var fakesong3 = "fake song3";
        var songTitles = new List<string> {
            fakesong1,
            fakesong2,
            fakesong3
        };

        client.Setup(x=> x.GetLyrics(artist, fakesong1)).ReturnsAsync(fakeLyrics11Words);
        client.Setup(x=> x.GetLyrics(artist, fakesong2)).ReturnsAsync(fakeLyrics9Words);
        client.Setup(x=> x.GetLyrics(artist, fakesong3)).ReturnsAsync(fakeLyrics7Words);
        countingService.Setup(x=> x.CleanAndCountLyrics(fakeLyrics11Words)).Returns(11);
        countingService.Setup(x=> x.CleanAndCountLyrics(fakeLyrics9Words)).Returns(9);
        countingService.Setup(x=> x.CleanAndCountLyrics(fakeLyrics7Words)).Returns(7);

        var service = new LyricsService(client.Object, countingService.Object);

        var result = await service.GetAverageWordCountForArtistAndSongs(artist, songTitles);

        result.AverageWordCount.Should().Be(9);
        result.NumberOfSongsNotFound.Should().Be(0);
        
    }

     [Test]
    [AutoData]
    public async Task GetAverageWordCountForArtistAndSongs_Successfully_returns_word_count_And_Number_Of_songs_not_found(
        Mock<ILyricsClient> client,
        Mock <ILyricsCountingService> countingService
    )
    {
        var fakeLyrics10Words = "lyrics lyrics\r\nLine one of a song,\nthe next line"; //10 words
        var fakeLyrics9Words = "Lyrics lyrics lyrics\r\nLine one song,\nthe next line"; //9 words

        var artist = "Fake Artist";
        var fakesong1 = "fake song1";
        var fakesong2 = "fake song2";
        var fakesong3 = "fake song3";
        var songTitles = new List<string> {
            fakesong1,
            fakesong2,
            fakesong3
        };

        client.Setup(x=> x.GetLyrics(artist, fakesong1)).ReturnsAsync(fakeLyrics10Words);
        client.Setup(x=> x.GetLyrics(artist, fakesong2)).ReturnsAsync(fakeLyrics9Words);
        client.Setup(x=> x.GetLyrics(artist, fakesong3)).ThrowsAsync(new SongNotFoundException());
        countingService.Setup(x=> x.CleanAndCountLyrics(fakeLyrics10Words)).Returns(10);
        countingService.Setup(x=> x.CleanAndCountLyrics(fakeLyrics9Words)).Returns(9);

        var service = new LyricsService(client.Object, countingService.Object);

        var result = await service.GetAverageWordCountForArtistAndSongs(artist, songTitles);

        result.AverageWordCount.Should().Be(9.5);
        result.NumberOfSongsNotFound.Should().Be(1);
        
    }
}