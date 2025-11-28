using System.ComponentModel.DataAnnotations.Schema;

namespace MotoRentAPI.Models
{
    public class DeliveryDriver
    {
        public required string Id { get; set; }
        public required string Name { get; set; }
        public required string CNPJ { get; set; }

        [Column(TypeName = "timestamp with time zone")]
        public DateTimeOffset BirthDate { get; set; }

        public required string DriverLicenseNumber { get; set; }
        public required string DriverLicenseType { get; set; }
        public string? DriverLicenseImagePath { get; set; }

        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    }
}
