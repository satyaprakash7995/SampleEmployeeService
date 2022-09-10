using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using SampleEmployeeService.ApplicationLayer.Common.Interfaces.Repositories;
using SampleEmployeeService.ApplicationLayer.Features.Employees.Queries.GetEmployeeById;
using SampleEmployeeService.ApplicationLayer.Features.Employees.Queries.GetEmployeeList;
using SampleEmployeeService.Domain.ResponseResult;
using AutoMapper;
using MediatR;


namespace SampleEmployeeService.ApplicationLayer.Features.Employees.Handlers;

public class GetEmployeeListCommandHandler : IRequestHandler<GetEmployeeListCommand,
    BaseResponseResult<List<EmployeeDto>>>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IMapper _mapper;

    public GetEmployeeListCommandHandler(IEmployeeRepository employeeRepository,
        IMapper mapper)
    {
        _employeeRepository = employeeRepository;
        _mapper = mapper;
    }

    public async Task<BaseResponseResult<List<EmployeeDto>>> Handle(
        GetEmployeeListCommand request,
        CancellationToken cancellationToken)
    {
        var employees = await _employeeRepository.GetEmployeeListAsync();
        var employeeDtos = _mapper.Map<List<EmployeeDto>>(employees);

        return await BaseResponseResult<List<EmployeeDto>>.SuccessAsync(employeeDtos);
    }
}