using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using OpsCore.Core.Interfaces;
using OpsCore.Core.Models;
using OpsCore.UI.ViewModels;
using System;

namespace OpsCore.UI.Views
{
    public sealed partial class EmployeesPage : Page
    {
        public EmployeesViewModel ViewModel { get; }

        public EmployeesPage()
        {
            this.InitializeComponent();
            ViewModel = new EmployeesViewModel(
                App.Services.GetService(typeof(IEmployeeRepository)) as IEmployeeRepository,
                App.Services.GetService(typeof(IDepartmentRepository)) as IDepartmentRepository,
                App.Services.GetService(typeof(IActivityLogRepository)) as IActivityLogRepository);
            _ = ViewModel.LoadEmployeesAsync();
        }

        private async void AddEmployee_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            var dialog = new ContentDialog
            {
                Title = "Add Employee",
                PrimaryButtonText = "Save",
                CloseButtonText = "Cancel",
                XamlRoot = this.XamlRoot
            };

            var panel = new StackPanel { Spacing = 8 };
            var firstNameBox = new TextBox { PlaceholderText = "First Name" };
            var lastNameBox = new TextBox { PlaceholderText = "Last Name" };
            var emailBox = new TextBox { PlaceholderText = "Email" };
            var phoneBox = new TextBox { PlaceholderText = "Phone Number" };

            var departmentCombo = new ComboBox
            {
                PlaceholderText = "Select Department",
                Width = 300
            };

            var departments = await ViewModel.GetDepartmentsAsync();
            foreach (var dept in departments)
                departmentCombo.Items.Add(dept);
            departmentCombo.DisplayMemberPath = "Name";

            panel.Children.Add(firstNameBox);
            panel.Children.Add(lastNameBox);
            panel.Children.Add(emailBox);
            panel.Children.Add(phoneBox);
            panel.Children.Add(departmentCombo);
            dialog.Content = panel;

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                var selectedDept = departmentCombo.SelectedItem as Department;
                await ViewModel.AddEmployeeAsync(new Employee
                {
                    FirstName = firstNameBox.Text,
                    LastName = lastNameBox.Text,
                    Email = emailBox.Text,
                    PhoneNumber = phoneBox.Text,
                    DepartmentId = selectedDept?.Id ?? 1,
                    IsOnDuty = false,
                    IsPresent = false
                });
            }
        }

        private async void TogglePresent_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (sender is ToggleButton toggle && toggle.DataContext is Employee employee)
            {
                await ViewModel.TogglePresentAsync(employee);
            }
        }

        private async void DeleteEmployee_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Employee employee)
            {
                var confirmDialog = new ContentDialog
                {
                    Title = "Delete Employee",
                    Content = $"Are you sure you want to delete '{employee.FirstName} {employee.LastName}'?",
                    PrimaryButtonText = "Delete",
                    CloseButtonText = "Cancel",
                    XamlRoot = this.XamlRoot
                };

                var result = await confirmDialog.ShowAsync();
                if (result == ContentDialogResult.Primary)
                {
                    await ViewModel.DeleteEmployeeAsync(employee);
                }
            }
        }
    }
}