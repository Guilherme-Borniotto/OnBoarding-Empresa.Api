using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Dto.Create;
using OnboardingSIGDB1.Domain.Dto.Update;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Interfaces.IServices;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.API.Controller;

[ApiController]
[Route("api/position")]
public class PositionController : ControllerBase
{
    private readonly IPositionservice _positionService;
    private readonly INotificationContext _notificationContext;

    public PositionController(IPositionservice positionService, INotificationContext notificationContext)
    {
        _positionService = positionService;
        _notificationContext = notificationContext;
    }

    [HttpGet]
    public async Task<IActionResult> GetByFiltersAsync([FromQuery] PositionFilter filter)
    {
        // O Service agora retorna PagedResponse<ReadPositionDto>
        var result = await _positionService.GetByFiltersAsync(filter);

        if (_notificationContext.HasNotifications)
            return BadRequest(_notificationContext.Notifications);

        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await _positionService.GetByIdAsync(id);

        if (_notificationContext.HasNotifications)
            return BadRequest(_notificationContext.Notifications);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> CreateAsync([FromBody] CreatePositionDto dto)
    {
        var result =await _positionService.AddAsync(dto);

        if (_notificationContext.HasNotifications)
            return BadRequest(_notificationContext.Notifications);

        return CreatedAtAction("GetById", new { id = result.Id }, dto);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAsync(UpdatePositionDto dto,int id)
    {
        var result =await _positionService.UpdateAsync(dto,id);

        if (_notificationContext.HasNotifications)
            return BadRequest(_notificationContext.Notifications);

        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        await _positionService.DeleteAsync(id);

        if (_notificationContext.HasNotifications)
            return BadRequest(_notificationContext.Notifications);

        return NoContent();
    }
}