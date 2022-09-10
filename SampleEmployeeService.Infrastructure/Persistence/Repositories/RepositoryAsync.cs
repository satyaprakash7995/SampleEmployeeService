using SampleEmployeeService.ApplicationLayer.Common.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using System.Linq.Expressions;
using System.Data.SqlClient;

namespace SampleEmployeeService.Infrastructure.Persistence.Repositories
{
    public class RepositoryAsync<T> : IRepositoryAsync<T> where T : class
    {
        protected readonly DbContext _dbContext;
        protected readonly IConfiguration _configuration;

        public RepositoryAsync(DbContext context, IConfiguration configuration)
        {
            _dbContext = context;
            _configuration = configuration;
        }

        public IQueryable<T> Entities => _dbContext.Set<T>();
        
        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public Task DeleteAsync(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
            return Task.CompletedTask;
        }     
        
        public async Task<List<T>> GetAllAsync()
        {
            return await _dbContext
                .Set<T>()
                .ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<List<T>> GetPagedResponseAsync(int pageNumber, int pageSize)
        {
            return await _dbContext
                .Set<T>()
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .AsNoTracking()
                .ToListAsync();
        }

        public Task UpdateAsync(T entity)
        {
            _dbContext.Entry(entity).CurrentValues.SetValues(entity);
            return Task.CompletedTask;
        }

        public Task AddRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().AddRange(entities);
            return Task.CompletedTask;
        }
        public Task RemoveRange(IEnumerable<T> entities)
        {
            _dbContext.Set<T>().RemoveRange(entities);
            return Task.CompletedTask;
        }
        public virtual void UpdateFromModel(T entity, object model)
        {
            _dbContext.Entry(entity).CurrentValues.SetValues(model);
        }
        public virtual int Count()
        {
            return Entities.Count();
        }
        public virtual bool Exists(Expression<Func<T, bool>> predicate)
        {
            return Entities.Any(predicate);
        }              

        public virtual IEnumerable<T> GetAll()
        {
            return Entities.ToList();
        }

    }
}
