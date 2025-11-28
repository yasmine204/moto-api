using System.Text.Json.Serialization;

namespace MotoRentAPI.Dtos
{
    public class MotorcycleDto
    {
        [JsonPropertyName("identificador")]
        public string Id { get; set; } = default!;

        [JsonPropertyName("ano")]
        public int Year { get; set; }

        [JsonPropertyName("modelo")]
        public string Model { get; set; } = default!;

        [JsonPropertyName("placa")]
        public string Plate { get; set; } = default!;
    }
}
