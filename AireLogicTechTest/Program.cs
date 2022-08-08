using System;
using System.Net.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AireLogicTechTest.Services;
using AireLogicTechTest.Repositories;

namespace AireLogicTechTest
{
    class Program
    {

        static async Task Main(string[] args) {
            var host = CreateHostBuilder(args).Build();
            var artist = "";
            while(string.IsNullOrEmpty(artist)) 
            {
                Console.WriteLine("Please enter a name of an artist you want to find the average word count per song for");
                artist = Console.ReadLine();
            }
            Console.WriteLine($"Searching for songs by {artist}");
            var songs = await host.Services.GetRequiredService<IArtistService>().GetSongsByArtist(artist);

            var numberOfSongs = songs.Count();
            Console.WriteLine($"{numberOfSongs} songs found");

            Console.WriteLine("Searching for lyrics and calculating average length");
            var lyricsService = host.Services.GetRequiredService<ILyricsService>();
            var averageWordCountResponse = await lyricsService.GetAverageWordCountForArtistAndSongs(artist, songs);

            var numberOfSongsFound = numberOfSongs - averageWordCountResponse.NumberOfSongsNotFound;
            Console.WriteLine($"Found lyrics for {numberOfSongsFound} of {numberOfSongs} songs");

            Console.WriteLine(numberOfSongsFound == 0 ? "Cannot provide an average" : $"The average number of words per song is {averageWordCountResponse.AverageWordCount}");
        }
 
        static IHostBuilder CreateHostBuilder(string[] args) => 
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) => {
                    services.AddTransient<IArtistService, ArtistService>()
                        .AddTransient<IArtistRepository, ArtistRepository>()
                        .AddTransient<ILyricsService, LyricsService>()
                        .AddTransient<ILyricsClient, LyricsClient>()
                        .AddTransient<ILyricsCountingService, LyricsCountingService>()
                        .AddTransient<IMusicBrainzClient, MusicBrainzClient>()
                        .AddHttpClient();
                    });
    }
}