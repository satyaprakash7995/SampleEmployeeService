
using SampleEmployeeService.Domain.ResponseResult;
using MediatR;

namespace SampleEmployeeService.ApplicationLayer.Features.Employees.Commands.CreateEmployee
{
    public class CreateOrUpdateEmployeeCommand : IRequest<BaseResponseResult<CreateOrUpdateEmployeeDto>>
    {
        public CreateOrUpdateEmployeeDto? CreateEmployeeDto { get; init; }
    }
}
