using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEmployeeService.ApplicationLayer.Features.Employees.Commands.CreateEmployee
{
    public class CreateOrUpdateEmployeeDtoValidator : AbstractValidator<CreateOrUpdateEmployeeDto>
    {
        public CreateOrUpdateEmployeeDtoValidator()
        {

        }
    }
}
