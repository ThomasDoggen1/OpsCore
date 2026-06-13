using Microsoft.EntityFrameworkCore;
using OpsCore.Core.Enums;
using OpsCore.Core.Models;

namespace OpsCore.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Asset> Assets { get; set; }
        public DbSet<AssetType> AssetTypes { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<MaintenanceRecord> MaintenanceRecords { get; set; }
        public DbSet<ActivityLog> ActivityLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Seed Departments
            modelBuilder.Entity<Department>().HasData(
                new Department { Id = 1, Name = "IT" },
                new Department { Id = 2, Name = "HR" },
                new Department { Id = 3, Name = "Finance" },
                new Department { Id = 4, Name = "Operations" }
            );

            // Seed AssetTypes
            modelBuilder.Entity<AssetType>().HasData(
                new AssetType { Id = 1, Name = "Laptop" },
                new AssetType { Id = 2, Name = "Server" },
                new AssetType { Id = 3, Name = "Router" },
                new AssetType { Id = 4, Name = "Switch" },
                new AssetType { Id = 5, Name = "Printer" }
            );

            // Seed Employees
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, FirstName = "John", LastName = "Smith", Email = "john@opscore.com", PhoneNumber = "0123456789", Password = "", IsOnDuty = true, IsPresent = true, DepartmentId = 1 },
                new Employee { Id = 2, FirstName = "Sarah", LastName = "Chen", Email = "sarah@opscore.com", PhoneNumber = "0123456788", Password = "", IsOnDuty = true, IsPresent = false, DepartmentId = 2 },
                new Employee { Id = 3, FirstName = "Mike", LastName = "Johnson", Email = "mike@opscore.com", PhoneNumber = "0123456787", Password = "", IsOnDuty = false, IsPresent = false, DepartmentId = 3 }
            );

            // Seed Assets
            modelBuilder.Entity<Asset>().HasData(
                new Asset { Id = 1, Name = "Dell Latitude 5520", AssetTypeId = 1, Status = AssetStatus.Active, Location = "Office A-101", EmployeeId = 1, LastMaintenanceDate = new DateTime(2024, 1, 15), MaintenanceIntervalDays = 90 },
                new Asset { Id = 2, Name = "MacBook Pro 16", AssetTypeId = 1, Status = AssetStatus.Active, Location = "Office B-202", EmployeeId = 2, LastMaintenanceDate = new DateTime(2024, 3, 10), MaintenanceIntervalDays = 180 },
                new Asset { Id = 3, Name = "Dell PowerEdge R740", AssetTypeId = 2, Status = AssetStatus.Maintenance, Location = "Server Room", EmployeeId = null, LastMaintenanceDate = new DateTime(2023, 12, 1), MaintenanceIntervalDays = 60 },
                new Asset { Id = 4, Name = "Cisco Switch 48 Port", AssetTypeId = 4, Status = AssetStatus.Active, Location = "Server Room", EmployeeId = null, LastMaintenanceDate = new DateTime(2024, 2, 20), MaintenanceIntervalDays = 120 },
                new Asset { Id = 5, Name = "HP LaserJet Pro", AssetTypeId = 5, Status = AssetStatus.Active, Location = "Office A-101", EmployeeId = null, LastMaintenanceDate = new DateTime(2024, 4, 1), MaintenanceIntervalDays = 90 }
            );

            // Seed ActivityLogs
            modelBuilder.Entity<ActivityLog>().HasData(
                new ActivityLog { Id = 1, AssetId = 1, EmployeeId = 1, Action = "Added to inventory", CreatedAt = new DateTime(2026, 5, 1) },
                new ActivityLog { Id = 2, AssetId = 2, EmployeeId = 2, Action = "Assigned to employee", CreatedAt = new DateTime(2026, 5, 3) },
                new ActivityLog { Id = 3, AssetId = 3, EmployeeId = 1, Action = "Status updated to Maintenance", CreatedAt = new DateTime(2026, 5, 5) }
            );
        }
    }
}