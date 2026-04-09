using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Dto.Create;
using OnboardingSIGDB1.Domain.Dto.Read;
using OnboardingSIGDB1.Domain.Dto.Update;
using OnboardingSIGDB1.Domain.Responses;

namespace OnboardingSIGDB1.Domain.Interfaces.IServices;

public interface IPositionservice
{
    Task<ReadPositionDto> AddAsync(CreatePositionDto dto);
    Task <ReadPositionDto>UpdateAsync(UpdatePositionDto dto, int id);
    Task DeleteAsync(int id);
    Task<ReadPositionDto> GetByIdAsync(int id);

    Task<PagedResponse<ReadPositionDto>> GetByFiltersAsync(PositionFilter filter);
}