using SampleEmployeeService.ApplicationLayer.Authorization.Models;
using SampleEmployeeService.ApplicationLayer.Common.Interfaces.Services;
using SampleEmployeeService.ApplicationLayer.Features.Employees.Commands.CreateEmployee;

namespace SampleEmployeeService.ApplicationLayer.Features.Employees.Commands.Authorization;

public class CreateOrUpdateEmployeeCommandAuthorizer : AbstractRequestAuthorizer<CreateOrUpdateEmployeeCommand>
{
    private readonly ICurrentUserService _currentUserService;

    public CreateOrUpdateEmployeeCommandAuthorizer(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public override void BuildPolicy(CreateOrUpdateEmployeeCommand request)
    {
        UseRequirement(new MustOwnEmployeeRequirement
        {
            EmployeeId = request.CreateEmployeeDto.EmployeeId,
            UserId = _currentUserService.UserId
        });
    }
}