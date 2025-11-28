using Microsoft.EntityFrameworkCore;
using MotoRentAPI.Data;
using MotoRentAPI.Models;

namespace MotoRentAPI.Repositories
{
    public class DeliveryDriverRepository : IDeliveryDriverRepository
    {
        private readonly AppDbContext _context;

        public DeliveryDriverRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<DeliveryDriver> AddAsync(DeliveryDriver driver)
        {
            _context.DeliveryDrivers.Add(driver);
            await _context.SaveChangesAsync();
            return driver;
        }

        public Task<DeliveryDriver?> GetByCnpjAsync(string cnpj)
        {
            return _context.DeliveryDrivers.FirstOrDefaultAsync(x => x.CNPJ == cnpj);
        }

        public Task<DeliveryDriver?> GetByDriverLicenseNumberAsync(string driverLicenseNumber)
        {
            return _context.DeliveryDrivers.FirstOrDefaultAsync(x =>
                x.DriverLicenseNumber == driverLicenseNumber
            );
        }

        public Task<DeliveryDriver?> GetByIdAsync(string id)
        {
            return _context.DeliveryDrivers.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task UploadAsync(DeliveryDriver driver)
        {
            _context.DeliveryDrivers.Update(driver);
            await _context.SaveChangesAsync();
        }
    }
}
