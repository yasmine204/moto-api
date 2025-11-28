using Microsoft.AspNetCore.Mvc;
using MotoRentAPI.Dtos;
using MotoRentAPI.Models;
using MotoRentAPI.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace MotoRentAPI.Controllers
{
    [ApiController]
    [Route("/entregadores")]
    [Tags("entregadores")]
    public class DeliveryDriversController : ControllerBase
    {
        private readonly DeliveryDriverService _service;

        public DeliveryDriversController(DeliveryDriverService service)
        {
            _service = service;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cadastrar entregador")]
        [SwaggerResponse(201, "Entregador criado")]
        [SwaggerResponse(400, "Dados inválidos", typeof(MessageResponseDto))]
        public async Task<IActionResult> Create([FromBody] DeliveryDriverCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(new MessageResponseDto { Message = "Dados inválidos" });

            try
            {
                var driver = new DeliveryDriver
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    CNPJ = dto.CNPJ,
                    BirthDate = dto.BirthDate.ToUniversalTime(),
                    DriverLicenseNumber = dto.DriverLicenseNumber,
                    DriverLicenseType = dto.DriverLicenseType,
                };

                await _service.CreateAsync(driver, dto.DriverLicenseImageBase64);
                return StatusCode(201);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new MessageResponseDto { Message = ex.Message });
            }
        }

        [HttpPost("{id}/cnh")]
        [SwaggerOperation(Summary = "Enviar foto da CNH")]
        [SwaggerResponse(201, "Imagem da CNH enviada com sucesso")]
        [SwaggerResponse(400, "Dados inválidos", typeof(MessageResponseDto))]
        [SwaggerResponse(404, "Entregador não encontrado", typeof(MessageResponseDto))]
        public async Task<IActionResult> UploadDriverLicenseImage(
            string id,
            [FromBody] DriverLicenseUploadImageDto dto
        )
        {
            if (!ModelState.IsValid)
                return BadRequest(new MessageResponseDto { Message = "Dados inválidos" });

            try
            {
                await _service.UploadDriverLicenseImageAsync(id, dto.DriverLicenseImageBase64);
                return StatusCode(201);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new MessageResponseDto { Message = ex.Message });
            }
        }
    }
}
