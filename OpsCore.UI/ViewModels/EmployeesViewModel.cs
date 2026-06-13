using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OpsCore.Core.Interfaces;
using OpsCore.Core.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace OpsCore.UI.ViewModels
{
    public partial class EmployeesViewModel : ObservableObject
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IActivityLogRepository _activityLogRepository;

        private const int SystemEmployeeId = 1;

        [ObservableProperty]
        private ObservableCollection<Employee> employees = new();

        [ObservableProperty]
        private bool isLoading;

        public EmployeesViewModel(
            IEmployeeRepository employeeRepository,
            IDepartmentRepository departmentRepository,
            IActivityLogRepository activityLogRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
            _activityLogRepository = activityLogRepository;
        }

        [RelayCommand]
        public async Task LoadEmployeesAsync()
        {
            IsLoading = true;
            var result = await _employeeRepository.GetAllAsync();
            Employees = new ObservableCollection<Employee>(result);
            IsLoading = false;
        }

        public async Task<List<Department>> GetDepartmentsAsync()
        {
            return await _departmentRepository.GetAllAsync();
        }

        public async Task AddEmployeeAsync(Employee employee)
        {
            await _employeeRepository.AddAsync(employee);

            await _activityLogRepository.AddAsync(new ActivityLog
            {
                AssetId = 1, // placeholder, geen asset gekoppeld
                EmployeeId = SystemEmployeeId,
                Action = $"{employee.FirstName} {employee.LastName} added as employee",
                CreatedAt = System.DateTime.Now
            });

            await LoadEmployeesAsync();
        }

        public async Task TogglePresentAsync(Employee employee)
        {
            employee.IsPresent = !employee.IsPresent;
            await _employeeRepository.UpdateAsync(employee);
            await LoadEmployeesAsync();
        }

        public async Task DeleteEmployeeAsync(Employee employee)
        {
            await _activityLogRepository.AddAsync(new ActivityLog
            {
                AssetId = 1, // placeholder
                EmployeeId = SystemEmployeeId,
                Action = $"{employee.FirstName} {employee.LastName} removed as employee",
                CreatedAt = System.DateTime.Now
            });

            await _employeeRepository.DeleteAsync(employee.Id);
            Employees.Remove(employee);
        }
    }
}