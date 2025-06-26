using AmadeusFlightApý.Dtos;
using AmadeusFlightApý.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace AmadeusFlightApý.Services
{
    public class FlightImportBackgroundService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _interval = TimeSpan.FromHours(24); // Her gün çalýþacak
        // Test için: TimeSpan.FromMinutes(1)

        public FlightImportBackgroundService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await ImportFlightsAsync();
                await Task.Delay(_interval, stoppingToken);
            }
        }

        private async Task ImportFlightsAsync()
        {
            using var scope = _serviceProvider.CreateScope();
            var flightService = scope.ServiceProvider.GetRequiredService<IFlightService>();
            var airportService = scope.ServiceProvider.GetRequiredService<IAirportService>();
            var airports = (await airportService.GetAllAsync()).ToList();
            if (airports.Count < 2) return; // En az iki havaalaný olmalý

            var random = new Random();
            var depIndex = random.Next(airports.Count);
            var arrIndex = (depIndex + 1) % airports.Count;
            var depAirport = airports[depIndex];
            var arrAirport = airports[arrIndex];

            var flightDto = new CreateFlightDto
            {
                DepartureAirportId = depAirport.Id,
                ArrivalAirportId = arrAirport.Id,
                DepartureDateTime = DateTime.UtcNow.AddDays(random.Next(1, 30)),
                ReturnDateTime = null,
                Price = random.Next(1000, 5000)
            };
            await flightService.CreateAsync(flightDto);
        }
    }
}
