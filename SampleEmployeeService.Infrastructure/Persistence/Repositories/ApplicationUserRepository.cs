using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SampleEmployeeService.ApplicationLayer.Common.Interfaces.Repositories;
using SampleEmployeeService.Domain.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEmployeeService.Infrastructure.Persistence.Repositories
{
    public class ApplicationUserRepository : RepositoryAsync<ApplicationUser>, IApplicationUserRepository
    {
        private SampleEmployeeServiceDbContext _appContext => (SampleEmployeeServiceDbContext)_dbContext;
        public ApplicationUserRepository(SampleEmployeeServiceDbContext context, IConfiguration configuration) : base(context, configuration)
        { }
        public List<ApplicationUser> GetActiveUsers(out long totalCount, int skip, int take, string emailAddress = null)
        {
            totalCount = (from u in _appContext.Users
                          where u.IsActive && (emailAddress == null || u.Email == emailAddress)
                          select u.Id).Count();

            var query = (from u in _appContext.Users
                         where u.IsActive && (emailAddress == null || u.Email == emailAddress)
                         select u);

            var list = query.Skip(skip).Take(take).ToList();

            return list;
        }

    }
}
