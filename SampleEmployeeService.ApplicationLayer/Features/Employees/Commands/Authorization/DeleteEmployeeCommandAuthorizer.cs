using SampleEmployeeService.ApplicationLayer.Authorization.Models;
using SampleEmployeeService.ApplicationLayer.Common.Interfaces.Services;
using SampleEmployeeService.ApplicationLayer.Features.Employees.Commands.DeleteEmployee;
//using AnthonyTravelPortal.ApplicationLayer.Features.Clients.Commands.DeleteClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEmployeeService.ApplicationLayer.Features.Employees.Commands.Authorization
{
    public class DeleteEmployeeCommandAuthorizer : AbstractRequestAuthorizer<DeleteEmployeeCommand>
    {
        private readonly ICurrentUserService _currentUserService;

        public DeleteEmployeeCommandAuthorizer(ICurrentUserService currentUserService)
        {
            _currentUserService = currentUserService;
        }

        public override void BuildPolicy(DeleteEmployeeCommand request)
        {
            UseRequirement(new MustOwnEmployeeRequirement
            {
                EmployeeId = request.EmployeeId,
                UserId = _currentUserService.UserId
            });
        }
    }
}
