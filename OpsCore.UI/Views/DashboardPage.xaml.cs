using Microsoft.UI.Xaml.Controls;
using OpsCore.UI.ViewModels;

namespace OpsCore.UI.Views
{
    public sealed partial class DashboardPage : Page
    {
        public DashboardViewModel ViewModel { get; }

        public DashboardPage()
        {
            this.InitializeComponent();
            ViewModel = new DashboardViewModel(
                App.Services.GetService(typeof(OpsCore.Core.Interfaces.IAssetRepository))
                as OpsCore.Core.Interfaces.IAssetRepository,
                App.Services.GetService(typeof(OpsCore.Core.Interfaces.IActivityLogRepository))
                as OpsCore.Core.Interfaces.IActivityLogRepository);
            _ = ViewModel.LoadDashboardAsync();
        }
    }
}