using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MotoRentAPI.Common.Attributes;

namespace MotoRentAPI.Dtos
{
    public class MotorcycleUpdateDto
    {
        [Required]
        [NotEmptyOrWhiteSpace]
        [JsonPropertyName("placa")]
        public required string Plate { get; set; }
    }
}
