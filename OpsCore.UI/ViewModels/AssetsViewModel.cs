using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using OpsCore.Core.Interfaces;
using OpsCore.Core.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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

        private List<Asset> _allAssets = new();

        [ObservableProperty]
        private ObservableCollection<Asset> assets = new();

        [ObservableProperty]
        private bool isLoading;

        [ObservableProperty]
        private string searchText = string.Empty;

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
            _allAssets = await _assetRepository.GetAllAsync();
            ApplyFilter();
            IsLoading = false;
        }

        partial void OnSearchTextChanged(string value)
        {
            ApplyFilter();
        }

        private void ApplyFilter()
        {
            var filtered = string.IsNullOrWhiteSpace(SearchText)
                ? _allAssets
                : _allAssets.Where(a =>
                    a.Name.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ||
                    (a.AssetType?.Name.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (a.Location?.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ?? false) ||
                    (a.Employee?.FirstName.Contains(SearchText, System.StringComparison.OrdinalIgnoreCase) ?? false)
                ).ToList();

            Assets.Clear();
            foreach (var item in filtered)
                Assets.Add(item);
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
                AssetId = null,
                EmployeeId = SystemEmployeeId,
                Action = $"{asset.Name} removed from inventory",
                CreatedAt = System.DateTime.Now
            });

            await _assetRepository.DeleteAsync(asset.Id);
            await LoadAssetsAsync();
        }
    }
}