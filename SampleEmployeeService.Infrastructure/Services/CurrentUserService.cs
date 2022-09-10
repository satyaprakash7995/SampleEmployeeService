using SampleEmployeeService.ApplicationLayer.Common.Interfaces.Services;
using SampleEmployeeService.Domain.Constants;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SampleEmployeeService.Infrastructure.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly IHttpContextAccessor _accessor;

        public CurrentUserService(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public ClaimsPrincipal User => _accessor?.HttpContext?.User;
        public string UserId => User?.FindFirstValue(ClaimTypes.NameIdentifier);
        public string Username => User?.FindFirstValue(ClaimTypes.Name);

        public bool IsAdmin()
        {
            return Constants.AdminRoles.Any(r => User.IsInRole(r)); 
        }
    }
}
