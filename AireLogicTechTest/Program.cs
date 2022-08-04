using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AireLogicTechTest.Services;

namespace AireLogicTechTest
{
    class Program
    {

        static void Main(string[] args) {
            var host = CreateHostBuilder(args).Build();
        }
            
        
        static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddTransient<IArtistService, ArtistService>()
                );
    }
}