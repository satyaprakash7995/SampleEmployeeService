using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SampleEmployeeService.ApplicationLayer.Common.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEmployeeService.Infrastructure.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {

        private readonly SampleEmployeeServiceDbContext _dbContext;

        public UnitOfWork(SampleEmployeeServiceDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<int> Commit(CancellationToken cancellationToken = default)
        {
            return await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
