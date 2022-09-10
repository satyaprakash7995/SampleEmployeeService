using System.Linq;
using AutoMapper;
using SampleEmployeeService.ApplicationLayer.Features.Employees.Queries;
using SampleEmployeeService.ApplicationLayer.Features.Employees.Commands.CreateEmployee;
using SampleEmployeeService.Domain.Entities;
using SampleEmployeeService.ApplicationLayer.Features.Employees.Queries.GetEmployeeById;
using SampleEmployeeService.Controllers.Models;

namespace SampleEmployeeService.Profiles;

public class DtoMappingProfile : Profile
{
    public DtoMappingProfile()
    {
        CreateMap<CreateOrUpdateEmployeeDto, Employee>().ReverseMap();
        CreateMap<EmployeeDto, Employee>().ReverseMap();
        CreateMap<EmployeeVm, EmployeeDto>().ReverseMap();
        CreateMap<EmployeeVm, CreateOrUpdateEmployeeDto>().ReverseMap();
    }
   
}

