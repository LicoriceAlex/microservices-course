using Microsoft.AspNetCore.Mvc;
using Services.Contracts.Dtos.Users;
using Services.Interfaces;

namespace Api.Controllers;

/// <summary>
/// Контроллер пользователей
/// </summary>
[ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUsersService _usersService;

    public UsersController(IUsersService usersService)
    {
        _usersService = usersService;
    }

    /// <summary>
    /// Получить всех пользователей
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var data = await _usersService.GetAllAsync();
        return Ok(data);
    }

    /// <summary>
    /// Получить пользователя
    /// </summary>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var data = await _usersService.GetAsync(id);
        return Ok(data);
    }

    /// <summary>
    /// Создать пользователя
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] UserCreateRequest dto)
    {
        var id = await _usersService.CreateAsync(dto);
        return Ok(id);
    }
}