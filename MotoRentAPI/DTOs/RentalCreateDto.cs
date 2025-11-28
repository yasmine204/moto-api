using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MotoRentAPI.Common.Attributes;
using MotoRentAPI.Enums;

namespace MotoRentAPI.Dtos
{
    public class RentalCreateDto
    {
        [Required]
        [NotEmptyOrWhiteSpace]
        [JsonPropertyName("entregador_id")]
        public required string DeliveryDriverId { get; set; }

        [Required]
        [NotEmptyOrWhiteSpace]
        [JsonPropertyName("moto_id")]
        public required string MotorcycleId { get; set; }

        [Required]
        [JsonPropertyName("data_inicio")]
        public DateTimeOffset StartDate { get; set; }

        [Required]
        [JsonPropertyName("data_termino")]
        public DateTimeOffset EndDate { get; set; }

        [Required]
        [JsonPropertyName("data_previsao_termino")]
        public DateTimeOffset PredictedEndDate { get; set; }

        [Required]
        [JsonPropertyName("plano")]
        public PlanEnum PlanDays { get; set; }
    }
}
