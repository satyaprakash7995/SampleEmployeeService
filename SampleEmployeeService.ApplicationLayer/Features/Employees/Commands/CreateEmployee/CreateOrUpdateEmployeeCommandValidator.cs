using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEmployeeService.ApplicationLayer.Features.Employees.Commands.CreateEmployee
{
    public class CreateOrUpdateEmployeeCommandValidator : AbstractValidator<CreateOrUpdateEmployeeCommand>
    {
        public CreateOrUpdateEmployeeCommandValidator()
        {
            RuleFor(a => a.CreateEmployeeDto)
                .SetValidator(new CreateOrUpdateEmployeeDtoValidator());
        }
    }
}
