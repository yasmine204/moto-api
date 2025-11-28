using Microsoft.EntityFrameworkCore;
using MotoRentAPI.Data;
using MotoRentAPI.Events;
using MotoRentAPI.Models;

namespace MotoRentAPI.Messaging
{
    public class Motorcycle2024Consumer : BackgroundService
    {
        private readonly IMessageSubscriber _subscriber;
        private readonly IServiceProvider _serviceProvider;

        public Motorcycle2024Consumer(
            IMessageSubscriber subscriber,
            IServiceProvider serviceProvider
        )
        {
            _subscriber = subscriber;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            Console.WriteLine("Motorcycle2024Consumer iniciado!");

            _subscriber.Subscribe<MotorcycleCreatedEvent>(async motorcycleEvent =>
            {
                using var scope = _serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                if (motorcycleEvent.Year == 2024)
                {
                    var exists = await db.Motorcycles.AnyAsync(
                        m => m.Id == motorcycleEvent.Id,
                        stoppingToken
                    );

                    if (!exists)
                    {
                        db.Motorcycles.Add(
                            new Motorcycle
                            {
                                Id = motorcycleEvent.Id,
                                Model = motorcycleEvent.Model,
                                Plate = motorcycleEvent.Plate,
                                Year = motorcycleEvent.Year,
                            }
                        );

                        await db.SaveChangesAsync(stoppingToken);
                        Console.WriteLine($"Moto {motorcycleEvent.Model} salva no banco.");
                    }
                }
                else
                {
                    Console.WriteLine("Moto ignorada, ano diferente de 2024.");
                }
            });

            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
