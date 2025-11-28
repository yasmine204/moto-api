using MotoRentAPI.Enums;
using MotoRentAPI.Models;

namespace MotoRentAPI.Data
{
    public static class SeedData
    {
        public static void SeedDatabase(AppDbContext context)
        {
            if (!context.Motorcycles.Any())
            {
                context.Motorcycles.AddRange(
                    new Motorcycle
                    {
                        Id = "moto456",
                        Year = 2024,
                        Model = "Honda CG 160",
                        Plate = "YTQ-1723",
                    },
                    new Motorcycle
                    {
                        Id = "moto789",
                        Year = 2024,
                        Model = "Yamaha Factor 150",
                        Plate = "DEF-4956",
                    }
                );
            }

            if (!context.DeliveryDrivers.Any())
            {
                context.DeliveryDrivers.AddRange(
                    new DeliveryDriver
                    {
                        Id = "entregador456",
                        Name = "Maria Oliveira Santos",
                        CNPJ = "98765432109876",
                        BirthDate = DateTimeOffset.Parse("1985-05-15T00:00:00Z"),
                        DriverLicenseNumber = "09876543211",
                        DriverLicenseType = "A",
                    },
                    new DeliveryDriver
                    {
                        Id = "entregador789",
                        Name = "Carlos Roberto Souza",
                        CNPJ = "45678901234567",
                        BirthDate = DateTimeOffset.Parse("1975-11-20T00:00:00Z"),
                        DriverLicenseNumber = "54321098765",
                        DriverLicenseType = "A+B",
                    }
                );
            }

            context.SaveChanges();

            if (!context.Rentals.Any())
            {
                context.Rentals.AddRange(
                    new Rental
                    {
                        Id = Guid.NewGuid(),
                        MotorcycleId = "moto456",
                        DeliveryDriverId = "entregador456",
                        StartDate = DateTimeOffset.Parse("2024-01-01T00:00:00Z"),
                        EndDate = DateTimeOffset.Parse("2024-01-07T23:59:59Z"),
                        PredictedEndDate = DateTimeOffset.Parse("2024-01-07T23:59:59Z"),
                        ReturnDate = DateTimeOffset.Parse("2024-01-07T18:00:00Z"),
                        PlanDays = PlanEnum.SevenDays,
                        DailyRate = 30,
                    },
                    new Rental
                    {
                        Id = Guid.NewGuid(),
                        MotorcycleId = "moto789",
                        DeliveryDriverId = "entregador789",
                        StartDate = DateTimeOffset.Parse("2024-02-01T00:00:00Z"),
                        EndDate = DateTimeOffset.Parse("2024-02-15T23:59:59Z"),
                        PredictedEndDate = DateTimeOffset.Parse("2024-02-15T23:59:59Z"),
                        ReturnDate = DateTimeOffset.Parse("2024-02-15T20:00:00Z"),
                        PlanDays = PlanEnum.FifteenDays,
                        DailyRate = 28,
                    }
                );

                context.SaveChanges();
            }
        }
    }
}
