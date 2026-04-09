using AutoMapper;
using OnboardingSIGDB1.Domain.Dto.Create;
using OnboardingSIGDB1.Domain.Dto.Read;
using OnboardingSIGDB1.Domain.Dto.Update;
using OnboardingSIGDB1.Domain.Models;
using OnboardingSIGDB1.Domain.Utils;

namespace OnboardingSIGDB1.Domain.Profiles;

public class EmpolyeeProfile : Profile
{

    public EmpolyeeProfile()
    {
        CreateMap<CreateEmployeeDto, Employee>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore());
   
        CreateMap<UpdateEmployeeDto, Employee>()
            .ForMember(dest => dest.Cpf, opt => opt.MapFrom(src => StringUtils.removemask(src.Cpf)));
        CreateMap<Employee, ReadEmployeeDto>()
            .ForMember(dest => dest.CompanyName, opt => opt.MapFrom(src => src.Company.Name));
    }
}