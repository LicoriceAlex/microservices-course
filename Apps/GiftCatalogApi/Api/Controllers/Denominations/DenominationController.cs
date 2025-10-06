using Api.Controllers.Denominations.Dtos;
using Api.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Denominations;

[ApiController]
[Route("api/denominations")]
public class DenominationController : ControllerBase
{
    private readonly IDenominationUseCaseManager _denominationUseCaseManager;

    public DenominationController(IDenominationUseCaseManager denominationUseCaseManager)
    {
        _denominationUseCaseManager = denominationUseCaseManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _denominationUseCaseManager.GetAllAsync();
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var response = await _denominationUseCaseManager.GetByIdAsync(id);  
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] DenominationCreateRequest dto)
    {
        var response = await _denominationUseCaseManager.CreateAsync(dto);
        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] DenominationUpdateRequest dto)
    {
        await _denominationUseCaseManager.UpdateAsync(id, dto); 
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _denominationUseCaseManager.DeleteAsync(id); return NoContent();
    }
}