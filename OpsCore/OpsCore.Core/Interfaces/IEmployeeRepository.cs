using OpsCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpsCore.Core.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync();
        Task<Employee?> GetByIdAsync(int id);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(int id);
    }
}
