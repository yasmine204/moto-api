using Microsoft.EntityFrameworkCore;
using MotoRentAPI.Data;
using MotoRentAPI.Models;

namespace MotoRentAPI.Repositories
{
    public class MotorcycleRepository : IMotorcycleRepository
    {
        private readonly AppDbContext _context;

        public MotorcycleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Motorcycle> AddAsync(Motorcycle motorcycle)
        {
            _context.Motorcycles.Add(motorcycle);
            await _context.SaveChangesAsync();
            return motorcycle;
        }

        public Task<Motorcycle?> GetByPlateAsync(string plate)
        {
            return _context.Motorcycles.FirstOrDefaultAsync(x => x.Plate == plate);
        }

        public Task<List<Motorcycle>> GetAllAsync(string? plate = null)
        {
            return _context
                .Motorcycles.Where(x => plate == null || x.Plate.Contains(plate))
                .ToListAsync();
        }

        public Task<Motorcycle?> GetByIdAsync(string id)
        {
            return _context.Motorcycles.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UpdateAsync(Motorcycle motorcycle)
        {
            _context.Motorcycles.Update(motorcycle);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Motorcycle motorcycle)
        {
            _context.Motorcycles.Remove(motorcycle);
            await _context.SaveChangesAsync();
        }
    }
}
