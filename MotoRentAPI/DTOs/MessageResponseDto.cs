using System.Text.Json.Serialization;

namespace MotoRentAPI.Dtos
{
    public class MessageResponseDto
    {
        [JsonPropertyName("mensagem")]
        public string Message { get; set; } = "";
    }
}
