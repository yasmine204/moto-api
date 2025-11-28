using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using MotoRentAPI.Common.Attributes;

namespace MotoRentAPI.Dtos
{
    public class UpdateReturnDateDto
    {
        [Required]
        [NotEmptyOrWhiteSpace]
        [JsonPropertyName("data_devolucao")]
        public DateTimeOffset ReturnDate { get; set; }
    }
}
