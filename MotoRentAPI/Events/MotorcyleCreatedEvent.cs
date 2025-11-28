namespace MotoRentAPI.Events
{
    public class MotorcycleCreatedEvent
    {
        public string Id { get; set; } = default!;
        public string Model { get; set; } = default!;
        public string Plate { get; set; } = default!;
        public int Year { get; set; }
    }
}
