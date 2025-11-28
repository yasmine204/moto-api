using Microsoft.EntityFrameworkCore;
using MotoRentAPI.Data;
using MotoRentAPI.Models;

namespace MotoRentAPI.Repositories
{
    public class RentalRepository : IRentalRepository
    {
        private readonly AppDbContext _context;

        public RentalRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Rental> AddAsync(Rental rental)
        {
            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();
            return rental;
        }

        public Task<Rental?> GetByIdAsync(Guid id)
        {
            return _context
                .Rentals.Include(r => r.Motorcycle)
                .Include(r => r.DeliveryDriver)
                .FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task UpdateAsync(Rental rental)
        {
            _context.Rentals.Update(rental);
            await _context.SaveChangesAsync();
        }
    }
}
