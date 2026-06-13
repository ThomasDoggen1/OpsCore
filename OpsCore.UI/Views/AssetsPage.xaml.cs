using Microsoft.UI.Xaml.Controls;
using OpsCore.Core.Enums;
using OpsCore.Core.Interfaces;
using OpsCore.Core.Models;
using OpsCore.UI.ViewModels;
using System;
using System.Linq;

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
                App.Services.GetService(typeof(IEmployeeRepository)) as IEmployeeRepository,
                App.Services.GetService(typeof(IActivityLogRepository)) as IActivityLogRepository);
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
                    LastMaintenanceDate = System.DateTime.Today,
                    MaintenanceIntervalDays = int.TryParse(intervalBox.Text, out var interval) ? interval : 90
                });
            }
        }

        private async void Asset_ItemClick(object sender, ItemClickEventArgs e)
        {
            var asset = e.ClickedItem as Asset;
            if (asset == null) return;

            var dialog = new ContentDialog
            {
                Title = $"Edit {asset.Name}",
                PrimaryButtonText = "Save",
                SecondaryButtonText = "Delete",
                CloseButtonText = "Cancel",
                XamlRoot = this.XamlRoot
            };

            var panel = new StackPanel { Spacing = 8 };

            var statusCombo = new ComboBox { PlaceholderText = "Status", Width = 300 };
            statusCombo.Items.Add("Active");
            statusCombo.Items.Add("Maintenance");
            statusCombo.Items.Add("Inactive");
            statusCombo.SelectedIndex = (int)asset.Status;

            var employeeCombo = new ComboBox { PlaceholderText = "Assign to Employee", Width = 300 };
            var employees = await ViewModel.GetEmployeesAsync();
            var noneOption = new Employee { Id = 0, FirstName = "None", LastName = "" };
            employeeCombo.Items.Add(noneOption);
            foreach (var emp in employees)
                employeeCombo.Items.Add(emp);
            employeeCombo.DisplayMemberPath = "FirstName";

            employeeCombo.SelectedItem = asset.EmployeeId == null
                ? noneOption
                : employees.FirstOrDefault(x => x.Id == asset.EmployeeId) ?? noneOption;

            panel.Children.Add(new TextBlock { Text = "Status" });
            panel.Children.Add(statusCombo);
            panel.Children.Add(new TextBlock { Text = "Assigned To" });
            panel.Children.Add(employeeCombo);

            dialog.Content = panel;

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                asset.Status = statusCombo.SelectedIndex switch
                {
                    0 => AssetStatus.Active,
                    1 => AssetStatus.Maintenance,
                    2 => AssetStatus.Inactive,
                    _ => asset.Status
                };

                var selectedEmployee = employeeCombo.SelectedItem as Employee;
                asset.EmployeeId = (selectedEmployee == null || selectedEmployee.Id == 0) ? null : selectedEmployee.Id;

                await ViewModel.UpdateAssetAsync(asset);
            }
            else if (result == ContentDialogResult.Secondary)
            {
                var confirmDialog = new ContentDialog
                {
                    Title = "Delete Asset",
                    Content = $"Are you sure you want to delete '{asset.Name}'?",
                    PrimaryButtonText = "Delete",
                    CloseButtonText = "Cancel",
                    XamlRoot = this.XamlRoot
                };

                var confirmResult = await confirmDialog.ShowAsync();
                if (confirmResult == ContentDialogResult.Primary)
                {
                    await ViewModel.DeleteAssetAsync(asset);
                }
            }
        }
    }
}