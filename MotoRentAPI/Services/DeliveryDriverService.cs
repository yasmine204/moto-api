using MotoRentAPI.Models;
using MotoRentAPI.Repositories;

namespace MotoRentAPI.Services
{
    public class DeliveryDriverService
    {
        private readonly IDeliveryDriverRepository _repo;
        private readonly ImageService _imageService;

        public DeliveryDriverService(IDeliveryDriverRepository repo, ImageService imageService)
        {
            _repo = repo;
            _imageService = imageService;
        }

        public async Task<DeliveryDriver> CreateAsync(
            DeliveryDriver driver,
            string? driverLicenseBase64
        )
        {
            if (!new[] { "A", "B", "A+B" }.Contains(driver.DriverLicenseType))
                throw new ArgumentException("Tipo de CNH inválido");

            var existsCNPJ = await _repo.GetByCnpjAsync(driver.CNPJ);
            if (existsCNPJ != null)
                throw new ArgumentException("CNPJ já registrado");

            var existsCNH = await _repo.GetByDriverLicenseNumberAsync(driver.DriverLicenseNumber);
            if (existsCNH != null)
                throw new ArgumentException("Número da CNH já registrado");

            if (!string.IsNullOrEmpty(driverLicenseBase64))
            {
                driver.DriverLicenseImagePath = _imageService.SaveDriverLicense(
                    driverLicenseBase64,
                    driver.Id
                );
            }

            return await _repo.AddAsync(driver);
        }

        public async Task<DeliveryDriver> UploadDriverLicenseImageAsync(
            string id,
            string driverLicenseBase64
        )
        {
            var driver = await _repo.GetByIdAsync(id);
            if (driver == null)
                throw new KeyNotFoundException("Entregador não encontrado");

            driver.DriverLicenseImagePath = _imageService.SaveDriverLicense(
                driverLicenseBase64,
                id
            );
            await _repo.UploadAsync(driver);

            return driver;
        }
    }
}
