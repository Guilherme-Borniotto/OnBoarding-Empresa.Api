using AutoMapper;
using OnboardingSIGDB1.Domain.Dto.Create;
using OnboardingSIGDB1.Domain.Dto.Read;
using OnboardingSIGDB1.Domain.Dto.Update;
using OnboardingSIGDB1.Domain.Models;
using OnboardingSIGDB1.Domain.Utils;

namespace OnboardingSIGDB1.Domain.Profiles;

public class CompanyProfile: Profile
{
    public CompanyProfile()
    {
        CreateMap<CreateCompanyDto, Company>();
        
        
        CreateMap<UpdateCompanyDto, Company>()
            .ForMember(dest => dest.Cnpj, opt => opt.MapFrom(src => StringUtils.removemask(src.Cnpj)));

        CreateMap<Company, ReadCompanyDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
        
        CreateMap<CreateEmployeeAndPositionDto, EmployeeAndPosition>();
        CreateMap<EmployeeAndPosition, ReadEmployeeAndPositionDto>()
            .ForMember(src => src.Situation, opt => opt.MapFrom(src => src.Situation))
            .ForMember(src => src.EmployeeId, opt => opt.MapFrom(src => src.EmployeeId))
            .ForMember(src => src.NameEmployee, opt => opt.MapFrom(src => src.Employee.Name))
            .ForMember(src => src.PositionId, opt => opt.MapFrom(src => src.PositionId))
            .ForMember(src => src.PositionName, opt => opt.MapFrom(src => src.Position.Name));
        
        CreateMap<UpdateEmployeeAndPositionDto, EmployeeAndPosition>()
            .ForMember(dest => dest.EmployeeId, opt => opt.Ignore())
            .ForMember(dest => dest.PositionId, opt => opt.Ignore());

        CreateMap<EmployeeAndPosition, ReadEmployeeAndPositionDto>()
            .ForMember(dest => dest.NameEmployee, opt => opt.MapFrom(src => src.Employee.Name))
            .ForMember(dest => dest.PositionName, opt => opt.MapFrom(src => src.Position.Name));
    }
}