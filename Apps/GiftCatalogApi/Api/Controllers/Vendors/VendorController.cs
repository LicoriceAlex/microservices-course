using Api.Controllers.Vendors.Dtos;
using Api.UseCases.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers.Vendors;

[ApiController]
[Route("api/vendors")]
public class VendorController : ControllerBase
{
    private readonly IVendorUseCaseManager _vendorUseCaseManager;

    public VendorController(IVendorUseCaseManager vendorUseCaseManager)
    {
        _vendorUseCaseManager = vendorUseCaseManager;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var response = await _vendorUseCaseManager.GetAllAsync();
        return Ok(response);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id)
    {
        var response = await _vendorUseCaseManager.GetByIdAsync(id);
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] VendorCreateRequest dto)
    {
        var response = await _vendorUseCaseManager.CreateAsync(dto);
        return Ok(response);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] VendorUpdateRequest dto)
    {
        await _vendorUseCaseManager.UpdateAsync(id, dto); 
        return NoContent();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _vendorUseCaseManager.DeleteAsync(id); return NoContent();
    }
}