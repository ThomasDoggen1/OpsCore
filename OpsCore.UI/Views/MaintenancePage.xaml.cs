using Microsoft.UI.Xaml.Controls;
using OpsCore.Core.Interfaces;
using OpsCore.Core.Models;
using OpsCore.UI.ViewModels;

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
                App.Services.GetService(typeof(IActivityLogRepository)) as IActivityLogRepository);
            _ = ViewModel.LoadAssetsAsync();
        }

        private async void MarkMaintained_Click(object sender, Microsoft.UI.Xaml.RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is Asset asset)
            {
                await ViewModel.MarkAsMaintainedAsync(asset);
            }
        }
    }
}