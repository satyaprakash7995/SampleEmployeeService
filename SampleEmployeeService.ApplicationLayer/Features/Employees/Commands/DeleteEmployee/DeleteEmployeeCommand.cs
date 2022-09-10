using SampleEmployeeService.Domain.ResponseResult;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEmployeeService.ApplicationLayer.Features.Employees.Commands.DeleteEmployee
{
    public class DeleteEmployeeCommand : IRequest<BaseResponseResult<bool>>
    {
        public int EmployeeId { get; set; }
    }
}

