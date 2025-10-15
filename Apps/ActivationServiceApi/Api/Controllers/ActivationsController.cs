using Microsoft.AspNetCore.Mvc;
using Services.Contracts.Dtos.Activations;
using Services.Interfaces;

namespace Api.Controllers;

/// <summary>
/// Контроллер активаций подарочных карт
/// </summary>
[ApiController]
[Route("api/activations")]
public class ActivationsController : ControllerBase
{
    private readonly IActivationsService _activationsService;

    public ActivationsController(IActivationsService activationsService)
    {
        _activationsService = activationsService;
    }

    /// <summary>
    /// Получить все активации
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _activationsService.GetAllAsync();
        return Ok(data);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id, [FromQuery] string? expand = null)
    {
        var includeCard = string.Equals(expand, "card", StringComparison.OrdinalIgnoreCase)
                          || (expand?.Split(',').Any(x => x.Trim().Equals("card", StringComparison.OrdinalIgnoreCase)) ?? false);

        var data = await _activationsService.GetAsync(id, includeCard);
        if (data is null) return NotFound(new { error = "activation not found" });

        return Ok(data);
    }
    /// <summary>
    /// Создать активацию
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ActivationCreateRequest dto)
    {
        var id = await _activationsService.CreateAsync(dto);
        return Ok(id);
    }

    /// <summary>
    /// Подтвердить активацию
    /// </summary>
    [HttpPost("{id:guid}/confirm")]
    public async Task<IActionResult> Confirm(Guid id, [FromBody] ActivationConfirmRequest dto)
    {
        await _activationsService.ConfirmAsync(id, dto);
        return NoContent();
    }
}