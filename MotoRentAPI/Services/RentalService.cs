using MotoRentAPI.Models;
using MotoRentAPI.Repositories;

namespace MotoRentAPI.Services
{
    public class RentalService
    {
        private readonly IRentalRepository _repo;
        private readonly IDeliveryDriverRepository _driverRepo;
        private readonly IMotorcycleRepository _motorcycleRepo;

        public RentalService(
            IRentalRepository repo,
            IDeliveryDriverRepository driverRepo,
            IMotorcycleRepository motorcycleRepo
        )
        {
            _repo = repo;
            _driverRepo = driverRepo;
            _motorcycleRepo = motorcycleRepo;
        }

        public async Task<Rental> CreateAsync(Rental rental)
        {
            var driver = await _driverRepo.GetByIdAsync(rental.DeliveryDriverId);
            if (driver == null)
                throw new ArgumentException("Entregador não encontrado");

            if (!driver.DriverLicenseType.Contains("A"))
                throw new ArgumentException("Entregador não possui categoria A");

            var motorcycle = await _motorcycleRepo.GetByIdAsync(rental.MotorcycleId);
            if (motorcycle == null)
                throw new ArgumentException("Moto não encontrada");

            decimal dailyRate = rental.PlanDays switch
            {
                Enums.PlanEnum.SevenDays => 30,
                Enums.PlanEnum.FifteenDays => 28,
                Enums.PlanEnum.ThirtyDays => 22,
                Enums.PlanEnum.FortyFiveDays => 20,
                Enums.PlanEnum.FiftyDays => 18,
                _ => throw new ArgumentException("Plano inválido"),
            };

            rental.DailyRate = dailyRate;
            rental.StartDate = rental.StartDate.Date.AddDays(1);

            await _repo.AddAsync(rental);
            return rental;
        }

        public async Task<Rental?> GetByIdAsync(Guid id)
        {
            return await _repo.GetByIdAsync(id);
        }

        public async Task<Rental> UpdateReturnDateAsync(Guid id, DateTimeOffset returnDate)
        {
            var rental = await _repo.GetByIdAsync(id);
            if (rental == null)
                throw new ArgumentException("Locação não encontrada");

            rental.ReturnDate = returnDate;

            int totalDays =
                (int)(returnDate.UtcDateTime.Date - rental.StartDate.UtcDateTime.Date).TotalDays
                + 1;
            int plannedDays = (int)rental.PlanDays;

            decimal total = 0;

            if (totalDays < plannedDays)
            {
                int missingDays = plannedDays - totalDays;
                decimal dailyValue = rental.DailyRate;
                decimal penaltyPercent = rental.PlanDays switch
                {
                    Enums.PlanEnum.SevenDays => 0.2m,
                    Enums.PlanEnum.FifteenDays => 0.4m,
                    _ => 0m,
                };
                total = (dailyValue * totalDays) + (dailyValue * missingDays * penaltyPercent);
            }
            else if (totalDays > plannedDays)
            {
                int extraDays = totalDays - plannedDays;
                total = (rental.DailyRate * plannedDays) + (50 * extraDays);
            }
            else
            {
                total = rental.DailyRate * plannedDays;
            }

            await _repo.UpdateAsync(rental);
            return rental;
        }
    }
}
