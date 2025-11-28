using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MotoRentAPI.Common.Attributes;

namespace MotoRentAPI.Dtos
{
    public class MotorcycleCreateDto
    {
        [Required]
        [NotEmptyOrWhiteSpace]
        [JsonPropertyName("identificador")]
        public required string Id { get; set; }

        [Required]
        [JsonPropertyName("ano")]
        public int Year { get; set; }

        [Required]
        [NotEmptyOrWhiteSpace]
        [JsonPropertyName("modelo")]
        public required string Model { get; set; }

        [Required]
        [NotEmptyOrWhiteSpace]
        [JsonPropertyName("placa")]
        public required string Plate { get; set; }
    }
}
