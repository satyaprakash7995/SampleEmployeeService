using AutoMapper;
using SampleEmployeeService.ApplicationLayer.Common.Interfaces.Repositories;
using SampleEmployeeService.ApplicationLayer.Features.Employees.Queries.GetEmployeeById;
using SampleEmployeeService.Domain.ResponseResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SampleEmployeeService.ApplicationLayer.Features.Employees.Handlers
{
    public class GetEmployeeByIdRequestHandler
        : IRequestHandler<GetEmployeeByIdCommand, BaseResponseResult<EmployeeDto>>
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;

        public GetEmployeeByIdRequestHandler(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        public async Task<BaseResponseResult<EmployeeDto>> Handle(
            GetEmployeeByIdCommand request,
            CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(request.EmployeeId);
            var employeeDto = _mapper.Map<EmployeeDto>(employee);

            return await BaseResponseResult<EmployeeDto>.SuccessAsync(employeeDto);
        }
    }
}
