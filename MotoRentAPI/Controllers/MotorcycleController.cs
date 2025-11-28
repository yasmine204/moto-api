using Microsoft.AspNetCore.Mvc;
using MotoRentAPI.Dtos;
using MotoRentAPI.Models;
using MotoRentAPI.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace MotoRentAPI.Controllers
{
    [ApiController]
    [Route("/motos")]
    [Tags("motos")]
    public class MotorcycleController : ControllerBase
    {
        private readonly MotorcycleService _service;

        public MotorcycleController(MotorcycleService service)
        {
            _service = service;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Cadastrar uma nova moto")]
        [SwaggerResponse(201, "Moto criada")]
        [SwaggerResponse(400, "Dados inválidos", typeof(MessageResponseDto))]
        public async Task<IActionResult> Create([FromBody] MotorcycleCreateDto dto)
        {
            try
            {
                var motorcycle = new Motorcycle
                {
                    Id = dto.Id,
                    Year = dto.Year,
                    Model = dto.Model,
                    Plate = dto.Plate,
                };

                await _service.CreateAsync(motorcycle);
                return StatusCode(201);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new MessageResponseDto { Message = ex.Message });
            }
        }

        [HttpGet]
        [SwaggerOperation(Summary = "Consultar motos existentes")]
        [SwaggerResponse(200, "Lista de motos", typeof(IEnumerable<MotorcycleDto>))]
        public async Task<IActionResult> GetAll([FromQuery] string? plate)
        {
            var list = await _service.GetAllAsync(plate);
            var dtoList = list.Select(m => new MotorcycleDto
            {
                Id = m.Id,
                Year = m.Year,
                Model = m.Model,
                Plate = m.Plate,
            });

            return Ok(dtoList);
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Consultar motos existentes por id")]
        [SwaggerResponse(200, "Detalhes da moto", typeof(MotorcycleDto))]
        [SwaggerResponse(404, "Moto não encontrada", typeof(MessageResponseDto))]
        public async Task<IActionResult> GetById(string id)
        {
            try
            {
                var motorcycle = await _service.GetByIdAsync(id);

                return Ok(
                    new MotorcycleDto
                    {
                        Id = motorcycle.Id,
                        Year = motorcycle.Year,
                        Model = motorcycle.Model,
                        Plate = motorcycle.Plate,
                    }
                );
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new MessageResponseDto { Message = ex.Message });
            }
        }

        [HttpPut("{id}/placa")]
        [SwaggerOperation(Summary = "Modificar a placa de uma moto")]
        [SwaggerResponse(200, "Placa modificada com sucesso", typeof(MessageResponseDto))]
        [SwaggerResponse(400, "Dados inválidos", typeof(MessageResponseDto))]
        [SwaggerResponse(404, "Moto não encontrada", typeof(MessageResponseDto))]
        public async Task<IActionResult> UpdatePlate(string id, [FromBody] MotorcycleUpdateDto dto)
        {
            try
            {
                await _service.UpdatePlateAsync(id, dto.Plate);
                return Ok(new MessageResponseDto { Message = "Placa modificada com sucesso" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new MessageResponseDto { Message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new MessageResponseDto { Message = ex.Message });
            }
        }

        [HttpDelete("{id}")]
        [SwaggerOperation(Summary = "Remover uma moto")]
        [SwaggerResponse(200, "Moto deletada com sucesso", typeof(MessageResponseDto))]
        [SwaggerResponse(404, "Moto não encontrada", typeof(MessageResponseDto))]
        public async Task<IActionResult> Delete(string id)
        {
            try
            {
                await _service.DeleteAsync(id);
                return Ok(new MessageResponseDto { Message = "Moto deletada com sucesso" });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new MessageResponseDto { Message = ex.Message });
            }
        }
    }
}
