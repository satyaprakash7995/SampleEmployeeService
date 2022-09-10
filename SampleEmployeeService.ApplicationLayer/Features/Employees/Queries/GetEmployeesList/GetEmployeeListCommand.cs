using SampleEmployeeService.ApplicationLayer.Features.Employees.Queries.GetEmployeeById;
using SampleEmployeeService.Domain.ResponseResult;
using MediatR;

namespace SampleEmployeeService.ApplicationLayer.Features.Employees.Queries.GetEmployeeList;

public class GetEmployeeListCommand : IRequest<BaseResponseResult<List<EmployeeDto>>>
{

}