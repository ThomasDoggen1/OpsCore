using Microsoft.UI.Xaml.Controls;
using OpsCore.Core.Enums;
using OpsCore.Core.Interfaces;
using OpsCore.Core.Models;
using OpsCore.UI.ViewModels;
using System;

namespace OpsCore.UI.Views
{
    public sealed partial class AssetsPage : Page
    {
        public AssetsViewModel ViewModel { get; }

        public AssetsPage()
        {
            this.InitializeComponent();
            ViewModel = new AssetsViewModel(
                App.Services.GetService(typeof(IAssetRepository)) as IAssetRepository,
                App.Services.GetService(typeof(IAssetTypeRepository)) as IAssetTypeRepository,
                App.Services.GetService(typeof(IEmployeeRepository)) as IEmployeeRepository);
            _ = ViewModel.LoadAssetsAsync();
        }

        private async void AddAsset_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            var dialog = new ContentDialog
            {
                Title = "Add Asset",
                PrimaryButtonText = "Save",
                CloseButtonText = "Cancel",
                XamlRoot = this.XamlRoot
            };

            var panel = new StackPanel { Spacing = 8 };
            var nameBox = new TextBox { PlaceholderText = "Asset Name" };
            var locationBox = new TextBox { PlaceholderText = "Location" };
            var intervalBox = new TextBox { PlaceholderText = "Maintenance Interval (days)" };

            var typeCombo = new ComboBox { PlaceholderText = "Select Type", Width = 300 };
            var assetTypes = await ViewModel.GetAssetTypesAsync();
            foreach (var type in assetTypes)
                typeCombo.Items.Add(type);
            typeCombo.DisplayMemberPath = "Name";

            var employeeCombo = new ComboBox { PlaceholderText = "Assign to Employee", Width = 300 };
            var employees = await ViewModel.GetEmployeesAsync();
            foreach (var emp in employees)
                employeeCombo.Items.Add(emp);
            employeeCombo.DisplayMemberPath = "FirstName";

            var statusCombo = new ComboBox { PlaceholderText = "Status", Width = 300 };
            statusCombo.Items.Add("Active");
            statusCombo.Items.Add("Maintenance");
            statusCombo.Items.Add("Inactive");

            panel.Children.Add(nameBox);
            panel.Children.Add(locationBox);
            panel.Children.Add(typeCombo);
            panel.Children.Add(statusCombo);
            panel.Children.Add(employeeCombo);
            panel.Children.Add(intervalBox);
            dialog.Content = panel;

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                var status = statusCombo.SelectedIndex switch
                {
                    0 => AssetStatus.Active,
                    1 => AssetStatus.Maintenance,
                    2 => AssetStatus.Inactive,
                    _ => AssetStatus.Active
                };

                var selectedType = typeCombo.SelectedItem as AssetType;
                var selectedEmployee = employeeCombo.SelectedItem as Employee;

                await ViewModel.AddAssetAsync(new Asset
                {
                    Name = nameBox.Text,
                    Location = locationBox.Text,
                    Status = status,
                    AssetTypeId = selectedType?.Id ?? 1,
                    EmployeeId = selectedEmployee?.Id,
                    LastMaintenanceDate = DateTime.Today,
                    MaintenanceIntervalDays = int.TryParse(intervalBox.Text, out var interval) ? interval : 90
                });
            }
        }
    }
}