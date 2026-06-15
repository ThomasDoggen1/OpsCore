using Microsoft.UI.Xaml.Controls;
using OpsCore.Core.Enums;
using OpsCore.Core.Interfaces;
using OpsCore.Core.Models;
using OpsCore.UI.ViewModels;
using System;

namespace OpsCore.UI.Views
{
    public sealed partial class MaintenancePage : Page
    {
        public MaintenanceViewModel ViewModel { get; }

        public MaintenancePage()
        {
            this.InitializeComponent();
            ViewModel = new MaintenanceViewModel(
                App.Services.GetService(typeof(IAssetRepository)) as IAssetRepository,
                App.Services.GetService(typeof(IActivityLogRepository)) as IActivityLogRepository,
                App.Services.GetService(typeof(IMaintenanceRecordRepository)) as IMaintenanceRecordRepository);
            _ = ViewModel.LoadAssetsAsync();
        }

        private async void MarkMaintained_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (sender is not Button btn || btn.Tag is not Asset asset) return;

            var dialog = new ContentDialog
            {
                Title = $"Complete Maintenance — {asset.Name}",
                PrimaryButtonText = "Save",
                CloseButtonText = "Cancel",
                XamlRoot = this.XamlRoot
            };

            var panel = new StackPanel { Spacing = 8 };

            var typeCombo = new ComboBox { PlaceholderText = "Maintenance Type", Width = 300 };
            typeCombo.Items.Add("Periodic");
            typeCombo.Items.Add("Emergency");
            typeCombo.Items.Add("Inspection");
            typeCombo.Items.Add("Repair");
            typeCombo.SelectedIndex = 0;

            var notesBox = new TextBox
            {
                PlaceholderText = "Notes (optional)",
                Width = 300,
                AcceptsReturn = true,
                Height = 80,
                TextWrapping = Microsoft.UI.Xaml.TextWrapping.Wrap
            };

            panel.Children.Add(new TextBlock { Text = "Type" });
            panel.Children.Add(typeCombo);
            panel.Children.Add(new TextBlock { Text = "Notes" });
            panel.Children.Add(notesBox);

            dialog.Content = panel;

            var result = await dialog.ShowAsync();

            if (result == ContentDialogResult.Primary)
            {
                var type = typeCombo.SelectedIndex switch
                {
                    0 => MaintenanceType.Periodic,
                    1 => MaintenanceType.Emergency,
                    2 => MaintenanceType.Inspection,
                    3 => MaintenanceType.Repair,
                    _ => MaintenanceType.Periodic
                };

                await ViewModel.CompleteMaintenanceAsync(asset, type, notesBox.Text);
            }
        }
    }
}