using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OpsCore.Core.Interfaces;
using OpsCore.Core.Models;
using OpsCore.Core.Services;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace OpsCore.UI.ViewModels
{
    public partial class MaintenanceViewModel : ObservableObject
    {
        private readonly IAssetRepository _assetRepository;
        private readonly MaintenanceService _maintenanceService;

        [ObservableProperty]
        private ObservableCollection<Asset> overdueAssets = new();

        [ObservableProperty]
        private ObservableCollection<Asset> dueSoonAssets = new();

        [ObservableProperty]
        private ObservableCollection<Asset> okAssets = new();

        [ObservableProperty]
        private bool isLoading;

        public MaintenanceViewModel(IAssetRepository assetRepository)
        {
            _assetRepository = assetRepository;
            _maintenanceService = new MaintenanceService();
        }

        [RelayCommand]
        public async Task LoadAssetsAsync()
        {
            IsLoading = true;
            var assets = await _assetRepository.GetAllAsync();

            OverdueAssets = new ObservableCollection<Asset>(
                assets.Where(a => _maintenanceService.IsOverdue(a)));

            DueSoonAssets = new ObservableCollection<Asset>(
                assets.Where(a => _maintenanceService.IsDueSoon(a)));

            OkAssets = new ObservableCollection<Asset>(
                assets.Where(a => !_maintenanceService.IsOverdue(a) && !_maintenanceService.IsDueSoon(a)));

            IsLoading = false;
        }
    }
}