using SampleEmployeeService.ApplicationLayer.Authorization.Interfaces;
using SampleEmployeeService.ApplicationLayer.Authorization.Models;
using SampleEmployeeService.ApplicationLayer.Common.Interfaces.Contexts;
using SampleEmployeeService.Domain.Constants;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SampleEmployeeService.ApplicationLayer.Features.Employees.Commands.Authorization
{
    public class MustOwnEmployeeRequirement : IAuthorizationRequirement
    {
        public int? EmployeeId { get; set; }
        public string? UserId { get; set; }

        private class MustOwnClientRequirementHandler : IAuthorizationHandler<MustOwnEmployeeRequirement>
        {
            private readonly ISampleEmployeeServiceDbContext _dbContext;

            public MustOwnClientRequirementHandler(ISampleEmployeeServiceDbContext dbContext)
            {
                _dbContext = dbContext;
            }

            public async Task<AuthorizationResult> Handle(
                MustOwnEmployeeRequirement request,
                CancellationToken cancellationToken)
            {
                var isCreateEmployeeCommand = request.EmployeeId == null;
                if (isCreateEmployeeCommand)
                {
                    return AuthorizationResult.Succeed();
                }

                var isUserClient = await _dbContext.Employees
                    .AnyAsync(x =>
                        x.EmployeeId == request.EmployeeId, cancellationToken);

                return isUserClient
                    ? AuthorizationResult.Succeed()
                    : AuthorizationResult.Fail("You don't own this Client to view.");
            }
        }
    }
}
