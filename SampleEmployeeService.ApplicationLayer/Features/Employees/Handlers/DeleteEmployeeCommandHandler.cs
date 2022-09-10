using SampleEmployeeService.ApplicationLayer.Common.Interfaces.Repositories;
using SampleEmployeeService.ApplicationLayer.Features.Employees.Commands.DeleteEmployee;
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
    public class DeleteEmployeeCommandHandler :
       IRequestHandler<DeleteEmployeeCommand, BaseResponseResult<bool>>
    {
        private readonly IMediator _mediator;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeleteEmployeeCommandHandler(IEmployeeRepository employeeRepository,
            IUnitOfWork unitOfWork, IMediator mediator)
        {
            _employeeRepository = employeeRepository;
            _unitOfWork = unitOfWork;
            _mediator = mediator;
        }
        public async Task<BaseResponseResult<bool>> Handle(
            DeleteEmployeeCommand request, CancellationToken cancellationToken)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(request.EmployeeId);

            if (employee == null)
                throw new InvalidOperationException($"No Employee is found with the given id: {request.EmployeeId}");

            await _employeeRepository.DeleteAsync(employee);
            var numOfRows = await _unitOfWork.Commit(cancellationToken);

            var hasDeletedItem = numOfRows > 0;

            return await BaseResponseResult<bool>.SuccessAsync(hasDeletedItem);
        }
    }
}
