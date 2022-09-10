using SampleEmployeeService.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEmployeeService.ApplicationLayer.Common.Interfaces.Repositories
{
    public interface IApplicationUserRepository
    {
        List<ApplicationUser> GetActiveUsers(out long totalCount, int skip, int take, string emailAddress = null);
    }
}
