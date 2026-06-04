using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OpsCore.Core.Interfaces;
using OpsCore.Core.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace OpsCore.UI.ViewModels
{
    public partial class EmployeesViewModel : ObservableObject
    {
        private readonly IEmployeeRepository _employeeRepository;

        [ObservableProperty]
        private ObservableCollection<Employee> employees = new();

        [ObservableProperty]
        private Employee? selectedEmployee;

        [ObservableProperty]
        private bool isLoading;

        public EmployeesViewModel(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        [RelayCommand]
        public async Task LoadEmployeesAsync()
        {
            IsLoading = true;
            var result = await _employeeRepository.GetAllAsync();
            Employees = new ObservableCollection<Employee>(result);
            IsLoading = false;
        }

        [RelayCommand]
        public async Task DeleteEmployeeAsync(Employee employee)
        {
            await _employeeRepository.DeleteAsync(employee.Id);
            Employees.Remove(employee);
        }
    }
}