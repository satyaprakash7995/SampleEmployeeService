using SampleEmployeeService.Domain.Entities;
using SampleEmployeeService.Domain.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEmployeeService.ApplicationLayer.Common.Interfaces.Contexts
{
    public interface ISampleEmployeeServiceDbContext
    {
        IDbConnection Connection { get; }
        DbSet<ApplicationUser> ApplicationUsers { get; set; }       
        public DbSet<Employee> Employees { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
