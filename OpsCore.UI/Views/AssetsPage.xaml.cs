using Microsoft.UI.Xaml.Controls;
using OpsCore.UI.ViewModels;

namespace OpsCore.UI.Views
{
    public sealed partial class AssetsPage : Page
    {
        public AssetsViewModel ViewModel { get; }
        public AssetsPage()
        {
            this.InitializeComponent();
            ViewModel = new AssetsViewModel(
                App.Services.GetService(typeof(OpsCore.Core.Interfaces.IAssetRepository))
                as OpsCore.Core.Interfaces.IAssetRepository);
            _ = ViewModel.LoadAssetsAsync();
        }
    }
}
