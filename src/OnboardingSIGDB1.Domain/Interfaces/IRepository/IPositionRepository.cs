using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Models;
using OnboardingSIGDB1.Domain.Responses;

namespace OnboardingSIGDB1.Data.Repositories.Base;

public interface IPositionRepository : IRepositoryBase<Position>
{
    Task<(IEnumerable<Position> data, int total)> FilterAsync(PositionFilter filter);
    Task<bool> AnyByPositionIdAsync(int positionId);
}