using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SampleEmployeeService.ApplicationLayer.Common.Interfaces.Repositories;
using SampleEmployeeService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEmployeeService.Infrastructure.Persistence.Repositories
{
    public  class EmployeeRepository : IEmployeeRepository
    {
        private readonly IRepositoryAsync<Employee> _repository;

        public EmployeeRepository(IRepositoryAsync<Employee> repository)
        {
            _repository = repository;
        }

        private IQueryable<Employee> Employees => _repository.Entities;
        public async Task<List<Employee>> GetEmployeeListAsync()
        {
            return await _repository.Entities.ToListAsync();
        }
        public async Task<Employee> GetEmployeeByIdAsync(int employeeId)
        {
            return  _repository.Entities.FirstOrDefault(x => x.EmployeeId == employeeId);
        }

        public async Task<int> InsertAsync(Employee employee)
        {
            await _repository.AddAsync(employee);
            return employee.EmployeeId;
        }

        public async Task UpdateAsync(Employee employee)
        {
            await _repository.UpdateAsync(employee);
        }

        public async Task DeleteAsync(Employee employee)
        {
            await _repository.DeleteAsync(employee);
        }
    }
}
