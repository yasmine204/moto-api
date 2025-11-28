using System.ComponentModel.DataAnnotations.Schema;
using MotoRentAPI.Enums;

namespace MotoRentAPI.Models
{
    public class Rental
    {
        public Guid Id { get; set; }

        public required string DeliveryDriverId { get; set; }
        public DeliveryDriver DeliveryDriver { get; set; } = null!;

        public required string MotorcycleId { get; set; }
        public Motorcycle Motorcycle { get; set; } = null!;

        [Column(TypeName = "timestamp with time zone")]
        public DateTimeOffset StartDate { get; set; }

        [Column(TypeName = "timestamp with time zone")]
        public DateTimeOffset EndDate { get; set; }

        [Column(TypeName = "timestamp with time zone")]
        public DateTimeOffset PredictedEndDate { get; set; }

        [Column(TypeName = "timestamp with time zone")]
        public DateTimeOffset ReturnDate { get; set; }

        public PlanEnum PlanDays { get; set; }
        public decimal DailyRate { get; set; }
    }
}
