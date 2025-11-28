using Microsoft.AspNetCore.Mvc;
using MotoRentAPI.Dtos;
using MotoRentAPI.Models;
using MotoRentAPI.Services;
using Swashbuckle.AspNetCore.Annotations;

namespace MotoRentAPI.Controllers
{
    [ApiController]
    [Route("/locacao")]
    [Tags("locação")]
    public class RentalController : ControllerBase
    {
        private readonly RentalService _service;

        public RentalController(RentalService service)
        {
            _service = service;
        }

        [HttpPost]
        [SwaggerOperation(Summary = "Alugar uma moto")]
        [SwaggerResponse(201, "Locação criada")]
        [SwaggerResponse(400, "Dados inválidos", typeof(MessageResponseDto))]
        public async Task<IActionResult> Create([FromBody] RentalCreateDto dto)
        {
            try
            {
                var rental = new Rental
                {
                    DeliveryDriverId = dto.DeliveryDriverId,
                    MotorcycleId = dto.MotorcycleId,
                    StartDate = dto.StartDate.ToUniversalTime(),
                    EndDate = dto.EndDate.ToUniversalTime(),
                    PredictedEndDate = dto.PredictedEndDate.ToUniversalTime(),
                    PlanDays = dto.PlanDays,
                };

                await _service.CreateAsync(rental);
                return StatusCode(201);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new MessageResponseDto { Message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        [SwaggerOperation(Summary = "Consultar locação por id")]
        [SwaggerResponse(200, "Detalhes da locação", typeof(RentalDto))]
        [SwaggerResponse(400, "Dados inválidos", typeof(MessageResponseDto))]
        [SwaggerResponse(404, "Locação não encontrada", typeof(MessageResponseDto))]
        public async Task<IActionResult> GetById(Guid id)
        {
            try
            {
                var rental = await _service.GetByIdAsync(id);

                return Ok(
                    new RentalDto
                    {
                        Id = rental.Id!,
                        DailyRate = rental.DailyRate,
                        DeliveryDriverId = rental.DeliveryDriverId,
                        MotorcycleId = rental.MotorcycleId,
                        StartDate = rental.StartDate,
                        EndDate = rental.EndDate,
                        PredictedEndDate = rental.PredictedEndDate,
                        ReturnDate = rental.ReturnDate,
                    }
                );
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new MessageResponseDto { Message = ex.Message });
            }
        }

        [HttpPut("{id}/devolucao")]
        [SwaggerOperation(Summary = "Informar data de devolução e calcular valor")]
        [SwaggerResponse(200, "Data de devolução informada com sucesso", typeof(RentalDto))]
        [SwaggerResponse(400, "Dados inválidos", typeof(MessageResponseDto))]
        [SwaggerResponse(404, "Locação não encontrada", typeof(MessageResponseDto))]
        public async Task<IActionResult> UpdateReturnDate(
            Guid id,
            [FromBody] UpdateReturnDateDto dto
        )
        {
            try
            {
                await _service.UpdateReturnDateAsync(id, dto.ReturnDate.ToUniversalTime());

                return Ok(
                    new MessageResponseDto { Message = "Data de devolução informada com sucesso" }
                );
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
    }
}
