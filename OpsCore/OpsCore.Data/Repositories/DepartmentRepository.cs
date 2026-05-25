using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using OpsCore.Core.Interfaces;
using OpsCore.Core.Models;
using OpsCore.Data.Context;

namespace OpsCore.Data.Repositories
{
    public class DepartmentRepository : IDepartmentRepository
    {
        private readonly AppDbContext _db;

        public DepartmentRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<List<Department>> GetAllAsync()
        {
            return await _db.Departments.ToListAsync();
        }

        public async Task<Department?> GetByIdAsync(int id)
        {
            return await _db.Departments.FindAsync(id);
        }

        public async Task AddAsync(Department department)
        {
            _db.Departments.Add(department);
            await _db.SaveChangesAsync();
        }

        public async Task UpdateAsync(Department department)
        {
            _db.Departments.Update(department);
            await _db.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var department = await _db.Departments.FindAsync(id);
            if (department != null)
            {
                _db.Departments.Remove(department);
                await _db.SaveChangesAsync();
            }
        }

        public Task DeletAsync(int id)
        {
            throw new NotImplementedException();
        }
    }
}
