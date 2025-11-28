using System.Text.Json.Serialization;

namespace MotoRentAPI.Dtos
{
    public class DeliveryDriverDto
    {
        [JsonPropertyName("identificador")]
        public string Id { get; set; } = default!;

        [JsonPropertyName("nome")]
        public string Name { get; set; } = default!;

        public string CNPJ { get; set; } = default!;

        [JsonPropertyName("data_nascimento")]
        public DateTimeOffset BirthDate { get; set; }

        [JsonPropertyName("numero_cnh")]
        public string DriverLicenseNumber { get; set; } = default!;

        [JsonPropertyName("tipo_cnh")]
        public string DriverLicenseType { get; set; } = default!;

        [JsonPropertyName("imagem_cnh")]
        public string? DriverLicenseImagePath { get; set; }
    }
}
