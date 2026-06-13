using Microsoft.UI.Xaml.Controls;
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
                App.Services.GetService(typeof(OpsCore.Core.Interfaces.IAssetRepository))
                as OpsCore.Core.Interfaces.IAssetRepository);
            _ = ViewModel.LoadAssetsAsync();
        }
    }
}