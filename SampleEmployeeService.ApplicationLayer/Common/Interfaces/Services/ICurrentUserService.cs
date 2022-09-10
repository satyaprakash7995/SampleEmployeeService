using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SampleEmployeeService.ApplicationLayer.Common.Interfaces.Services
{
    public interface ICurrentUserService
    {
        ClaimsPrincipal User { get; }
        string UserId { get; }
        string Username { get; }
        bool IsAdmin();
    }
}
