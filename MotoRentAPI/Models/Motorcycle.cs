namespace MotoRentAPI.Models
{
    public class Motorcycle
    {
        public required string Id { get; set; }
        public required int Year { get; set; }
        public required string Model { get; set; }
        public required string Plate { get; set; }

        public ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    }
}
