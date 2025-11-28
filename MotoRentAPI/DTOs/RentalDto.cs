using System.Text.Json.Serialization;

namespace MotoRentAPI.Dtos
{
    public class RentalDto
    {
        [JsonPropertyName("identificador")]
        public Guid Id { get; set; }

        [JsonPropertyName("valor_diaria")]
        public decimal DailyRate { get; set; }

        [JsonPropertyName("entregador_id")]
        public string DeliveryDriverId { get; set; } = null!;

        [JsonPropertyName("moto_id")]
        public string MotorcycleId { get; set; } = null!;

        [JsonPropertyName("data_inicio")]
        public DateTimeOffset StartDate { get; set; }

        [JsonPropertyName("data_termino")]
        public DateTimeOffset EndDate { get; set; }

        [JsonPropertyName("data_previsao_termino")]
        public DateTimeOffset PredictedEndDate { get; set; }

        [JsonPropertyName("data_devolucao")]
        public DateTimeOffset? ReturnDate { get; set; }
    }
}
