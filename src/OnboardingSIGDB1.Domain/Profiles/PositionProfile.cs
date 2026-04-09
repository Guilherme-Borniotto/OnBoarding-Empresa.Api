using AutoMapper;
using OnboardingSIGDB1.Domain.Dto.Create;
using OnboardingSIGDB1.Domain.Dto.Read;
using OnboardingSIGDB1.Domain.Dto.Update;
using OnboardingSIGDB1.Domain.Models;

namespace OnboardingSIGDB1.Domain.Profiles;

public class PositionProfile : Profile
{
    public PositionProfile()
    {
        CreateMap<CreatePositionDto, Position>();
        CreateMap<UpdatePositionDto, Position>();
        CreateMap<Position,ReadPositionDto>();
    }
}