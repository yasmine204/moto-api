using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MotoRentAPI.Common.Attributes;

namespace MotoRentAPI.Dtos
{
    public class DriverLicenseUploadImageDto
    {
        [Required]
        [NotEmptyOrWhiteSpace]
        [JsonPropertyName("imagem_cnh")]
        public required string DriverLicenseImageBase64 { get; set; }
    }
}
