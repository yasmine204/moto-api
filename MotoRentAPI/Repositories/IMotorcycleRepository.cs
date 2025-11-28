using MotoRentAPI.Models;

namespace MotoRentAPI.Repositories
{
    public interface IMotorcycleRepository
    {
        Task<Motorcycle> AddAsync(Motorcycle motorcycle);
        Task<Motorcycle?> GetByPlateAsync(string plate);
        Task<List<Motorcycle>> GetAllAsync(string? plate = null);
        Task<Motorcycle?> GetByIdAsync(string id);
        Task UpdateAsync(Motorcycle motorcycle);
        Task DeleteAsync(Motorcycle motorcycle);
    }
}
