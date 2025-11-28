using MotoRentAPI.Models;

namespace MotoRentAPI.Repositories
{
    public interface IRentalRepository
    {
        Task<Rental> AddAsync(Rental rental);
        Task<Rental?> GetByIdAsync(Guid id);
        Task UpdateAsync(Rental rental);
    }
}
