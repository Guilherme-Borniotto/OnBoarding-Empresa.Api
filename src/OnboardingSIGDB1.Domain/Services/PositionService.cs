using AutoMapper;
using OnboardingSIGDB1.Data.Repositories.Base;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Dto.Create;
using OnboardingSIGDB1.Domain.Dto.Read;
using OnboardingSIGDB1.Domain.Dto.Update;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Interfaces.IServices;
using OnboardingSIGDB1.Domain.Models;
using OnboardingSIGDB1.Domain.Responses;

namespace OnboardingSIGDB1.Domain.Services;

public class PositionService : IPositionservice
{
    
    private readonly IPositionRepository _positionRepository;
    private readonly IUnitOfWork _uow;
    private readonly IMapper _mapper;
    private readonly INotificationContext _notification;

    public PositionService(
        IPositionRepository positionRepository, 
        IUnitOfWork uow, 
        IMapper mapper, 
        INotificationContext notification)
    
    {
        _positionRepository = positionRepository;
        _uow = uow;
        _mapper = mapper;
        _notification = notification;
    }
    

    public async Task<ReadPositionDto> AddAsync(CreatePositionDto dto)
    {
        var Position = _mapper.Map<Position>(dto);

        if (!Position.Validate())
        {
            _notification.AddNotifications(Position.Notifications);
        }

        await _positionRepository.AddAsync(Position);

        if (!await _uow.Commit())
        {
            _notification.AddNotification("Database", "error while persisting the data.");
            return null;
        }
        return _mapper.Map<ReadPositionDto>(Position);
    }

    public async Task<ReadPositionDto> UpdateAsync(UpdatePositionDto dto, int id)
    {
        var PositionExists = await _positionRepository.GetByIdAsync(id);
        if (PositionExists == null)
        {
            _notification.AddNotification("PositionId", "Position not found.");
            return null;
        }

       
        _mapper.Map(dto, PositionExists);

        if (!PositionExists.Validate())
        {
            _notification.AddNotifications(PositionExists.Notifications);
            return null;
        }

        _positionRepository.Update(PositionExists);

        if (!await _uow.Commit())
        {
            _notification.AddNotification("Database", "Error updating data.");
            return null;
        }
        return _mapper.Map<ReadPositionDto>(PositionExists);
    }

    public async Task DeleteAsync(int id)
    {
        var PositionExists = await _positionRepository.GetByIdAsync(id);
        if (PositionExists == null)
        {
            _notification.AddNotification("PositionId", "Position not found.");
            return;
        }
        
        var isPositionInUse = await _positionRepository.AnyByPositionIdAsync(id);
        if (isPositionInUse)
        {
          
            _notification.AddNotification("Position", "Cannot delete this position because it is linked to one or more employees.");
            return ; 
        }
        
        _positionRepository.Delete(PositionExists);
        if (!await _uow.Commit())
        {
            _notification.AddNotification("Database", "Error deleting the Employee.");
            
        }
    }

    public async Task<ReadPositionDto> GetByIdAsync(int id)
    {
        var PositionExists = await _positionRepository.GetByIdAsync(id);
        
        if(PositionExists == null)
            _notification.AddNotification("PositionId", "Position not found.");
        
        return _mapper.Map<ReadPositionDto>(PositionExists);
    }


    public async Task<PagedResponse<ReadPositionDto>> GetByFiltersAsync(PositionFilter filter)
    {
        var (positions, total) = await _positionRepository.FilterAsync(filter);
        
        var positionsDto = _mapper.Map<IEnumerable<ReadPositionDto>>(positions);

    
        return new PagedResponse<ReadPositionDto>(
            positionsDto, 
            filter.Page, 
            filter.PageSize, 
            total
        );
    }
}

