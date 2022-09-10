using SampleEmployeeService.Domain.ResponseResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEmployeeService.ApplicationLayer.Features.Employees.Queries.GetEmployeeById
{
    public class GetEmployeeByIdCommand : IRequest<BaseResponseResult<EmployeeDto>>
    {
        public int EmployeeId { get; set; }
    }
}
