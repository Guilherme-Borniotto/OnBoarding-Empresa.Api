using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Dto.Create;
using OnboardingSIGDB1.Domain.Dto.Read;
using OnboardingSIGDB1.Domain.Dto.Update;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Interfaces.IServices;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.API.Controller;

[ApiController]
[Route("Api/Company")]
public class CompanyController: ControllerBase
{
    private readonly ICompanyService  _companyService;
    private readonly INotificationContext _notificationContext;

    public CompanyController(ICompanyService companyService
    , INotificationContext notificationContext)
    {
        _companyService = companyService;
        _notificationContext = notificationContext;
    }


    [HttpGet("Filter")]
    
    public async Task<IActionResult> GetByFilterAsync([FromQuery]CompanyFilter? filter)
    {
        
       var result = await _companyService.GetByFiltersAsync(filter);

       if (_notificationContext.HasNotifications)
           return BadRequest(_notificationContext.Notifications);
       
       return Ok(result);
    }
   

    [HttpGet("{id}")]
    public async Task<IActionResult> GetByIdAsync(int id)
    {
      var result= await _companyService.GetByIdAsync(id);

      if (_notificationContext.HasNotifications)
          return BadRequest(_notificationContext.Notifications);

      return Ok(result);
    }
    
    
    [HttpPost]
    public async Task<IActionResult> AddCompanyAsync(CreateCompanyDto dto)
    {
        var result = await _companyService.AddAsync(dto);
        
        if (_notificationContext.HasNotifications)
            return BadRequest(_notificationContext.Notifications);
        
        return CreatedAtAction("GetById", new { id = result.Id }, result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateCompany(int id, UpdateCompanyDto dto)
    {
        var result = await _companyService.UpdateAsync(dto, id);
        
        if(_notificationContext.HasNotifications)
            return BadRequest(_notificationContext.Notifications);
        
        return Ok(result);
    }

    [HttpDelete]
    public async Task<IActionResult> DeleteCompanyAsync(int id)
    {
         await _companyService.Delete(id);
         
         if (_notificationContext.HasNotifications)
             return  BadRequest(_notificationContext.Notifications);
         
         return  NoContent();
    }
    
    // Endpoints Vinculo
    [HttpGet("Link/{employeeId}/{positionId}")]
    public async Task<IActionResult> GetByLinkId(int employeeId, int positionId)
    {
        // Busca um vínculo específico
        var result = await _companyService.GetByLinkIdAsync(employeeId, positionId);
        
        if (_notificationContext.HasNotifications)
            return BadRequest(_notificationContext.Notifications);
            
        return Ok(result);
    }

    [HttpPost("Link")]
    public async Task<IActionResult> LinkEmployeeToPositionAsync([FromBody] CreateEmployeeAndPositionDto dto)
    {
        // Cria a relação na tabela intermediária EmployeeAndPosition
      var result =  await _companyService.AddPositionLink(dto);

        if (_notificationContext.HasNotifications)
            return BadRequest(_notificationContext.Notifications);

        return CreatedAtAction(
            nameof(GetByLinkId),
            new { employeeid = result.EmployeeId, positionid = result.PositionId },
            result);
    }
    [HttpGet("Link/Filter")]
    public async Task<IActionResult> GetLinksByFilterAsync([FromQuery] EmployeeAndPositionFilter filter)
    {
        // Chama o service que retorna o PagedResponse de vínculos
        var result = await _companyService.GetByFilterLinkAsync(filter);

        if (_notificationContext.HasNotifications)
            return BadRequest(_notificationContext.Notifications);

        return Ok(result);
    }
    
    [HttpPut("Link/{employeeId}/{positionId}")]
    public async Task<IActionResult> UpdateLinkAsync(int employeeId, int positionId,UpdateEmployeeAndPositionDto dto)
    {
        var result = await _companyService.UpdatePositionLinkAsync(dto,employeeId, positionId);

        if (_notificationContext.HasNotifications)
            return BadRequest(_notificationContext.Notifications);

        return Ok(result);
    }
    
    
}