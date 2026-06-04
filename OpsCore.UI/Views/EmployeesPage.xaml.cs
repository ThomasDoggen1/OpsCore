using Microsoft.UI.Xaml.Controls;
using OpsCore.UI.ViewModels;

namespace OpsCore.UI.Views
{
    public sealed partial class EmployeesPage : Page
    {
        public EmployeesViewModel ViewModel { get; }

        public EmployeesPage()
        {
            this.InitializeComponent();
            ViewModel = new EmployeesViewModel(
                App.Services.GetService(typeof(OpsCore.Core.Interfaces.IEmployeeRepository))
                as OpsCore.Core.Interfaces.IEmployeeRepository);
            _ = ViewModel.LoadEmployeesAsync();
        }
    }
}