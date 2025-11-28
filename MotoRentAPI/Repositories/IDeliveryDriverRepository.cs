using MotoRentAPI.Models;

namespace MotoRentAPI.Repositories
{
    public interface IDeliveryDriverRepository
    {
        Task<DeliveryDriver> AddAsync(DeliveryDriver driver);
        Task<DeliveryDriver?> GetByCnpjAsync(string cnpj);
        Task<DeliveryDriver?> GetByDriverLicenseNumberAsync(string driverLicenseNumber);
        Task<DeliveryDriver?> GetByIdAsync(string id);
        Task UploadAsync(DeliveryDriver driver);
    }
}
