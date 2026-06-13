using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OpsCore.Core.Interfaces;
using OpsCore.Core.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace OpsCore.UI.ViewModels
{
    public partial class AssetsViewModel : ObservableObject
    {
        private readonly IAssetRepository _assetRepository;
        private readonly IAssetTypeRepository _assetTypeRepository;
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IActivityLogRepository _activityLogRepository;

        private const int SystemEmployeeId = 1;

        [ObservableProperty]
        private ObservableCollection<Asset> assets = new();

        [ObservableProperty]
        private bool isLoading;

        public AssetsViewModel(
            IAssetRepository assetRepository,
            IAssetTypeRepository assetTypeRepository,
            IEmployeeRepository employeeRepository,
            IActivityLogRepository activityLogRepository)
        {
            _assetRepository = assetRepository;
            _assetTypeRepository = assetTypeRepository;
            _employeeRepository = employeeRepository;
            _activityLogRepository = activityLogRepository;
        }

        [RelayCommand]
        public async Task LoadAssetsAsync()
        {
            IsLoading = true;
            var result = await _assetRepository.GetAllAsync();
            Assets = new ObservableCollection<Asset>(result);
            IsLoading = false;
        }

        public async Task<List<AssetType>> GetAssetTypesAsync()
        {
            return await _assetTypeRepository.GetAllAsync();
        }

        public async Task<List<Employee>> GetEmployeesAsync()
        {
            return await _employeeRepository.GetAllAsync();
        }

        public async Task AddAssetAsync(Asset asset)
        {
            await _assetRepository.AddAsync(asset);

            await _activityLogRepository.AddAsync(new ActivityLog
            {
                AssetId = asset.Id,
                EmployeeId = SystemEmployeeId,
                Action = "Added to inventory",
                CreatedAt = System.DateTime.Now
            });

            await LoadAssetsAsync();
        }

        public async Task UpdateAssetAsync(Asset asset)
        {
            await _assetRepository.UpdateAsync(asset);

            await _activityLogRepository.AddAsync(new ActivityLog
            {
                AssetId = asset.Id,
                EmployeeId = SystemEmployeeId,
                Action = "Status or assignment updated",
                CreatedAt = System.DateTime.Now
            });

            await LoadAssetsAsync();
        }

        public async Task DeleteAssetAsync(Asset asset)
        {
            await _activityLogRepository.AddAsync(new ActivityLog
            {
                AssetId = asset.Id,
                EmployeeId = SystemEmployeeId,
                Action = $"{asset.Name} removed from inventory",
                CreatedAt = System.DateTime.Now
            });

            await _assetRepository.DeleteAsync(asset.Id);
            Assets.Remove(asset);
        }
    }
}