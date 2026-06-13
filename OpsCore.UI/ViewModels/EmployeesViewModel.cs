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

        [ObservableProperty]
        private ObservableCollection<Employee> employees = new();

        [ObservableProperty]
        private bool isLoading;

        public EmployeesViewModel(IEmployeeRepository employeeRepository, IDepartmentRepository departmentRepository)
        {
            _employeeRepository = employeeRepository;
            _departmentRepository = departmentRepository;
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
            await LoadEmployeesAsync();
        }

        public async Task TogglePresentAsync(Employee employee)
        {
            employee.IsPresent = !employee.IsPresent;
            await _employeeRepository.UpdateAsync(employee);
            await LoadEmployeesAsync();
        }

        [RelayCommand]
        public async Task DeleteEmployeeAsync(Employee employee)
        {
            await _employeeRepository.DeleteAsync(employee.Id);
            Employees.Remove(employee);
        }
    }
}