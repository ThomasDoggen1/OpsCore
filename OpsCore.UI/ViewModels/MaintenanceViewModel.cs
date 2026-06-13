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
        private readonly IActivityLogRepository _activityLogRepository;
        private readonly MaintenanceService _maintenanceService;

        private const int SystemEmployeeId = 1;

        [ObservableProperty]
        private ObservableCollection<Asset> allAssets = new();

        [ObservableProperty]
        private ObservableCollection<Asset> overdueAssets = new();

        [ObservableProperty]
        private ObservableCollection<Asset> dueSoonAssets = new();

        [ObservableProperty]
        private ObservableCollection<Asset> okAssets = new();

        [ObservableProperty]
        private bool isLoading;

        public MaintenanceViewModel(IAssetRepository assetRepository, IActivityLogRepository activityLogRepository)
        {
            _assetRepository = assetRepository;
            _activityLogRepository = activityLogRepository;
            _maintenanceService = new MaintenanceService();
        }

        [RelayCommand]
        public async Task LoadAssetsAsync()
        {
            IsLoading = true;
            var assets = await _assetRepository.GetAllAsync();

            AllAssets = new ObservableCollection<Asset>(assets);

            OverdueAssets = new ObservableCollection<Asset>(
                assets.Where(a => _maintenanceService.IsOverdue(a)));

            DueSoonAssets = new ObservableCollection<Asset>(
                assets.Where(a => _maintenanceService.IsDueSoon(a)));

            OkAssets = new ObservableCollection<Asset>(
                assets.Where(a => !_maintenanceService.IsOverdue(a) && !_maintenanceService.IsDueSoon(a)));

            IsLoading = false;
        }

        public async Task MarkAsMaintainedAsync(Asset asset)
        {
            asset.LastMaintenanceDate = System.DateTime.Today;
            await _assetRepository.UpdateAsync(asset);

            await _activityLogRepository.AddAsync(new ActivityLog
            {
                AssetId = asset.Id,
                EmployeeId = SystemEmployeeId,
                Action = $"{asset.Name} maintenance completed",
                CreatedAt = System.DateTime.Now
            });

            await LoadAssetsAsync();
        }
    }
}