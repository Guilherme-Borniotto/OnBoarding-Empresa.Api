using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Dto.Create;
using OnboardingSIGDB1.Domain.Dto.Update;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Interfaces.IServices;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.API.Controller;

[ApiController]
[Route("api/Employee")]
public class EmployeeController: ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly INotificationContext _notificationContext;

    public EmployeeController(IEmployeeService employeeService, INotificationContext notificationContext)
    {
        _employeeService = employeeService;
        _notificationContext = notificationContext;
    }

    [HttpGet("Filter")]
    public async Task<IActionResult> GetByFilterAsync([FromQuery] EmployeeFilter filter)
    {
        var result = await _employeeService.GetByFiltersAsync(filter);
        
        if (_notificationContext.HasNotifications)
            return BadRequest(_notificationContext.Notifications);
            
        return Ok(result);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
        var result = await _employeeService.GetByIdAsync(id);

        if (_notificationContext.HasNotifications)
            return BadRequest(_notificationContext.Notifications);

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> AddEmployeeAsync(CreateEmployeeDto dto)
    {
        var result = await _employeeService.AddAsync(dto);
        
        if (_notificationContext.HasNotifications)
            return BadRequest(_notificationContext.Notifications);
        
       
        return CreatedAtAction("GetById", new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateEmployeeAsync(int id, UpdateEmployeeDto dto)
    {
        var result = await _employeeService.UpdateAsync(dto, id);
        
        if (_notificationContext.HasNotifications)
            return BadRequest(_notificationContext.Notifications);
        
        return Ok(result);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteEmployeeAsync(int id)
    {
        await _employeeService.DeleteAsync(id);
        
        if (_notificationContext.HasNotifications)
            return BadRequest(_notificationContext.Notifications);
        
        return NoContent();
    }
    
}