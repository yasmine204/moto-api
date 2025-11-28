using MotoRentAPI.Messaging;
using MotoRentAPI.Models;
using MotoRentAPI.Repositories;

namespace MotoRentAPI.Services
{
    public class MotorcycleService
    {
        private readonly IMotorcycleRepository _repo;
        private readonly IMessagePublisher _publisher;

        public MotorcycleService(IMotorcycleRepository repo, IMessagePublisher publisher)
        {
            _repo = repo;
            _publisher = publisher;
        }

        public async Task<Motorcycle> CreateAsync(Motorcycle motorcycle)
        {
            var exists = await _repo.GetByPlateAsync(motorcycle.Plate);
            if (exists != null)
                throw new ArgumentException("Placa já registrada");

            await _repo.AddAsync(motorcycle);
            return motorcycle;
        }

        public Task<List<Motorcycle>> GetAllAsync(string? plate) => _repo.GetAllAsync(plate);

        public async Task<Motorcycle> GetByIdAsync(string id)
        {
            var motorcycle = await _repo.GetByIdAsync(id);
            if (motorcycle == null)
                throw new KeyNotFoundException("Moto não encontrada");

            return motorcycle;
        }

        public async Task<Motorcycle> UpdatePlateAsync(string id, string newPlate)
        {
            var motorcycle = await _repo.GetByIdAsync(id);
            if (motorcycle == null)
                throw new KeyNotFoundException("Moto não encontrada");

            var plateExists = await _repo.GetByPlateAsync(newPlate);
            if (plateExists != null && plateExists.Id != id)
                throw new ArgumentException("Placa já registrada");

            motorcycle.Plate = newPlate;
            await _repo.UpdateAsync(motorcycle);

            return motorcycle;
        }

        public async Task DeleteAsync(string id)
        {
            var motorcycle = await _repo.GetByIdAsync(id);
            if (motorcycle == null)
                throw new KeyNotFoundException("Moto não encontrada");

            await _repo.DeleteAsync(motorcycle);
        }
    }
}
