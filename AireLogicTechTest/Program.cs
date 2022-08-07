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
            var artists = await host.Services.GetRequiredService<IArtistService>().GetSongsByArtist("The Beatles");
        }
            
        
        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) => {
                    services.AddTransient<IArtistService, ArtistService>()
                        .AddTransient<IArtistRepository, ArtistRepository>()
                        .AddTransient<IMusicBrainzClient, MusicBrainzClient>()
                        .AddHttpClient();
                    });
    }
}