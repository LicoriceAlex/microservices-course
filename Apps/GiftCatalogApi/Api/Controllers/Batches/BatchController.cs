using Api.Controllers.Batches.Dtos;
using Api.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Batches;

[ApiController]
[Route("api/batches")]
public class BatchController : ControllerBase
{
    private readonly IBatchUseCaseManager _batchUseCaseManager;

    public BatchController(IBatchUseCaseManager batchUseCaseManager)
    {
        _batchUseCaseManager = batchUseCaseManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _batchUseCaseManager.GetAllAsync();
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var response = await _batchUseCaseManager.GetByIdAsync(id);
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] BatchCreateRequest dto)
    {
        var response = await _batchUseCaseManager.CreateAsync(dto);
        return Ok(response);
    }

    [HttpPut("{id:guid}/close")]
    public async Task<IActionResult> Close(Guid id)
    {
        await _batchUseCaseManager.CloseAsync(id); 
        return NoContent();
    }
}