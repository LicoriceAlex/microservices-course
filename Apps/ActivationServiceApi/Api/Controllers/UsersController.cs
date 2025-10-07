using Microsoft.AspNetCore.Mvc;
using Services.Contracts.Dtos.Users;
using Services.Interfaces;

namespace Api.Controllers;

/// <summary>
/// контроллер пользователей
/// </summary>
[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _svc;

    public UsersController(IUsersService svc)
    {
        _svc = svc;
    }

    /// <summary>получить всех пользователей</summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _svc.GetAllAsync();
        return Ok(data);
    }

    /// <summary>получить пользователя</summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var data = await _svc.GetAsync(id);
        return Ok(data);
    }

    /// <summary>создать пользователя</summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserCreateRequest dto)
    {
        var id = await _svc.CreateAsync(dto);
        return Ok(id);
    }
}