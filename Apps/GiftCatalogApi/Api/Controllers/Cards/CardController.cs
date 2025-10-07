using Api.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Cards;

[ApiController]
[Route("api/cards")]
public class CardController : ControllerBase
{
    private readonly ICardUseCaseManager _cardUseCaseManager;

    public CardController(ICardUseCaseManager cardUseCaseManager)
    {
        _cardUseCaseManager = cardUseCaseManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _cardUseCaseManager.GetAllAsync();
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var response = await _cardUseCaseManager.GetByIdAsync(id);
        return Ok(response);
    }

    [HttpPut("{id:guid}/block")]
    public async Task<IActionResult> Block(Guid id)
    {
        await _cardUseCaseManager.BlockAsync(id); 
        return NoContent();
    }

    [HttpPut("{id:guid}/unblock")]
    public async Task<IActionResult> Unblock(Guid id)
    {
        await _cardUseCaseManager.UnblockAsync(id); 
        return NoContent();
    }
}