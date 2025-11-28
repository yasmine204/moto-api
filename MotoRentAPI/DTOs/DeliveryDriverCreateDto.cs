using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MotoRentAPI.Common.Attributes;

namespace MotoRentAPI.Dtos
{
    public class DeliveryDriverCreateDto
    {
        [Required]
        [NotEmptyOrWhiteSpace]
        [JsonPropertyName("identificador")]
        public required string Id { get; set; }

        [Required]
        [NotEmptyOrWhiteSpace]
        [JsonPropertyName("nome")]
        public required string Name { get; set; }

        [Required]
        [NotEmptyOrWhiteSpace]
        public required string CNPJ { get; set; }

        [Required]
        [JsonPropertyName("data_nascimento")]
        public DateTimeOffset BirthDate { get; set; }

        [Required]
        [NotEmptyOrWhiteSpace]
        [JsonPropertyName("numero_cnh")]
        public required string DriverLicenseNumber { get; set; }

        [Required, MinLength(1)]
        [JsonPropertyName("tipo_cnh")]
        public required string DriverLicenseType { get; set; }

        [JsonPropertyName("imagem_cnh")]
        public string? DriverLicenseImageBase64 { get; set; }
    }
}
