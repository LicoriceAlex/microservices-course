using Microsoft.AspNetCore.Mvc;
using Services.Contracts.Dtos.Activations;
using Services.Interfaces;

namespace Api.Controllers;

/// <summary>
/// контроллер активаций
/// </summary>
[ApiController]
[Route("api/activations")]
public class ActivationsController : ControllerBase
{
    private readonly IActivationsService _svc;

    public ActivationsController(IActivationsService svc)
    {
        _svc = svc;
    }

    /// <summary>получить все активации</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _svc.GetAllAsync();
        return Ok(data);
    }

    /// <summary>получить активацию</summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var data = await _svc.GetAsync(id);
        return Ok(data);
    }

    /// <summary>создать активацию</summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] ActivationCreateRequest dto)
    {
        var id = await _svc.CreateAsync(dto);
        return Ok(id);
    }

    /// <summary>подтвердить активацию</summary>
    [HttpPost("{id:guid}/confirm")]
    public async Task<IActionResult> Confirm(Guid id, [FromBody] ActivationConfirmRequest dto)
    {
        await _svc.ConfirmAsync(id, dto);
        return NoContent();
    }
}