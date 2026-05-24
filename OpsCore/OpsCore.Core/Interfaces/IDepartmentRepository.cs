using OpsCore.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpsCore.Core.Interfaces
{
    public interface IDepartmentRepository
    {
        Task<List<Department>> GetAllAsync();
        Task<Department?> GetByIdAsync(int id);
        Task AddAsync(Department department);
        Task UpdateAsync(Department department);
        Task DeletAsync(int id);
    }
}
