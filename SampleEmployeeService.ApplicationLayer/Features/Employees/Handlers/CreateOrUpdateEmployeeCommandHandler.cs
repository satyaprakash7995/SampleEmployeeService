using AutoMapper;
using SampleEmployeeService.ApplicationLayer.Common.Exception;
using SampleEmployeeService.ApplicationLayer.Common.Interfaces.Repositories;
using SampleEmployeeService.ApplicationLayer.Features.Employees.Commands.CreateEmployee;
using SampleEmployeeService.Domain.Entities;
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
    public class CreateOrUpdateEmployeeCommandHandler :
        IRequestHandler<CreateOrUpdateEmployeeCommand, BaseResponseResult<CreateOrUpdateEmployeeDto>>
    {
        private readonly IMapper _mapper;
        private readonly IEmployeeRepository _EmployeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateOrUpdateEmployeeCommandHandler(
            IMapper mapper,
            IEmployeeRepository employeeRepository,
            IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _EmployeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<BaseResponseResult<CreateOrUpdateEmployeeDto>> Handle(CreateOrUpdateEmployeeCommand command,
            CancellationToken cancellationToken)
        {
            var dto = command.CreateEmployeeDto;

            Employee employee;
            var wasEmployeeAlreadyCreated = dto.EmployeeId != null && dto.EmployeeId.Value > 0;
            if (wasEmployeeAlreadyCreated)
            {
                employee = await UpdateEmployeeAsync(dto, cancellationToken);
                return await BaseResponseResult<CreateOrUpdateEmployeeDto>.SuccessAsync(_mapper.Map<CreateOrUpdateEmployeeDto>(employee));
            }
            else
            {
                employee = await CreateNewEmployeeAsync(dto, cancellationToken);
                var result = _mapper.Map<CreateOrUpdateEmployeeDto>(employee);
                return await BaseResponseResult<CreateOrUpdateEmployeeDto>.SuccessAsync(result, "Employee created");
            }
        }

        private async Task<Employee> CreateNewEmployeeAsync(CreateOrUpdateEmployeeDto dto, CancellationToken cancellationToken)
        {
            var employee = _mapper.Map<Employee>(dto);
            await _EmployeeRepository.InsertAsync(employee);
            await _unitOfWork.Commit(cancellationToken);

            return employee;
        }

        private async Task<Employee> UpdateEmployeeAsync(CreateOrUpdateEmployeeDto dto, CancellationToken cancellationToken)
        {
            var employee = await _EmployeeRepository.GetEmployeeByIdAsync(dto.EmployeeId.Value);

            if (employee == null)
                throw new NotFoundException(nameof(Employee), dto.EmployeeId);

            _mapper.Map(dto, employee);

            await _EmployeeRepository.UpdateAsync(employee);
            await _unitOfWork.Commit(cancellationToken);

            return employee;
        }
    }
}