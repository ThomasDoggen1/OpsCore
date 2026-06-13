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
    public partial class DashboardViewModel : ObservableObject
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IActivityLogRepository _activityLogRepository;
        private readonly MaintenanceService _maintenanceService;

        [ObservableProperty]
        private int totalAssets;

        [ObservableProperty]
        private int activeAssets;

        [ObservableProperty]
        private int maintenanceDue;

        [ObservableProperty]
        private int overdueAssets;

        [ObservableProperty]
        private ObservableCollection<ActivityLog> recentActivity = new();

        [ObservableProperty]
        private bool isLoading;

        public DashboardViewModel(IAssetRepository assetRepository, IActivityLogRepository activityLogRepository)
        {
            _assetRepository = assetRepository;
            _activityLogRepository = activityLogRepository;
            _maintenanceService = new MaintenanceService();
        }

        [RelayCommand]
        public async Task LoadDashboardAsync()
        {
            IsLoading = true;

            var assets = await _assetRepository.GetAllAsync();

            TotalAssets = assets.Count;
            ActiveAssets = assets.Count(a => a.Status == OpsCore.Core.Enums.AssetStatus.Active);
            MaintenanceDue = assets.Count(a => _maintenanceService.IsDueSoon(a));
            OverdueAssets = assets.Count(a => _maintenanceService.IsOverdue(a));

            var logs = await _activityLogRepository.GetRecentAsync(5);
            RecentActivity = new ObservableCollection<ActivityLog>(logs);

            IsLoading = false;
        }
    }
}