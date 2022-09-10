using SampleEmployeeService.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleEmployeeService.ApplicationLayer.Common.Interfaces.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployeeListAsync();

        Task<Employee> GetEmployeeByIdAsync(int employeeId);

        Task<int> InsertAsync(Employee employee);

        Task UpdateAsync(Employee employee);

        Task DeleteAsync(Employee employee);
    }
}
