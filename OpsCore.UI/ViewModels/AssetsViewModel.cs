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

        [ObservableProperty]
        private ObservableCollection<Asset> assets = new();

        [ObservableProperty]
        private bool isLoading;

        public AssetsViewModel(IAssetRepository assetRepository, IAssetTypeRepository assetTypeRepository, IEmployeeRepository employeeRepository)
        {
            _assetRepository = assetRepository;
            _assetTypeRepository = assetTypeRepository;
            _employeeRepository = employeeRepository;
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
            await LoadAssetsAsync();
        }

        [RelayCommand]
        public async Task DeleteAssetAsync(Asset asset)
        {
            await _assetRepository.DeleteAsync(asset.Id);
            Assets.Remove(asset);
        }
    }
}